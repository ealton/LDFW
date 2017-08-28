using UnityEditor;
using UnityEngine;

namespace LDFW.Tween
{
    
    [CanEditMultipleObjects]
    [CustomEditor(typeof(LDFWTweenBaseThree))]
    public class LDFWTweenBaseThreeEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            LDFWTweenBaseThree myTarget = (LDFWTweenBaseThree)target;

            //base.OnInspectorGUI();
            myTarget.targetTransform = (Transform) EditorGUILayout.ObjectField ("Target", myTarget.targetTransform, typeof(Transform), true);
            myTarget.autoPlay = EditorGUILayout.Toggle("Auto Play", myTarget.autoPlay);
            myTarget.autoDestroyComponent = EditorGUILayout.Toggle("Destroy Component", myTarget.autoDestroyComponent);
            myTarget.autoDestroyGameObject = EditorGUILayout.Toggle("Destroy GameObject", myTarget.autoDestroyGameObject);
            myTarget.ignoreTimeScale = EditorGUILayout.Toggle("Ignore System TimeScale", myTarget.ignoreTimeScale);

            myTarget.tweenStyle = (TweenStyle)EditorGUILayout.EnumPopup("Tween Style", myTarget.tweenStyle);
            myTarget.curveStyle = (CurveStyle)EditorGUILayout.EnumPopup("Curve Style", myTarget.curveStyle);

            myTarget.fromValueVec = EditorGUILayout.Vector3Field("From Value", myTarget.fromValueVec);
            myTarget.toValueVec = EditorGUILayout.Vector3Field("To Value", myTarget.toValueVec);

            myTarget.startDelay = EditorGUILayout.FloatField("Start Delay", myTarget.startDelay);
            myTarget.duration = EditorGUILayout.FloatField("Tween Duration", myTarget.duration);
            myTarget.targetTimeScale = EditorGUILayout.FloatField("Time Scale", myTarget.targetTimeScale);

            if (myTarget.curveStyle == CurveStyle.Custom)
            {
                if (myTarget.curveList == null || myTarget.curveList.Length != 3)
                {
                    myTarget.curveList = new AnimationCurve[3];
                    myTarget.curveList[0] = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
                    myTarget.curveList[1] = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
                    myTarget.curveList[2] = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
                }

                myTarget.curveList[0] = EditorGUILayout.CurveField("X Curve", myTarget.curveList[0]);
                myTarget.curveList[1] = EditorGUILayout.CurveField("Y Curve", myTarget.curveList[1]);
                myTarget.curveList[2] = EditorGUILayout.CurveField("Z Curve", myTarget.curveList[2]);
                
                myTarget.generateRandomCurveBasedOnFromAndTo = EditorGUILayout.Toggle("Use random curve", myTarget.generateRandomCurveBasedOnFromAndTo);
            }
        }
    }

    [CustomEditor(typeof(LDFWTweenPosition))]
    public class LDFWTweenPositionEditor : LDFWTweenBaseThreeEditor
    {
    }

    [CustomEditor(typeof(LDFWTweenScale))]
    public class LDFWTweenScaleEditor : LDFWTweenBaseThreeEditor
    {
    }

    [CustomEditor(typeof(LDFWTweenRotation))]
    public class LDFWTweenRotationEditor : LDFWTweenBaseThreeEditor
    {
    }

    [CustomEditor(typeof(LDFWTweenWorldPosition))]
    public class LDFWTweenWorldPositionEditor : LDFWTweenBaseThreeEditor
    {
    }

    [CustomEditor(typeof(LDFWTweenWorldRotation))]
    public class LDFWTweenWorldRotationEditor : LDFWTweenBaseThreeEditor
    {
    }

}