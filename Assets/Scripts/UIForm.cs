using Nouranium;
using UnityEngine;
using UnityEngine.Events;

public class UIForm : MonoBehaviour
{
    [Header("Actions (Optional)")]
    [SerializeField] private Message[] showOn;

    [SerializeField] private Message[] hideOn;

    [Header("Events")]
    [SerializeField] private UnityEvent onShow;

    [SerializeField] private UnityEvent onHide;
    [SerializeField] private UnityEvent onSubmit;
    [SerializeField] private UnityEvent onSkip;

    private void Start()
    {
        foreach (Message msg in showOn)
        {
            msg.StartListening(Show);
        }

        foreach (Message msg in hideOn)
        {
            msg.StartListening(Hide);
        }
    }

    public void Show()
    {
        onShow.Invoke();
    }

    public void Hide()
    {
        onHide.Invoke();
    }

    public void Submit()
    {
        onSubmit.Invoke();
    }

    public void Skip()
    {
        onSkip.Invoke();
    }
}