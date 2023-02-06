using System;
using System.Collections;
using UnityEngine;
using Extensions.UnityEvents;


namespace Core
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _dashDistance;
        [SerializeField] private float _invulnerabilityAfterHitDuration;

        [SerializeField] private UnityPlayerStateEvent _onStateChanged;

        private PlayerState _state;
        private PlayerState State
        {
            get => _state;
            set
            {
                if (_state == value)
                    return;

                _state = value;
                _onStateChanged?.Invoke(_state);
            }
        }
        
        public float MoveSpeed => _moveSpeed;
        public float DashDistance => _dashDistance;


        public void TakeHit()
        {
            switch (State)
            {
                case PlayerState.Default:
                    State = PlayerState.Invulnerable;
                    StartCoroutine(nameof(AfterHitInvulnerabilityCoroutine));
                    break;
                
                default:
                    break;
            }
        }


        public IEnumerator AfterHitInvulnerabilityCoroutine()
        {
            yield return new WaitForSeconds(_invulnerabilityAfterHitDuration);
            State = PlayerState.Default;
        }
    }


    [Serializable]
    public enum PlayerState
    {
        Default = 0,
        Invulnerable = 1,
    }
}
