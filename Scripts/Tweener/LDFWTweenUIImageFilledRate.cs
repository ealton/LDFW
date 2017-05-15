using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace LDFW.Tween
{
    public class LDFWTweenUIImageFilledRate : LDFWTweenBaseOne
    {

        // Public Variables
        public Image targetImage;

        // Private Variables
        private Vector2 tempVector = Vector2.zero;

        public LDFWTweenUIImageFilledRate SetTargetImage(Image image)
        {
            targetImage = image;
            targetTransform = image.transform;

            return this;
        }

        void Awake()
        {
            if (targetTransform == null)
                targetTransform = GetComponent<Transform>();
            
            targetImage = (targetTransform as RectTransform).GetComponent<Image>();
        }

        protected override void PreStart()
        {
            if (targetImage != null)
                startingValue[0] = targetImage.fillAmount;
        }

        protected override void PostCurrentValueCalculation()
        {
            if (targetImage != null)
                targetImage.fillAmount = currentValue[0];
        }
    }

}