using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public System.Action OnTap;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        // Mouse (Editor / PC)
        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            OnTap?.Invoke();
        }

        // Touch (Mobile)
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            OnTap?.Invoke();
        }
    }
}
