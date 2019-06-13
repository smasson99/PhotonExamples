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
        text = GetComponent<TMP_Text>();
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
