﻿using ScriptableObjects.Values;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public abstract class ScriptableValueInputField : MonoBehaviour
    {
        protected TMP_InputField inputField;

        public UnityEvent OnSubmitted;

        [SerializeField]
        protected bool selectOnSubmit;

        [SerializeField]
        protected StringValue stringValue;

        protected virtual void Awake()
        {
            inputField = GetComponent<TMP_InputField>();
        }

        private void Start()
        {
            stringValue.ResetValue();
        }

        protected virtual void OnEnable()
        {
            inputField.onValueChanged.AddListener(OnValueChanged);
            inputField.onEndEdit.AddListener(OnEndEdit);
            inputField.onSubmit.AddListener(OnSubmit);
        }

        protected virtual void OnDisable()
        {
            inputField.onValueChanged.RemoveListener(OnValueChanged);
            inputField.onEndEdit.RemoveListener(OnEndEdit);
            inputField.onSubmit.RemoveListener(OnSubmit);
        }

        protected void OnValueChanged(string value)
        {
            stringValue.Value = value;
        }

        protected void OnEndEdit(string value)
        {
            stringValue.Value = value;
        }

        protected void OnSubmit(string value)
        {
            if (selectOnSubmit) 
                inputField.ActivateInputField();

            NotifySubmitted();
        }

        protected void NotifySubmitted()
        {
            OnSubmitted.Invoke();
        }
    }
}