using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LDFW.Tween
{


    public class LDFWTweenUpdater : MonoBehaviour
    {

        [SerializeField]
        public List<LDFWTweenBase> tweenList;
        public List<LDFWTweenBase> removableList;
        private int nextTweenID = 1;

        private void Awake()
        {
            tweenList = new List<LDFWTweenBase>();
            removableList = new List<LDFWTweenBase>();
        }

        private void Update()
        {
            if (tweenList != null)
            {
                foreach (var tween in tweenList)
                {
                    if (tween != null)
                        tween.Update();

                }
            }

            //ClearUpRemovableList();
        }

        public void AddTween(LDFWTweenBase tween)
        {
            tweenList.Add(tween);
        }

        public void RemoveTween(LDFWTweenBase tween)
        {
            removableList.Add(tween);
        }

        public int GetTweenID()
        {
            return nextTweenID++;
        }

        public void ClearUpRemovableList()
        {
            foreach (var tween in removableList)
            {
                if (tween != null && tweenList.Contains(tween))
                    tweenList.Remove(tween);
            }

            while (removableList.Count > 0)
                removableList.RemoveAt(0);
        }


    }

}
