using UnityEditor;
using UnityEngine;

namespace LDFW.Tween
{

    [CanEditMultipleObjects]
    [CustomEditor(typeof(LDFWTweenBaseTwo))]
    public class LDFWTweenBaseTwoEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            LDFWTweenBaseTwo myTarget = (LDFWTweenBaseTwo)target;

            //base.OnInspectorGUI();
            myTarget.targetTransform = (Transform)EditorGUILayout.ObjectField("Target", myTarget.targetTransform, typeof(Transform), true);
            myTarget.autoPlay = EditorGUILayout.Toggle("Auto Play", myTarget.autoPlay);
            myTarget.autoDestroyComponent = EditorGUILayout.Toggle("Destroy Component", myTarget.autoDestroyComponent);
            myTarget.autoDestroyGameObject = EditorGUILayout.Toggle("Destroy GameObject", myTarget.autoDestroyGameObject);
            myTarget.ignoreTimeScale = EditorGUILayout.Toggle("Ignore System TimeScale", myTarget.ignoreTimeScale);

            myTarget.tweenStyle = (TweenStyle)EditorGUILayout.EnumPopup("Tween Style", myTarget.tweenStyle);
            myTarget.curveStyle = (CurveStyle)EditorGUILayout.EnumPopup("Curve Style", myTarget.curveStyle);

            myTarget.fromValueVec = EditorGUILayout.Vector2Field("From Value", myTarget.fromValueVec);
            myTarget.toValueVec = EditorGUILayout.Vector2Field("To Value", myTarget.toValueVec);

            myTarget.startDelay = EditorGUILayout.FloatField("Start Delay", myTarget.startDelay);
            myTarget.duration = EditorGUILayout.FloatField("Tween Duration", myTarget.duration);
            myTarget.targetTimeScale = EditorGUILayout.FloatField("Time Scale", myTarget.targetTimeScale);

            if (myTarget.curveStyle == CurveStyle.Custom)
            {
                if (myTarget.curveList == null || myTarget.curveList.Length != 3)
                {
                    myTarget.curveList = new AnimationCurve[2];
                    myTarget.curveList[0] = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
                    myTarget.curveList[1] = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
                }

                myTarget.curveList[0] = EditorGUILayout.CurveField("X Curve", myTarget.curveList[0]);
                myTarget.curveList[1] = EditorGUILayout.CurveField("Y Curve", myTarget.curveList[1]);

                myTarget.generateRandomCurveBasedOnFromAndTo = EditorGUILayout.Toggle("Use random curve", myTarget.generateRandomCurveBasedOnFromAndTo);
            }
        }
    }

    [CustomEditor(typeof(LDFWTweenUIAnchoredPosition))]
    public class LDFWTweenUIAnchoredPositionEditor : LDFWTweenBaseTwoEditor
    {
    }

    [CustomEditor(typeof(LDFWTweenUV))]
    public class LDFWTweenUVEditor : LDFWTweenBaseTwoEditor
    {
    }
    

}