using UnityEngine;
using UnityEditor;

namespace LDFW.Model
{

    [CustomEditor (typeof (RollPlane))]
    public class RollPlaneEditor : Editor
    {




        public override void OnInspectorGUI ()
        {
            RollPlane rollPlane = (RollPlane) target;

            rollPlane.bendAngles = EditorGUILayout.Vector3Field ("Rolling Angle Flags", rollPlane.bendAngles);
            rollPlane.startingPosition = EditorGUILayout.Vector3Field ("Rolling start position [-1 1]", rollPlane.startingPosition);
            rollPlane.initialRadius = EditorGUILayout.FloatField ("Outer rolling radius", rollPlane.initialRadius);
            rollPlane.radiusDecreasingRate = EditorGUILayout.Vector3Field ("Radius decreasing rate", rollPlane.radiusDecreasingRate);

            if (GUILayout.Button("Recalculate"))
            {
                rollPlane.Recalculate ();
            }
        }
    }

}