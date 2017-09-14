using UnityEngine;
using System;
using System.Collections;

namespace LDFW.Tween {

    public class LDFWTweenShader : LDFWTweenBaseFour
    {

        public string shaderVariableName;
        public Material targetMaterial;
        private ShaderVariableType shaderVariableType;



        public LDFWTweenBase Init(int fromValue, int toValue, Material mat, string shaderVariableName, float duration, float startDelay, 
            bool autoStart = false, Action endAction = null, 
            bool autoDestroyComponent = false, bool autoDestroyGameObject = false)
        {
            this.shaderVariableName = shaderVariableName;
            this.shaderVariableType = ShaderVariableType.INT;
            this.targetMaterial = mat;

            return base.Init(new Vector4(fromValue, 0, 0, 0), new Vector4(toValue, 0, 0, 0), duration, startDelay, autoStart, endAction, autoDestroyComponent, autoDestroyGameObject);
        }

        public LDFWTweenBase InitFloat(float fromValue, float toValue, Material mat, string shaderVariableName, float duration, float startDelay,
            bool autoStart = false, Action endAction = null,
            bool autoDestroyComponent = false, bool autoDestroyGameObject = false)
        {
            this.shaderVariableName = shaderVariableName;
            this.shaderVariableType = ShaderVariableType.FLOAT;
            this.targetMaterial = mat;

            return base.Init(new Vector4(fromValue, 0, 0, 0), new Vector4(toValue, 0, 0, 0), duration, startDelay, autoStart, endAction, autoDestroyComponent, autoDestroyGameObject);
        }

        public LDFWTweenBase InitColor(Color fromValue, Color toValue, Material mat, string shaderVariableName, float duration, float startDelay,
            bool autoStart = false, Action endAction = null, 
            bool autoDestroyComponent = false, bool autoDestroyGameObject = false)
        {
            this.shaderVariableName = shaderVariableName;
            this.shaderVariableType = ShaderVariableType.COLOR;
            this.targetMaterial = mat;

            return base.Init(fromValue, toValue, duration, startDelay, autoStart, endAction, autoDestroyComponent, autoDestroyGameObject);
        }

        public LDFWTweenBase InitVector4(Vector4 fromValue, Vector4 toValue, Material mat, string shaderVariableName, float duration, float startDelay,
            bool autoStart = false, Action endAction = null, 
            bool autoDestroyComponent = false, bool autoDestroyGameObject = false)
        {
            this.shaderVariableName = shaderVariableName;
            this.shaderVariableType = ShaderVariableType.VECTOR4;
            this.targetMaterial = mat;

            return base.Init(fromValue, toValue, duration, startDelay, autoStart, endAction, autoDestroyComponent, autoDestroyGameObject);
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