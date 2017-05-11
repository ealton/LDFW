using UnityEngine;
using System.Collections;

namespace LDFW.Tween {
    /*
    public class LDFWTweenUV : LDFWTweenBase {


        private Material material;

        new void Awake () {
            base.Awake ();
            material = targetTransform.GetComponent<MeshRenderer> ().material;
        }

        protected override void PreStart () {
            if (useCurrentValueAsStartingValue) {
                fromValue.x = material.GetTextureScale ("_MainTex").x;
                fromValue.y = material.GetTextureScale ("_MainTex").y;
            }
        }

        new void Start () {
            base.Start ();
            useRelativeValueBasedOnStartingValue = false;
        }

        new void Update () {
            if (!isTweenerPlaying) {
                return;
            }
            base.Update ();

            if (accumulatedTime > startDelay) {

                material.SetTextureScale ("_MainTex", new Vector2 (currentValue.x, currentValue.y));
                material.SetTextureOffset ("_MainTex", new Vector2 (-(currentValue.x - 1) * 0.5f, -(currentValue.y - 1) * 0.5f));

            }
        }

    }
    */
}