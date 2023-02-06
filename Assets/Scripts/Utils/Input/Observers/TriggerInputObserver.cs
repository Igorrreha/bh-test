using UnityEngine;
using UnityEngine.Events;
using Utils.Input.Providers;


namespace Utils.Input.Observers
{
    public class TriggerInputObserver : MonoBehaviour
    {
        [SerializeField] private bool _isActive;
        [SerializeField] private TriggerInputProvider _inputProvider; 
        [SerializeField] private UnityEvent _onInput;


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


        private void OnInputHandled()
        {
            if (!_isActive)
                return;

            _onInput?.Invoke();
        }
    }
}
