using UnityEngine;

public class ViewCounter : MonoBehaviour
{
    private Counter _counter;

    private void Awake() =>
        _counter = GetComponent<Counter>();

    private void OnEnable() =>
        _counter.CountChanged += DisplayCount;

    private void OnDisable() =>
        _counter.CountChanged -= DisplayCount;

    private void DisplayCount() =>
        Debug.Log(_counter.Count);
}
