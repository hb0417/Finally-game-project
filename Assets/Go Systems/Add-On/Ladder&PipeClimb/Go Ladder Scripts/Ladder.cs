using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using GoSystem.Control;
namespace GoSystem
{
    [GBehaviourAttribute("Ladder Controller", true)]
    public class Ladder : GoSystemsBehaviour
    {
        public float speed = 1;
        public GoInput InputLsdderButtons;
        private GoInputSystem IsInput = new GoInputSystem();
        public float offsetUp = 0.5f, offsetDown = 0.2f;
       private bool LadderActivate, Exit, ExitDown ,MobileInputSystem;
        [HideInInspector] public string Livels = "ClimbLivel", Tag = "PipeClimb", climb = "LadderClimb", Active = "LadderActive",ladder = "Ladder",
        massage = "you have problem with your veargen";
       private GameObject point;
       private Animator animator;
       private LadderPoint pointer;
       private Transform POINT;
       public Vector3 Offset;
       private GoCameraSystem goCamera = new GoCameraSystem();
       private Vector3 fixYpos; GoSystems Gs;
       private bool lockcode, lockclimb, lockPos;

        public UnityEngine.Events.UnityEvent onAction, ExitAction;
        private void Start()
        {
            animator = GoSystems.animatorControler;
            Gs = GoSystems.getSystem(gameObject);
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.tag != Tag&&other.transform.root.GetComponent<LadderPoint>() != null && !LadderActivate)
            {
                pointer = other.transform.root.GetComponent<LadderPoint>();
                point = other.gameObject;
                UiActive(true);
                var getInput = IsInput.GetKeyDown(InputLsdderButtons.Kaybord.ToString(), InputLsdderButtons.Joystick.ToString(), MobileInputSystem);
                if (getInput)
                {
                    if (point == pointer.PointDown.gameObject)
                    {
                        GoSystems.animatorControler.SetInteger(Livels, 3);
                        fixYpos = point.transform.position;
                        fixYpos.y += offsetDown;
                        LockSystems();
                    }
                    else if (point == pointer.PointUp.gameObject)
                    {

                        fixYpos = point.transform.position;
                        fixYpos.y -= offsetUp;
                        LadderActivate = true;
                        GoSystems.animatorControler.SetInteger(Livels, 2);
                        LockSystems();
                    }
                }
            }
            else if(!LadderActivate)
            {
                UiActive(false,true);
                pointer = null;
                point = null;

            }

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != ladder)
                return;

