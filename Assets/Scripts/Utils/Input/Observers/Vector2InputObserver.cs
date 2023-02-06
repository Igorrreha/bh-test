using UnityEngine;
using Extensions.UnityEvents;
using Utils.Input.Providers;


namespace Utils.Input.Observers
{
    public class Vector2InputObserver : InputObserver<Vector2, Vector2InputProvider, UnityVector2Event>
    {
        private bool _zeroInputInPreviousFrame;


        protected override bool CheckInputIsValid(Vector2 input)
        {
            var zeroInput = input.magnitude == 0;
            if (zeroInput && _zeroInputInPreviousFrame)
                return false;
            
            _zeroInputInPreviousFrame = zeroInput;
            return true;
        }
    }
}
