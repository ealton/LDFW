using UnityEngine;
using System.Collections;

namespace LDFW.UserInput
{
    
    public interface ITouchBegin
    {
        void OnTouchBegin(InputData input);
    }

    public interface ITouchDrag
    {
        void OnTouchDrag(InputData input);
    }

    public interface ITouchEnd
    {
        void OnTouchEnd(InputData input);
    }

    public interface ITouchClick
    {
        void OnTouchClick(InputData input);
    }

    public interface ITouchDoubleClick
    {
        void OnTouchDoubleClick(InputData input);
    }
}