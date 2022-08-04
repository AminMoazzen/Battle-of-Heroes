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

    private Coroutine _holding;
    private Status _status;

    public void OnPointerDown(PointerEventData eventData)
    {
        _status = Status.Click;
        StartCountdown(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCountdown();
        switch (_status)
        {
            case Status.Click:
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
        _holding = StartCoroutine(Holding(eventData));
    }

    private void StopCountdown()
    {
        if (_holding != null)
            StopCoroutine(_holding);
    }

    private IEnumerator Holding(PointerEventData eventData)
    {
        yield return new WaitForSecondsRealtime(holdThreshold);

        _status = Status.Hold;
        onHoldBegin.Invoke(eventData);
    }
}