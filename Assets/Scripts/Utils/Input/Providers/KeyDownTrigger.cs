using UnityEngine;


namespace Utils.Input.Providers
{
    public sealed class KeyDownTrigger : TriggerInputProvider
    {
        [SerializeField] private KeyCode _keyCode;
        
        
        public void Update()
        {
            if (UnityEngine.Input.GetKeyDown(_keyCode))
                OnInput?.Invoke();
        }
    }
}
