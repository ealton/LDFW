using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LDFW.Tween
{

    [CustomEditor(typeof(LDFWTweenUpdater))]
    public class LDFWTweenUpdaterEditor : Editor
    {

        public override void OnInspectorGUI()
        {

            var tweenList = (target as LDFWTweenUpdater).tweenList;
            GUILayout.Label("Total tween count: " + tweenList.Count);

            for (int i = 0; i < tweenList.Count; i++)
            {
                var tween = tweenList[i];

                if (tween == null)
                {
                    GUILayout.Label("NULL");
                }
                else
                {
                    GUILayout.Label("ID = " + tween.tweenID + ", Target = " + tween.target.name);
                    GUILayout.Label("F = " + tween.GetFromValueString());
                    GUILayout.Label("T = " + tween.GetToValueString());
                    GUILayout.Label("C = " + tween.GetCurrentValueString());
                    GUILayout.Label("T = " + tween.tweenTime.ToString("0.00") + " / " + tween.tweenDuration.ToString("0.00"));
                    GUILayout.Label(tween.isTweenPlaying ? "Is Playing" : "Is Not Playing");
                    GUILayout.Label(tween.isTweenBackwards ? "Pong" : "Ping");
                    GUILayout.Label("".Concatenate(
                        "A:", tween.autoPlay.ToString().Substring(0, 1), " ",
                        "TS:", tween.ignoreTimeScale.ToString().Substring(0, 1), " ",
                        "R:", tween.removeUponCompletion.ToString().Substring(0, 1), " ",
                        "D:", tween.destroyTargetUponCompletion.ToString().Substring(0, 1)));
                }

                GUILayout.Space(10);
            }

        }

    }

}
