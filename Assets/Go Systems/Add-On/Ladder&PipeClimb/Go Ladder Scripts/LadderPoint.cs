using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GoSystem
{
    [GBehaviourAttribute("Ladder Pointer", false)]
    public class LadderPoint : GoSystemsBehaviour
    {

        public Transform PointUp, PointDown;
        [Header("UI")]
        public GameObject UpUi;
        public GameObject DownUi;

        private void OnDrawGizmos()
        {
            if (PointDown != null && PointUp != null)
            {
                DrowMithods.GoDrow(PointDown.transform.gameObject.GetComponent<Collider>().bounds.center, PointDown.transform.gameObject.GetComponent<Collider>().bounds.size);
                DrowMithods.GoDrow(PointUp.transform.gameObject.GetComponent<Collider>().bounds.center, PointUp.transform.gameObject.GetComponent<Collider>().bounds.size);
            }
        }
        private void Awake()
        {
            try
            {
               // var up = PointUp.gameObject.AddComponent<LadderPoint>();
                //var down = PointDown.gameObject.AddComponent<LadderPoint>();
           //     up.PointUp = PointUp;
               // up.PointDown = PointDown;
              //  down.PointDown = PointDown;
              //  down.PointUp = PointUp;
            }
            catch
            {

            }
            
            
            }
    }
}