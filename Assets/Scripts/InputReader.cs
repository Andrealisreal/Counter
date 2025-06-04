using UnityEngine;
using UnityEngine.Events;

public class InputReader : MonoBehaviour
{
    public event UnityAction MouseClicked;

    private PlayerInput _input;

    private void Awake()
    {
        _input = new PlayerInput();

        _input.Player.ClickMouseLeft.performed += context => Click();
    }

    private void OnEnable() =>
        _input.Enable();

    private void OnDisable() =>
        _input.Disable();

    private void Click() =>
        MouseClicked?.Invoke();
}
