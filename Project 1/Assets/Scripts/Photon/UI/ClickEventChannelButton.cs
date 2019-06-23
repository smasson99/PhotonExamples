using System;
using EventChannels;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ClickEventChannelButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    protected EventChannel onClickEventChannel = null;
    
    protected Button button;

    protected virtual void Awake()
    {
        GetComponents();
        
        VerifyComponents();
    }

    protected virtual void GetComponents()
    {
        button = GetComponent<Button>();
    }

    protected virtual void VerifyComponents()
    {
        if (button is null)
        {
            throw new NullReferenceException(nameof(button));
        }
    }

    protected virtual void NotifyClicked()
    {
        onClickEventChannel.Publish();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        NotifyClicked();
    }
}
