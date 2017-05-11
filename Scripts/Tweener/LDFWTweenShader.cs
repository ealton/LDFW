using UnityEngine;
using System;
using System.Collections;

namespace LDFW.Tween {

    public class LDFWTweenShader : LDFWTweenBase {

        public string shaderVariableName;
        public Material targetMaterial;
        private ShaderVariableType shaderVariableType;

        protected override void PreStart()
        {
            if (targetTransform != null)
            {
                targetMaterial = targetTransform.GetComponent<Renderer>().material;
            }
        }

        public new LDFWTweenBase Init(int fromValue, int toValue, string shaderVariableName, float duration, float startDelay, Action endAction = null, 
            bool autoStart = false, bool autoDestroyComponent = false, bool autoDestroyGameObject = false)
        {
            this.shaderVariableName = shaderVariableName;
            this.shaderVariableType = ShaderVariableType.INT;

            return base.Init(new float[] { fromValue }, new float[] { toValue }, 
                duration, startDelay, endAction, autoStart, autoDestroyComponent, autoDestroyGameObject);
        }

        public new LDFWTweenBase InitFloat(float fromValue, float toValue, string shaderVariableName, float duration, float startDelay, Action endAction = null, 
            bool autoStart = false, bool autoDestroyComponent = false, bool autoDestroyGameObject = false)
        {
            this.shaderVariableName = shaderVariableName;
            this.shaderVariableType = ShaderVariableType.FLOAT;

            return base.Init(new float[] { fromValue }, new float[] { toValue }, 
                duration, startDelay, endAction, autoStart, autoDestroyComponent, autoDestroyGameObject);
        }

        public LDFWTweenBase InitColor(Color fromValue, Color toValue, string shaderVariableName, float duration, float startDelay, Action endAction = null, 
            bool autoStart = false, bool autoDestroyComponent = false, bool autoDestroyGameObject = false)
        {
            this.shaderVariableName = shaderVariableName;
            this.shaderVariableType = ShaderVariableType.COLOR;

            return base.Init(new float[] { fromValue.r, fromValue.g, fromValue.b, fromValue.a }, 
                new float[] { toValue.r, toValue.g, toValue.b, toValue.a }, 
                duration, startDelay, endAction, autoStart, autoDestroyComponent, autoDestroyGameObject);
        }

        public LDFWTweenBase InitVector4(Vector4 fromValue, Vector4 toValue, string shaderVariableName, float duration, float startDelay, Action endAction = null, 
            bool autoStart = false, bool autoDestroyComponent = false, bool autoDestroyGameObject = false)
        {
            this.shaderVariableName = shaderVariableName;
            this.shaderVariableType = ShaderVariableType.VECTOR4;

            return base.Init(new float[] { fromValue.x, fromValue.y, fromValue.z, fromValue.w }, 
                new float[] { toValue.x, toValue.y, toValue.z, toValue.w }, 
                duration, startDelay, endAction, autoStart, autoDestroyComponent, autoDestroyGameObject);
        }

        protected override void PostCurrentValueCalculation()
        {
            switch (shaderVariableType)
            {
                case ShaderVariableType.INT:
                    targetMaterial.SetInt(shaderVariableName, (int)currentValue[0]);
                    break;
                case ShaderVariableType.FLOAT:
                    targetMaterial.SetFloat(shaderVariableName, currentValue[0]);
                    break;
                case ShaderVariableType.COLOR:
                    targetMaterial.SetColor(shaderVariableName, new Color(currentValue[0], currentValue[1], currentValue[2], currentValue[3]));
                    break;
                case ShaderVariableType.VECTOR4:
                    targetMaterial.SetVector(shaderVariableName, new Vector4(currentValue[0], currentValue[1], currentValue[2], currentValue[3]));
                    break;
            }

        }

        private enum ShaderVariableType {
            FLOAT,
            INT,
            COLOR,
            VECTOR4
        }
    }

}