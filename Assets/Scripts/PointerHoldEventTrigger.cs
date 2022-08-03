using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerHoldEventTrigger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private enum Status
    {
        Click,
        Hold,
        Cancel,
    }

    [SerializeField] private float holdThreshold = 3;
    [SerializeField] private UnityEvent<PointerEventData> onClick;
    [SerializeField] private UnityEvent<PointerEventData> onHoldBegin;
    [SerializeField] private UnityEvent<PointerEventData> onHoldEnd;

    private float _currentTime;
    private Coroutine _countingDown;
    private Status _status;

    public void OnPointerDown(PointerEventData eventData)
    {
        _status = Status.Click;
        StartCountdown(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (_status)
        {
            case Status.Click:
                StopCountdown();
                onClick.Invoke(eventData);
                break;

            case Status.Hold:
                onHoldEnd.Invoke(eventData);
                break;

            default:
                break;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        StopCountdown();
        _status = Status.Cancel;
        onHoldEnd.Invoke(eventData);
    }

    private void StartCountdown(PointerEventData eventData)
    {
        StopCountdown();
        _countingDown = StartCoroutine(CountingDown(eventData));
    }

    private void StopCountdown()
    {
        if (_countingDown != null)
            StopCoroutine(_countingDown);
    }

    private IEnumerator CountingDown(PointerEventData eventData)
    {
        while (_currentTime < holdThreshold)
        {
            yield return null;
            _currentTime += Time.deltaTime;
        }

        _currentTime = 0;
        _status = Status.Hold;
        onHoldBegin.Invoke(eventData);
    }
}