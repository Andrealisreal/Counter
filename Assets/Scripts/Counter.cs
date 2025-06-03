using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Counter : MonoBehaviour
{
    [SerializeField] private float _step = 0.5f;
    [SerializeField] private float _delay = 1f;

    private float _count;
    private bool _isRunning;
    private Coroutine _counterRoutine;

    private PlayerInput _playerInput;
    private ViewCounter _viewCounter;

    public event UnityAction CountChanged;

    public float Count => _count;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.Player.ClickMouseLeft.performed += context => ToggleCounter();
    }

    private void OnEnable() =>
        _playerInput.Enable();

    private void OnDisable() =>
        _playerInput.Disable();

    private void Start()
    {
        _count = 0;
        _viewCounter = GetComponent<ViewCounter>();
    }

    private void ToggleCounter()
    {
        if (_isRunning)
        {
            StopCoroutine(_counterRoutine);
            _isRunning = false;
        }
        else
        {
            _counterRoutine = StartCoroutine(CounterRoutine());
            _isRunning = true;
        }
    }

    private void Increase()
    {
        _count += _step;
        CountChanged?.Invoke();
    }

    private IEnumerator CounterRoutine()
    {
        while (true)
        {
            Increase();
            yield return new WaitForSeconds(_delay);
        }
    }
}
