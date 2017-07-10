using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace LDFW.Tween
{

    public struct LDFWTweenEmptyEvent{
        public float targetPercentage;
        public Action eventAction;

        public LDFWTweenEmptyEvent(float percent, Action action)
        {
            targetPercentage = percent;
            eventAction = action;
        }
    }

    public class LDFWTweenEmpty : LDFWTweenBaseOne
    {

        public List<LDFWTweenEmptyEvent> targetEventList;


        public LDFWTweenBase Init(float time, float delay, LDFWTweenEmptyEvent[] eventList, bool autoPlay = false)
        {
            SetTargetEventList(eventList);
            return base.Init(0, 0, time, delay, autoPlay);
        }

        public LDFWTweenEmpty SetTargetEventList(List<LDFWTweenEmptyEvent> eventList)
        {
            targetEventList = eventList;
            PreStart();

            return this;
        }

        public LDFWTweenEmpty SetTargetEventList(LDFWTweenEmptyEvent[] eventList)
        {
            targetEventList = new List<LDFWTweenEmptyEvent>();
            foreach (var tweenEvent in eventList)
                targetEventList.Add(tweenEvent);

            PreStart();

            return this;
        }

        public LDFWTweenEmpty AddEvent(LDFWTweenEmptyEvent tweenEvent)
        {
            targetEventList.Add(tweenEvent);
            PreStart();

            return this;
        }

        protected override void PostCurrentValueCalculation()
        {
            float currentProgress = GetCurrentPercentage();
            while (targetEventList != null && targetEventList.Count > 0 && currentProgress > targetEventList[0].targetPercentage)
            {
                targetEventList[0].eventAction();
                targetEventList.RemoveAt(0);
            }
        }

        protected override void PreStart()
        {
            if (targetEventList == null)
            {
                targetEventList = new List<LDFWTweenEmptyEvent>();
            }
            else
            {
                targetEventList = DnVSort(targetEventList);

                /*
                string targetPercentageList = "";
                foreach (var emptyEvent in targetEventList)
                    targetPercentageList += " " + emptyEvent.targetPercentage;

                Debug.Log("Sorted target percentage list = " + targetPercentageList);
                */
            }
        }

        private List<LDFWTweenEmptyEvent> DnVSort(List<LDFWTweenEmptyEvent> list)
        {

            if (list == null || list.Count == 0 || list.Count == 1)
            {
                return list;
            }
            else if (list.Count == 2)
            {
                if (list[0].targetPercentage > list[1].targetPercentage)
                {
                    LDFWTweenEmptyEvent temp = list[0];
                    list[0] = list[1];
                    list[1] = temp;
                }
                return list;
            }

            // List.count > 2
            List<LDFWTweenEmptyEvent> leftList = new List<LDFWTweenEmptyEvent>();
            List<LDFWTweenEmptyEvent> rightList = new List<LDFWTweenEmptyEvent>();
            List<LDFWTweenEmptyEvent> middleList = new List<LDFWTweenEmptyEvent>();

            float pivot = list[list.Count / 2].targetPercentage;
            
            foreach (var emptyEvent in list)
            {
                if (emptyEvent.targetPercentage < pivot)
                {
                    leftList.Add(emptyEvent);
                }
                else if (emptyEvent.targetPercentage > pivot)
                {
                    rightList.Add(emptyEvent);
                }
                else
                {
                    middleList.Add(emptyEvent);
                }
            }

            leftList = DnVSort(leftList);
            rightList = DnVSort(rightList);

            foreach (var emptyEvent in middleList)
            {
                leftList.Add(emptyEvent);
            }
            foreach (var emptyEvent in rightList)
            {
                leftList.Add(emptyEvent);
            }

            return leftList;
        }

    }

}