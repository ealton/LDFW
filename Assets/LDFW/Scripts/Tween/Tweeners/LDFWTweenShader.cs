using UnityEngine;
using System;
using System.Collections;

namespace LDFW.Tween
{

    public class LDFWTweenShader : LDFWTweenBase
    {

        public string shaderVariableName;
        private ShaderVariableType shaderVariableType;



        public LDFWTweenBase InitInt(
            Material mat, string shaderVariableName,
            int fromValue, int toValue, float duration, float delay)
        {
            this.shaderVariableName = shaderVariableName;
            this.shaderVariableType = ShaderVariableType.INT;
            this.target = mat;

            return Init(fromValue, toValue, duration, delay);
        }

        public LDFWTweenBase InitFloat(
            Material mat, string shaderVariableName,
            float fromValue, float toValue, float duration, float delay)
        {
            this.shaderVariableName = shaderVariableName;
            this.shaderVariableType = ShaderVariableType.FLOAT;
            this.target = mat;

            return Init(fromValue, toValue, duration, delay);
        }

        public LDFWTweenBase InitColor(
            Material mat, string shaderVariableName,
            Color fromColor, Color toColor, float duration, float delay)
        {
            this.shaderVariableName = shaderVariableName;
            this.shaderVariableType = ShaderVariableType.COLOR;
            this.target = mat;

            return Init(
                new float[] { fromColor.r, fromColor.g, fromColor.b, fromColor.a },
                new float[] { toColor.r, toColor.g, toColor.b, toColor.a },
                duration, delay);
        }

        public LDFWTweenBase InitVector4(
            Material mat, string shaderVariableName,
            Vector4 fromValue, Vector4 toValue, float duration, float delay)
        {
            this.shaderVariableName = shaderVariableName;
            this.shaderVariableType = ShaderVariableType.COLOR;
            this.target = mat;

            return Init(fromValue, toValue, duration, delay);
        }

        public LDFWTweenBase InitTextureUV(
            Material mat, string shaderVariableName,
            Vector2 fromScale, Vector2 toScale, Vector2 fromOffset, Vector2 toOffset,
            float duration, float delay)
        {
            this.shaderVariableName = shaderVariableName;
            this.shaderVariableType = ShaderVariableType.COLOR;
            this.target = mat;

            return Init(
                new float[] { fromScale.x, fromScale.y, fromOffset.x, fromOffset.y },
                new float[] { toScale.x, toScale.y, toOffset.x, toOffset.y },
                duration, delay);
        }

        protected override void PostCurrentValueCalculation()
        {
            switch (shaderVariableType)
            {
                case ShaderVariableType.INT:
                    (target as Material).SetInt(shaderVariableName, (int)currentValue[0]);
                    break;
                case ShaderVariableType.FLOAT:
                    (target as Material).SetFloat(shaderVariableName, currentValue[0]);
                    break;
                case ShaderVariableType.COLOR:
                    (target as Material).SetColor(shaderVariableName, new Color(currentValue[0], currentValue[1], currentValue[2], currentValue[3]));
                    break;
                case ShaderVariableType.VECTOR4:
                    (target as Material).SetVector(shaderVariableName, new Vector4(currentValue[0], currentValue[1], currentValue[2], currentValue[3]));
                    break;

                case ShaderVariableType.TextureUV:
                    (target as Material).SetTextureScale(
                        shaderVariableName, new Vector2(currentValue[0], currentValue[1]));
                    (target as Material).SetTextureOffset(
                        shaderVariableName, new Vector2(currentValue[2], currentValue[3]));
                    break;

            }

        }

        private enum ShaderVariableType
        {
            FLOAT,
            INT,
            COLOR,
            VECTOR4,

            TextureUV
        }


    }

}