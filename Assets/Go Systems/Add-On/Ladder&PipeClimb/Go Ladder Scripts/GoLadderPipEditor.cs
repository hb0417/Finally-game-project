using UnityEditor;
using UnityEngine;

namespace GoSystem
{
    namespace editor
    {
        public class GoLadderPipEditor
        {
            public class GoLadderEditor : EditorWindow
            {
                string Label;
                string Massage = "Put your Character ";
                Vector2 rect = new Vector2(500, 250);

                Object Charcter;
                Object LiftHand;
                Object RightHand;

                Editor humanoidpreview;

                public Texture2D logo;
                static GUISkin _mySkin;


                [MenuItem("Tools/Go Systems/Actions/LadderAndPipe", false, 0)]
                public static void ShowWindow()
                {
                    GetWindow<GoLadderEditor>("Ladder And Pipe System(Add-On)");
                }
                public virtual void OnGUI()
                {
                    this.minSize = rect;
                    _mySkin = GoSystemsEditor.minue.MySkin;
                    GUILayout.BeginVertical(_mySkin.window);
                    GUILayout.Label("Ladder System", _mySkin.label);

                    EditorGUILayout.Space(15);
                    if (humanoidpreview != null)
                    {
                        humanoidpreview.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(rect.x, rect.y), _mySkin.window);
                    }
                    //controler
                    Charcter = EditorGUILayout.ObjectField("Charcter", Charcter, typeof(GameObject), true, GUILayout.ExpandWidth(true)) as GameObject;
                    LiftHand = EditorGUILayout.ObjectField("Left hand", LiftHand, typeof(Transform), true, GUILayout.ExpandWidth(true)) as Transform;
                    RightHand = EditorGUILayout.ObjectField("Right hand", RightHand, typeof(Transform), true, GUILayout.ExpandWidth(true)) as Transform;

                    if (Charcter != null)
                    {
                        GameObject @char = Charcter as GameObject;
                        LiftHand = @char.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftHand).transform;
                        RightHand = @char.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightHand).transform;
                    }
                    if (GUILayout.Button("Create", _mySkin.button))
                    {


                        if (Charcter != null)
                        {
                            GameObject @char = Charcter as GameObject;
                            if (@char.GetComponent<Ladder>() == null&& @char.GetComponent<PipClimb>() == null)
                            {
                                Ladder CRC = @char.AddComponent<Ladder>();
                                PipClimb Pc = @char.AddComponent<PipClimb>();
                                CapsuleCollider CCT = @char.GetComponent<CapsuleCollider>();
                                var size = CCT.radius;
                                var Hight = CCT.height;
                                var Pos = CCT.center;
                                CapsuleCollider CCA = @char.AddComponent<CapsuleCollider>();
                                CCA.height = Hight;
                                CCA.radius = size;
                                CCA.center = Pos;
                                CCA.isTrigger = true;

                                this.Close();
                            }
                            else
                            {
                                Massage = "You alrady have Ladder And Pipe System system";
                            }
                        }
                        else
                        {
                            Massage = "Charcter can not be Empty";
                        }


                    }

                    EditorGUILayout.Space(10);
                    EditorGUILayout.HelpBox(Massage, MessageType.Info);
                    GUILayout.FlexibleSpace();
                    GUILayout.EndVertical();




                }


            }
         
        }
    }
}
