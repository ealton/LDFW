using UnityEngine;
using System.Collections;

namespace LDFW.Tween {

    
    public class LDFWTweenUV : LDFWTweenBaseTwo {


        private Material material;
        private string textureName = "_MainTex";

        public LDFWTweenBase SetTextureName(string name)
        {
            textureName = name;
            return this;
        }

        protected override void PreStart () {
            if (useCurrentValueAsStartingValue) {
                Vector2 textureScale = material.GetTextureScale("_MainTex");
                startingValue[0] = textureScale.x;
                startingValue[1] = textureScale.y;
            }
        }

        protected override void PostCurrentValueCalculation()
        {
            material.SetTextureScale("_MainTex", new Vector2(currentValue[0], currentValue[1]));
            material.SetTextureOffset("_MainTex", new Vector2(-(currentValue[0] - 1) * 0.5f, -(currentValue[1] - 1) * 0.5f));
        }
        
    }
    
}