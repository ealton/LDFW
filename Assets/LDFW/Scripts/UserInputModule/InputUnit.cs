using UnityEngine;
using System.Collections;

namespace LDFW.UserInput
{
    public class InputUnit
    {

        public float                    altitudeAngle;
        public float                    azimuthAngle;
        public float                    deltaPosition;
        public float                    deltaTime;
        public float                    radius;
        public float                    radiusVariance;
        public float                    tapCount;
        public float                    type;
        public float                    maximumPossiblePressure;

        public int                      fingerId;

        public InputUnitPhase           phase;
        public Vector2                  position;
        public Vector2                  rawPosition;

        public InputUnit ()
        {

        }
    }


    public enum InputUnitPhase
    {
        Began,
        Moved,
        Stationary,
        Ended,
        Canceled,
    }
}