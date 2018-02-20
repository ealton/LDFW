using UnityEngine;
using System.Collections;

namespace LDFW.Tween
{

    public partial class LDFWTweenManager : MonoBehaviour
    {

        private static LDFWTweenManager _instance;
        public static LDFWTweenManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    CreateInstance();
                }

                return _instance;
            }
        }
        public LDFWTweenUpdater tweenUpdater;












        private static void CreateInstance()
        {

            if (_instance != null)
            {
                DestroyImmediate(_instance.gameObject);
            }

            var go = new GameObject("LDFWTweenManager");
            DontDestroyOnLoad(go);

            _instance = go.AddComponent<LDFWTweenManager>();
            _instance.tweenUpdater = go.AddComponent<LDFWTweenUpdater>();
        }



    }

}
