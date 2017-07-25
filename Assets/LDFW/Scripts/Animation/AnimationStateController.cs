using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using Version3.Common;

namespace Version3.Common
{

    



    public class AnimationStateController : MonoBehaviour
    {

        public string                       currentAnimatoinStateName;

        public Animator                     animator;
        private AnimationStateAction[]      animationStateActionList;

        private bool                        isTrackingAnimationState = false;
        private bool                        isInitialInTransition = false;
        
        private int                         layer;
        private int                         stateActionListLength;

        private AnimationStateAction        beginAction;
        private AnimationStateAction        endAction;
        


        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="currentAnimatoinStateName"></param>
        /// <param name="animator"></param>
        /// <param name="animationStateActionList"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public AnimationStateController Reset(string currentAnimatoinStateName, Animator animator, AnimationStateAction[] animationStateActionList, int layer = 0)
        {
            this.currentAnimatoinStateName = currentAnimatoinStateName;
            this.animator = animator;
            this.animationStateActionList = animationStateActionList;
            this.layer = layer;
            this.isInitialInTransition = animator.IsInTransition(layer);

            stateActionListLength = animationStateActionList.Length;
            isTrackingAnimationState = true;

            return this;
        }

        /// <summary>
        /// 停止跟踪
        /// </summary>
        public void StopTracking()
        {
            isTrackingAnimationState = false;
            Destroy(this);
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (isTrackingAnimationState)
            {
                AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(layer);
                if (isInitialInTransition)
                {
                    if (animator.IsInTransition(layer))
                        info = animator.GetNextAnimatorStateInfo(layer);
                    else
                        isInitialInTransition = false;
                }

                //Debug.Log("Current normalized time = " + info.normalizedTime);
                CheckAnimationStateActionList(info.normalizedTime);


            }
        }

        /// <summary>
        /// Check action state list
        /// </summary>
        /// <param name="currentAnimationStateProgress"></param>
        private void CheckAnimationStateActionList(float currentAnimationStateProgress)
        {
            if (!isTrackingAnimationState)
            {
                Destroy(this);
                return;
            }

            if (animationStateActionList != null)
            {
                for (int i = 0; i < stateActionListLength; i++)
                {
                    if (animationStateActionList[i] != null)
                    {
                        if (animationStateActionList[i].CheckForAction(currentAnimationStateProgress))
                            animationStateActionList[i] = null;
                    }
                }
            }

            if (currentAnimationStateProgress > 1)
                StopTracking();
        }

    }


    [Serializable]
    public class AnimationStateAction
    {

        /// <summary>
        /// Target percentage progress of this action
        /// </summary>
        private float                       triggerProgress = 0f;

        /// <summary>
        /// Call back action of this 
        /// </summary>
        private Action                      triggerAction = null;

        /// <summary>
        /// Has triggered or not
        /// </summary>
        private bool                        hasTriggered = false;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="action"></param>
        public AnimationStateAction(float progress, Action action)
        {
            if (progress < 0 || progress > 1)
                Debug.LogError("Progress should (- [0, 1]");

            triggerProgress = progress;
            triggerAction = action;
            hasTriggered = false;
        }

        /// <summary>
        /// Validate the current progress, triggers the action if current progress is greater than trigger progress
        /// </summary>
        /// <param name="currentProgress"></param>
        /// <returns></returns>
        public bool CheckForAction(float currentProgress)
        {
            if (hasTriggered || triggerAction == null)
                return true;

            if (currentProgress > triggerProgress)
            {
                triggerAction();
                hasTriggered = true;
            }

            return hasTriggered;
        }
    }
}