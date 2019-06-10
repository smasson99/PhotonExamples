using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ScriptableValueInputField : MonoBehaviour
{
    [SerializeField]
    protected InputFieldValue inputFieldValue = null;

    protected TMP_InputField inputField;
    
    protected virtual void Awake()
    {
        GetComponents();
        
        VerifySerializeFields();
        VerifyComponents();
    }

    protected virtual void GetComponents()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    protected virtual void VerifySerializeFields()
    {
        if (inputFieldValue is null)
        {
            throw new NullReferenceException(nameof(inputFieldValue));
        }
    }

    protected virtual void VerifyComponents()
    {
        if (inputField is null)
        {
            throw new NullReferenceException(nameof(inputFieldValue));
        }
    }

    private void Start()
    {
        inputFieldValue.Value = "";
    }

    protected virtual void OnEnable()
    {
        inputField.onValueChanged.AddListener(OnValueChanged);
        inputField.onEndEdit.AddListener(OnEndEdit);
    }
    
    protected virtual void OnDisable()
    {
        inputField.onValueChanged.RemoveListener(OnValueChanged);
        inputField.onEndEdit.RemoveListener(OnEndEdit);
    }

    protected virtual void OnValueChanged(string value)
    {
        inputFieldValue.Value = value;
    }

    protected virtual void OnEndEdit(string value)
    {
        inputFieldValue.Value = value;
    }
}
