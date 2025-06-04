using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Counter : MonoBehaviour
{
    [SerializeField] private float _step = 0.5f;
    [SerializeField] private float _delay = 1f;

    public event UnityAction CountChanged;

    private InputReader _inputReader;

    private Coroutine _currentCoroutine;
    private float _count;
    private bool _isRunning = true;

    public float Count => _count;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _count = 0;
    }

    private void OnEnable() =>
        _inputReader.MouseClicked += ToggleCounter;

    private void OnDisable() =>
        _inputReader.MouseClicked -= ToggleCounter;

    private void Increase()
    {
        _count += _step;
        CountChanged?.Invoke();
    }

    private IEnumerator CounterRoutine()
    {
        WaitForSeconds waitForSeconds = new(_delay);

        while (true)
        {
            Increase();
            yield return waitForSeconds;
        }
    }

    private void ToggleCounter()
    {
        if (_isRunning)
        {
            _currentCoroutine = StartCoroutine(CounterRoutine());
            _isRunning = false;
        }
        else
        {
            StopCoroutine(_currentCoroutine);
            _isRunning = true;
        }
    }
}
