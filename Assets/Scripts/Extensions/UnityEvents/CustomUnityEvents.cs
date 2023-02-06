using System;
using Core;
using UnityEngine;
using UnityEngine.Events;


namespace Extensions.UnityEvents
{
    [Serializable]
    public class UnityVector2Event : UnityEvent<Vector2>
    {
    }


    [Serializable]
    public class UnityPlayerStateEvent : UnityEvent<PlayerState>
    {
    }
}
