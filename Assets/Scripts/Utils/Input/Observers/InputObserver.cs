using UnityEngine;
using UnityEngine.Events;
using Utils.Input.Providers;


namespace Utils.Input.Observers
{
    public abstract class InputObserver<TInput, TProvider, TUnityEvent> : MonoBehaviour
        where TProvider : InputProvider<TInput>
        where TUnityEvent : UnityEvent<TInput>
    {
        [SerializeField] private bool _isActive;
        [SerializeField] private TProvider _inputProvider; 
        [SerializeField] private TUnityEvent _onInput;


        public void SetActive(bool isActive)
        {
            _isActive = isActive;
        }


        private void OnEnable()
        {
            _inputProvider.OnInput += OnInputHandled;
        }


        private void OnDisable()
        {
            _inputProvider.OnInput -= OnInputHandled;
        }


        private void OnInputHandled(TInput input)
        {
            if (!_isActive)
                return;

            if (CheckInputIsValid(input))
                _onInput?.Invoke(input);
        }


        protected abstract bool CheckInputIsValid(TInput input);
    }
}
