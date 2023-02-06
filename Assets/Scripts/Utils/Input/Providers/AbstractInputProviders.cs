using System;
using UnityEngine;


namespace Utils.Input.Providers
{
    public abstract class TriggerInputProvider : MonoBehaviour
    {
        public Action OnInput;
    }
    
    
    public abstract class InputProvider<T> : MonoBehaviour
    {
        public Action<T> OnInput;
    }


    public abstract class Vector2InputProvider : InputProvider<Vector2>
    {
    }
}
