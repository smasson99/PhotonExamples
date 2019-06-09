using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class ErrorText : MonoBehaviour
{
    [SerializeField]
    private OnFormErrorHappened formErrorHappened = null;

    protected TMP_Text text;
    
    private void Awake()
    {
        GetComponents();
        
        VerifyComponents();
        
        VerifySerializeFields();
    }

    protected virtual void GetComponents()
    {
        text = GetComponent<TMP_Text>();
    }

    protected virtual void VerifyComponents()
    {
        if (text is null)
        {
            throw new NullReferenceException(nameof(text));
        }
    }

    private void VerifySerializeFields()
    {
        if (formErrorHappened is null)
        {
            throw new NullReferenceException(nameof(formErrorHappened));
        }
    }

    private void OnEnable()
    {
        formErrorHappened.OnPublished += OnFormErrorHappened;
    }

    private void OnDisable()
    {
        formErrorHappened.OnPublished -= OnFormErrorHappened;
    }

    private void OnFormErrorHappened()
    {
        text.text = formErrorHappened.ErrorMessage;
    }
}
