using UnityEngine;
using UnityEditor;

namespace LDFW.Tween
{

    [CanEditMultipleObjects]
    [CustomEditor(typeof(LDFWTweenCamera))]
    public class LDFWTweenCameraEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            LDFWTweenCamera myTarget = (LDFWTweenCamera)target;

            myTarget.targetCamera = (Camera)EditorGUILayout.ObjectField("Target Camera", myTarget.targetCamera, typeof(Camera), true);
            myTarget.autoPlay = EditorGUILayout.Toggle("Auto Play", myTarget.autoPlay);
            myTarget.autoDestroyComponent = EditorGUILayout.Toggle("Destroy Component", myTarget.autoDestroyComponent);
            myTarget.autoDestroyGameObject = EditorGUILayout.Toggle("Destroy GameObject", myTarget.autoDestroyGameObject);
            myTarget.ignoreTimeScale = EditorGUILayout.Toggle("Ignore System TimeScale", myTarget.ignoreTimeScale);

            myTarget.tweenStyle = (TweenStyle)EditorGUILayout.EnumPopup("Tween Style", myTarget.tweenStyle);
            myTarget.curveStyle = (CurveStyle)EditorGUILayout.EnumPopup("Curve Style", myTarget.curveStyle);

            myTarget.fromCamera = (Camera)EditorGUILayout.ObjectField("From Camera", myTarget.fromCamera, typeof(Camera), true);
            myTarget.toCamera = (Camera)EditorGUILayout.ObjectField("To Camera", myTarget.toCamera, typeof(Camera), true);

            myTarget.startDelay = EditorGUILayout.FloatField("Start Delay", myTarget.startDelay);
            myTarget.duration = EditorGUILayout.FloatField("Tween Duration", myTarget.duration);
            myTarget.targetTimeScale = EditorGUILayout.FloatField("Time Scale", myTarget.targetTimeScale);

            if (myTarget.curveStyle == CurveStyle.Custom)
            {
                if (myTarget.curveList == null || myTarget.curveList.Length != 12)
                {
                    myTarget.curveList = new AnimationCurve[12];

                    for (int i = 0; i < 12; i++)
                        myTarget.curveList[i] = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
                    
                }

                myTarget.curveList[0] = EditorGUILayout.CurveField("Field of View", myTarget.curveList[0]);
                myTarget.curveList[1] = EditorGUILayout.CurveField("Far clip plane", myTarget.curveList[1]);
                myTarget.curveList[2] = EditorGUILayout.CurveField("Near clip plane", myTarget.curveList[2]);
                myTarget.curveList[3] = EditorGUILayout.CurveField("Position X", myTarget.curveList[3]);
                myTarget.curveList[4] = EditorGUILayout.CurveField("Position Y", myTarget.curveList[4]);
                myTarget.curveList[5] = EditorGUILayout.CurveField("Posiiton Z", myTarget.curveList[5]);
                myTarget.curveList[6] = EditorGUILayout.CurveField("Rotation X", myTarget.curveList[6]);
                myTarget.curveList[7] = EditorGUILayout.CurveField("Rotation Y", myTarget.curveList[7]);
                myTarget.curveList[8] = EditorGUILayout.CurveField("Rotation Z", myTarget.curveList[8]);
                myTarget.curveList[9] = EditorGUILayout.CurveField("Scale X", myTarget.curveList[9]);
                myTarget.curveList[10] = EditorGUILayout.CurveField("Scale Y", myTarget.curveList[10]);
                myTarget.curveList[11] = EditorGUILayout.CurveField("Scale Z", myTarget.curveList[11]);

                myTarget.generateRandomCurveBasedOnFromAndTo = EditorGUILayout.Toggle("Use random curve", myTarget.generateRandomCurveBasedOnFromAndTo);
            }
        }
    }

}
