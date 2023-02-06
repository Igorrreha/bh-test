using UnityEngine;


namespace Utils.Input.Providers
{
    public sealed class MouseMovementInputProvider : Vector2InputProvider
    {
        public void Update()
        {
            var input = new Vector2(UnityEngine.Input.GetAxis("Mouse X"), UnityEngine.Input.GetAxis("Mouse Y"));
            OnInput?.Invoke(input);
        }
    }
}