            if (pointer != null && lockcode == false)
            {
                if (LadderActivate == true)
                {
                    // if out put from up Ladder
                    if (other.gameObject == pointer.PointUp.gameObject)
                    {
                        lockclimb = true;
                        animator.SetInteger(Livels, 0);
                        animator.SetTrigger(Active);
                        CapsuleCollider cc = gameObject.GetComponent<CapsuleCollider>();
                        cc.isTrigger = true;
                        GoSystems.OnExit = new UnityEngine.Events.UnityEvent();
                        GoSystems.OnExit.AddListener(UnLockSystems);
                        LadderActivate = false;
                    }
                    else if (other.gameObject == pointer.PointDown.gameObject &&lockcode == false)
                    {
                        DropDownCharacter();
                    }
                }
            }
        }
       private void DropDownCharacter()
        {
            animator.SetInteger(Livels, 1);
            animator.SetTrigger(Active);
            ExitDown = true;
            lockclimb = true;
            GoSystems.OnExit = new UnityEngine.Events.UnityEvent();
            GoSystems.OnExit.AddListener(UnLockSystems);
        }
        private void UiActive(bool Active, bool past = false)
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
        private void Update()
        {
            if (LadderActivate == true)
            {
                var dis = Vector3.Distance(transform.position, fixYpos);
                if (dis > 0.1f && lockPos == false)
                {
                    transform.position = VTransition(transform.position, fixYpos,6);
                    transform.rotation = Quaternion.Lerp(transform.rotation, CheckDistins(pointer).rotation, 5 * Time.deltaTime);
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
        }
       public Transform CheckDistins(LadderPoint point)
        {
            var Pointer = point;
            var pUp = Vector3.Distance(Pointer.PointUp.position, Control.GoSystemsController.Charcter.transform.position);
            var pDp = Vector3.Distance(Pointer.PointDown.position, Control.GoSystemsController.Charcter.transform.position);
            if (pUp > pDp)
            {
                return Pointer.PointDown.transform;
            }
            else
            {
                return Pointer.PointUp.transform;
            }
        }
        void Iclimb()
        {

            var Pos = new Vector3(POINT.transform.position.x + Offset.x, transform.position.y, POINT.transform.position.z + Offset.z);
            var Rot = CheckDistins(pointer).rotation;
            transform.position = VTransition(transform.position, Pos, 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, Rot, 5 * Time.deltaTime);

            if (GoSystems.Axis.z > 0)
            {
                animator.SetBool(climb, true);
                animator.SetInteger(Livels, 3);
                animator.speed = 1;
            }
            if (GoSystems.Axis.z < 0)
            {
                animator.SetInteger(Livels,4);
                animator.SetBool(climb, true);
            }
            if (GoSystems.Axis.z == 0)
            {
                animator.SetBool(climb, false);
            }

            if (transform.position.y < pointer.PointDown.transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, pointer.PointDown.transform.position.y, transform.position.z);
            }
            if(transform.position.y > pointer.PointUp.transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, pointer.PointUp.transform.position.y, transform.position.z);
            }
        }
        void LockSystems()
        {
            try
            {
                POINT = CheckDistins(pointer);
                Control.GoSystemsController.GoisKinematic(true);
                Control.GoSystemsController.GoUseGravety(false);
                onAction.Invoke();
                Exit = true;
                Gs.IsRagdall = false;
                LadderActivate = true;
                lockcode = true;
                Gs.IsLockMove = true;
                Gs.IsLockJump = true;
                lockclimb = false;
                StartCoroutine(lockOut());
                GoSystems.IsFoods = false;
                GoSystems.IsLags = false;
                animator.applyRootMotion = true;
                animator.SetTrigger(Active);
            }
            catch
            {
                print(massage);
            }
        }
        void UnLockSystems()
        {
            try
            {
                Control.GoSystemsController.GoisKinematic(false);
                Control.GoSystemsController.GoUseGravety(true);
                UiActive(false, true);
                ExitAction.Invoke();
                Exit = false;
                GoSystems.animatorControler.speed = 1;
                LadderActivate = false;
                lockclimb = false;
                lockPos = false;
                Gs.IsLockMove = false;
                Gs.IsLockJump = false;
                Gs.IsRagdall = true;
                ExitDown = false;
                GoSystems.IsFoods = true;
                GoSystems.IsLags = true;
                animator.applyRootMotion = false;
                CapsuleCollider cc = gameObject.GetComponent<CapsuleCollider>();
                cc.isTrigger = false;
                point = null;
                pointer = null;
            }
            catch
            {
                print(massage);
            }
        }
        IEnumerator lockOut()
        {
            yield return new WaitForSeconds(1);
            lockcode = false;
            StopCoroutine(lockOut());
        }
        private void OnAnimatorMove()
        {
            if (Exit == true)
            {
                if (lockclimb == false)
                {
                    Vector3 delta = animator.deltaPosition;
                    delta.x = 0f;
                    delta.z = 0f;
                    transform.position += delta;
                }
                if (lockclimb == true && ExitDown == false)
                {
                    Vector3 delta = animator.deltaPosition;
                    transform.position += delta;
                }
                else if ( lockclimb == true && ExitDown == true)
                {
                    Vector3 delta = animator.deltaPosition;
                    delta.x = 0f;
                    delta.y = 0f;
                    transform.position += delta;
                }
            }
        }
        public void OnMobileLadderClick()
        {
            StartCoroutine(InputLsdderButtons.MobileInput(SetBool));
        }
        private void SetBool(bool status)
        {
            MobileInputSystem = status;
        }
        public Vector3 VTransition(Vector3 start, Vector3 end, float t)
        {
            var m = t *Time.deltaTime;
            Vector3 transitionedValue = Vector3.zero;
            transitionedValue.x = start.x + (end.x - start.x) * m;
            transitionedValue.y = start.y + (end.y - start.y) * m;
            transitionedValue.z = start.z + (end.z - start.z) * m;
            return transitionedValue;
        }
    }
}