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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="percent"></param>
        /// <param name="action"></param>
        public LDFWTweenEmptyEvent(float percent, Action action)
        {
            targetPercentage = percent;
            eventAction = action;
        }
    }

    public class LDFWTweenEmpty : LDFWTweenBaseOne
    {

        private List<LDFWTweenEmptyEvent> targetEventList;

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="time"></param>
        /// <param name="delay"></param>
        /// <param name="eventList"></param>
        /// <param name="autoPlay"></param>
        /// <returns></returns>
        public LDFWTweenBase Init(float time, float delay, LDFWTweenEmptyEvent[] eventList, bool autoPlay = false)
        {
            SetTargetEventList(eventList);
            return base.Init(0, 0, time, delay, autoPlay);
        }

        /// <summary>
        /// Set target event list
        /// </summary>
        /// <param name="eventList"></param>
        /// <returns></returns>
        public LDFWTweenEmpty SetTargetEventList(List<LDFWTweenEmptyEvent> eventList)
        {
            targetEventList = eventList;
            PreStart();

            return this;
        }

        /// <summary>
        /// Set target event list
        /// </summary>
        /// <param name="eventList"></param>
        /// <returns></returns>
        public LDFWTweenEmpty SetTargetEventList(LDFWTweenEmptyEvent[] eventList)
        {
            targetEventList = new List<LDFWTweenEmptyEvent>();
            foreach (var tweenEvent in eventList)
                targetEventList.Add(tweenEvent);

            PreStart();

            return this;
        }

        /// <summary>
        /// Add event
        /// </summary>
        /// <param name="tweenEvent"></param>
        /// <returns></returns>
        public LDFWTweenEmpty AddEvent(LDFWTweenEmptyEvent tweenEvent)
        {
            targetEventList.Add(tweenEvent);
            PreStart();

            return this;
        }

        /// <summary>
        /// PreStart function
        /// </summary>
        protected override void PreStart()
        {
            base.PreStart();

            if (targetEventList == null)
            {
                targetEventList = new List<LDFWTweenEmptyEvent>();
            }
            else
            {
                targetEventList = DnVSort(targetEventList);
            }
        }

        /// <summary>
        /// Post current value calculation
        /// </summary>
        protected override void PostCurrentValueCalculation()
        {
            float currentProgress = GetCurrentPercentage();
            while (targetEventList != null && targetEventList.Count > 0 && currentProgress > targetEventList[0].targetPercentage)
            {
                if (targetEventList[0].eventAction != null)
                    targetEventList[0].eventAction();

                targetEventList.RemoveAt(0);
            }
        }

        /// <summary>
        /// Divide and conquer sort based on target percentage
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
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
            List<LDFWTweenEmptyEvent> leftList = new List<LDFWTweenEmptyEvent>();
            List<LDFWTweenEmptyEvent> rightList = new List<LDFWTweenEmptyEvent>();
            List<LDFWTweenEmptyEvent> middleList = new List<LDFWTweenEmptyEvent>();

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