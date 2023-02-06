using UnityEngine;


namespace Utils.Input.Providers
{
    public sealed class KeyboardMovementInputProvider : Vector2InputProvider
    {
        public void Update()
        {
            var input = new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
            OnInput?.Invoke(input);
        }
    }
}
