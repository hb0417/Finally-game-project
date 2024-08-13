using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GoSystem
{
    [GBehaviourAttribute("PipClimb Controller", true)]
    public class PipClimb : GoSystemsBehaviour
    {
        public float speed = 1;
        public GoInput PipeInputButtons;
        private GoInputSystem IsInput = new GoInputSystem();
        public float offsetUp = 0.5f, offsetDown = 0.2f, offsetYAxisUp = 1;
        private bool PipeActivate, Exit, ExitDown; bool lockOut,MobileInputSystem;
        private GameObject point;
        private LadderPoint pointer;
        private Transform PointPositionTarget;
        public Vector3 Offset;private string Active = "PipeActive",Tag = "PipeClimb";
        private Vector3 fixYpos; Ladder ladder = new Ladder();
        GoSystems Gs; GoCameraSystem goCamera = new GoCameraSystem();
        private int AnitmationRaite = 1;
        private bool lockcode =true, lockclimb=true, lockPos;
        public UnityEngine.Events.UnityEvent onAction, ExitAction;
        private void Start()
        {
            Gs = GoSystems.getSystem(gameObject);
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.tag == Tag && other.transform.root.GetComponent<LadderPoint>() != null)
            {

                if (!PipeActivate)
                {
                    pointer = other.transform.root.GetComponent<LadderPoint>();
                    point = other.gameObject;
                    UiActive(true);
                    var getInput = IsInput.GetKeyDown(PipeInputButtons.Kaybord.ToString(), PipeInputButtons.Joystick.ToString(), MobileInputSystem);
                    if (getInput)
                    {
                        if (point == pointer.PointDown.gameObject)
                        {
                            GoSystems.animatorControler.SetInteger(ladder.Livels, 1);
                            GoSystems.animatorControler.SetTrigger(Active);
                            fixYpos = point.transform.position;
                            fixYpos.y += offsetDown;
                            Gs.AxisCtrl = false;
                            GoSystems.OnExit = new UnityEngine.Events.UnityEvent();
                            GoSystems.OnExit.AddListener(IsAcyivatedSystem);
                            LockSystems();
                        }
                        else if (point != null && point == pointer.PointUp.gameObject)
                        {
                            fixYpos = point.transform.position;
                            fixYpos.y -= offsetUp;
                            Gs.AxisCtrl = false;
                            GoSystems.animatorControler.SetInteger(ladder.Livels, -1);
                            GoSystems.animatorControler.SetTrigger(Active);
                            GoSystems.OnExit = new UnityEngine.Events.UnityEvent();
                            GoSystems.OnExit.AddListener(IsAcyivatedSystem);
                            LockSystems();
                        }
                    }

                }
                else
                {
                    if (!lockcode)
                    {
                        EndingPointer(other.gameObject);
                    }
                }


            }
            else if (!PipeActivate&& lockcode)
            {
                UiActive(false,true);
                pointer = null;
                point = null;
            }
        }
        private void IsAcyivatedSystem()
        {
            Gs.AxisCtrl = true;
            lockcode = false;
        }
        private void EndingPointer(GameObject gameObject)
        {
            if (!lockcode)
            {
                if (gameObject == pointer.PointUp.gameObject && GoSystems.Axis.z != 0 && lockclimb == false && PipeActivate == true)
                {
                    endPipeUp();


                }
                else if (gameObject.gameObject == pointer.PointDown.gameObject && GoSystems.Axis.z != 0 && lockclimb == false && PipeActivate == true)
                {
                    endPipeDown();
                }

            }
        }
        
        private void UiActive(bool Active,bool past = false)
        {
            try
            {
                if (point != null && pointer != null)
                {
                    if (!past)
                    {
                        if (point == pointer.PointDown.gameObject)
                        {
                            print(pointer.DownUi);
                            pointer.DownUi.SetActive(Active);
                        }
                        else if (point == pointer.PointUp.gameObject)
                        {
                            pointer.UpUi.SetActive(Active);
                        }
                    }
                    else
                    {
                        pointer.UpUi.SetActive(Active);
                        pointer.DownUi.SetActive(Active);
                    }
                }
            }
            catch
            {
                print("Your UI not Setup");
            }
        }
        private void endPipeUp()
        {
            GoSystems.animatorControler.SetTrigger(Active);
            GoSystems.animatorControler.SetInteger("ClimbLivel", 2);
            lockclimb = true; ExitDown = false;
            CapsuleCollider cc = gameObject.GetComponent<CapsuleCollider>();
            cc.isTrigger = true;
            GoSystems.OnExit = new UnityEngine.Events.UnityEvent();
            GoSystems.OnExit.AddListener(UnLockSystems);
        }
        private void endPipeDown()
        {
            GoSystems.animatorControler.SetTrigger(Active);
            GoSystems.animatorControler.SetInteger("ClimbLivel", 4);
            lockclimb = true; ExitDown = true;
            GoSystems.OnExit = new UnityEngine.Events.UnityEvent();
            GoSystems.OnExit.AddListener(UnLockSystems);
        }
        private void Update()
        {
            if (PipeActivate == true)
            {
                getPlayPosition();
            }
        }
        private void getPlayPosition()
        {
            var dis = Vector3.Distance(transform.position, fixYpos);
            if (dis > 0.1f && lockPos == false)
            {
                transform.position = ladder.VTransition(transform.position, fixYpos, 6);
                transform.rotation = Quaternion.Lerp(transform.rotation,ladder.CheckDistins(pointer).rotation, 5 * Time.deltaTime);
            }
            else
            {
                lockPos = true;
                if (lockclimb == false)
                {
                    Iclimb();
                    GoSystems.animatorControler.speed = speed;
                }
            }
        }
        private void Iclimb()
        {
            var Pos = new Vector3(PointPositionTarget.transform.position.x + Offset.x, transform.position.y, PointPositionTarget.transform.position.z + Offset.z);
            var Rot = ladder.CheckDistins(pointer).rotation;
            transform.position = ladder.VTransition(transform.position, Pos, 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, Rot, 5 * Time.deltaTime);

            if (transform.position.y < pointer.PointDown.transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, pointer.PointDown.transform.position.y, transform.position.z);
            }
            if (transform.position.y > pointer.PointUp.transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, pointer.PointUp.transform.position.y, transform.position.z);
            }
        }
        void LockSystems()
        {
            try
            {
                PointPositionTarget =ladder.CheckDistins(pointer);
                Control.GoSystemsController.GoisKinematic(true);
                Control.GoSystemsController.GoUseGravety(false);
                onAction.Invoke();
                Exit = true;
                PipeActivate = true;
                Gs.IsRagdall = false;
                Gs.LockAllActions(true);
                lockclimb = false;
                lockPos = false;
                GoSystems.animatorControler.applyRootMotion = true;
                GoSystems.animatorControler.SetTrigger(Active);
            }
            catch
            {
                print(ladder.massage);
            }
        }
        void UnLockSystems()
        {
            try
            {
                Control.GoSystemsController.GoisKinematic(false);
                Control.GoSystemsController.GoUseGravety(true);
                ExitAction.Invoke();
                Exit = false;
                GoSystems.animatorControler.speed = 1;
                PipeActivate = false;
                lockclimb = true;
                lockPos = true;
                Gs.IsRagdall = true;
                lockcode = true;
                ExitDown = false;
                Gs.AxisCtrl = true;
                Gs.LockAllActions(false);
                GoSystems.animatorControler.applyRootMotion = false;
                CapsuleCollider cc = gameObject.GetComponent<CapsuleCollider>();
                cc.isTrigger = false;
                point = null;
                pointer = null;
            }
            catch
            {
                print(ladder.massage);
            }
        }
        public void OnMobilePipeClick()
        {
            StartCoroutine(PipeInputButtons.MobileInput(SetBool));
        }
        private void SetBool(bool status)
        {
            MobileInputSystem = status;
        }
        private void OnAnimatorMove()
        {
            if (Exit == true)
            {
                if (lockclimb == false)
                {
                    Vector3 delta = GoSystems.animatorControler.deltaPosition;
                    delta.x = 0f;
                    delta.z = 0f;

                    transform.position += delta;
                }
                if (lockclimb == true && ExitDown == false)
                {
                    Vector3 delta = GoSystems.animatorControler.deltaPosition;
                    transform.position += delta;

                }
                else if (lockclimb == true && ExitDown == true)
                {

                    Vector3 delta = GoSystems.animatorControler.deltaPosition;
                    delta.x = 0f;
                    delta.y = 0f;
                    transform.position += delta;
                }
            }
        }
    }
}