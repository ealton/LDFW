using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace LDFW.Tween
{

    public class LDFWTweenEmptyEvent
    {
        public float targetPercentage = 0;
        public Action eventAction = null;

        public LDFWTweenEmptyEvent(float percent, Action action)
        {
            targetPercentage = percent;
            eventAction = action;
        }
    }

    public class LDFWTweenEvents : LDFWTweenBase
    {

        private List<LDFWTweenEmptyEvent> targetEventList;


        public LDFWTweenEvents Init(float time, float delay, LDFWTweenEmptyEvent[] eventList, bool autoPlay = false)
        {
            SetTargetEventList(eventList);
            base.Init(0, 0, time, delay);
            return this;
        }


        public LDFWTweenEvents SetTargetEventList(List<LDFWTweenEmptyEvent> eventList)
        {
            targetEventList = eventList;
            return this;
        }


        public LDFWTweenEvents SetTargetEventList(LDFWTweenEmptyEvent[] eventList)
        {
            targetEventList = new List<LDFWTweenEmptyEvent>();
            foreach (var tweenEvent in eventList)
                targetEventList.Add(tweenEvent);
            return this;
        }


        public LDFWTweenEvents AddEvent(LDFWTweenEmptyEvent tweenEvent)
        {
            targetEventList.Add(tweenEvent);
            return this;
        }

        protected override void PostCurrentValueCalculation()
        {
            var currentProgress = tweenTime / tweenDuration;
            while (targetEventList != null && targetEventList.Count > 0 &&
                   currentProgress > targetEventList[0].targetPercentage)
            {
                var action = targetEventList[0].eventAction;
                targetEventList.RemoveAt(0);

                if (action != null)
                    action();

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

            // at this point, (List.count > 2) is true
            var leftList = new List<LDFWTweenEmptyEvent>();
            var rightList = new List<LDFWTweenEmptyEvent>();
            var middleList = new List<LDFWTweenEmptyEvent>();

            float pivot = list[list.Count / 2].targetPercentage;

            foreach (var emptyEvent in list)
            {
                if (emptyEvent.targetPercentage < pivot)
                    leftList.Add(emptyEvent);
                else if (emptyEvent.targetPercentage > pivot)
                    rightList.Add(emptyEvent);
                else
                    middleList.Add(emptyEvent);
            }

            leftList = DnVSort(leftList);
            rightList = DnVSort(rightList);

            foreach (var emptyEvent in middleList)
                leftList.Add(emptyEvent);

            foreach (var emptyEvent in rightList)
                leftList.Add(emptyEvent);

            return leftList;
        }

    }

}