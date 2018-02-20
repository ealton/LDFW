using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace LDFW.Tween
{
    /*
    public class LDFWTweenUILayoutElement : LDFWTweenBaseOne {

        public LayoutElement target;

        public enum LayoutElementTweenTarget {
            MinWidth,
            MinHeight,
            PreferredWidth,
            PreferredHeight,
            FlexibleWidth,
            FlexibleHeight
        }

        public LayoutElementTweenTarget layoutTweenTarget;

        void Awake () {
            if (target == null) {
                target = GetComponent<LayoutElement> ();
            }
        }

        public LDFWTweenUILayoutElement SetLayoutElementTweenTarget(LayoutElementTweenTarget target)
        {
            layoutTweenTarget = target;
            return this;
        }

        private void SetTargetValue (float val) {
            switch (layoutTweenTarget) {
                case LayoutElementTweenTarget.MinWidth:
                    target.minWidth = val;
                    break;
                case LayoutElementTweenTarget.MinHeight:
                    target.minHeight = val;
                    break;
                case LayoutElementTweenTarget.PreferredWidth:
                    target.preferredWidth = val;
                    break;
                case LayoutElementTweenTarget.PreferredHeight:
                    target.preferredHeight = val;
                    break;
                case LayoutElementTweenTarget.FlexibleWidth:
                    target.flexibleWidth = val;
                    break;
                case LayoutElementTweenTarget.FlexibleHeight:
                    target.flexibleHeight = val;
                    break;
            }
        }

        private float GetTargetValue () {
            switch (layoutTweenTarget) {
                case LayoutElementTweenTarget.MinWidth:
                    return target.minWidth;
                case LayoutElementTweenTarget.MinHeight:
                    return target.minHeight;
                case LayoutElementTweenTarget.PreferredWidth:
                    return target.preferredWidth;
                case LayoutElementTweenTarget.PreferredHeight:
                    return target.preferredHeight;
                case LayoutElementTweenTarget.FlexibleWidth:
                    return target.flexibleWidth;
                case LayoutElementTweenTarget.FlexibleHeight:
                    return target.flexibleHeight;
                default:
                    return 0f;
            }
        }

        protected override void PostCurrentValueCalculation()
        {
            if (target != null) {
                SetTargetValue (currentValue[0]);
            }
        }

    }
    */

}