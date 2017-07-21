using UnityEngine;
using System.Collections;

namespace LDFW.UserInput
{
    
    /// <summary>
    /// Touch begin
    /// </summary>
    public interface ITouchBegin
    {
        void OnTouchBegin(InputData input);
    }

    /// <summary>
    /// Touch drag
    /// </summary>
    public interface ITouchDrag
    {
        void OnTouchDrag(InputData input);
    }

    /// <summary>
    /// Touch end
    /// </summary>
    public interface ITouchEnd
    {
        void OnTouchEnd(InputData input);
    }

    /// <summary>
    /// Touch click
    /// </summary>
    public interface ITouchClick
    {
        void OnTouchClick(InputData input);
    }

    /// <summary>
    /// Touch double click
    /// </summary>
    public interface ITouchDoubleClick
    {
        void OnTouchDoubleClick(InputData input);
    }
}