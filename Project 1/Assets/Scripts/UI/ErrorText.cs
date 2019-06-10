using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class ErrorText : MonoBehaviour
{
    [SerializeField]
    private OnFormValidated formValidated = null;

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
        if (formValidated is null)
        {
            throw new NullReferenceException(nameof(formValidated));
        }
    }

    private void OnEnable()
    {
        formValidated.OnPublished += OnFormValidated;
    }

    private void OnDisable()
    {
        formValidated.OnPublished -= OnFormValidated;
    }

    private void OnFormValidated()
    {
        text.text = formValidated.HasAnErrorMessage ? formValidated.ErrorMessage : "";
    }
}
