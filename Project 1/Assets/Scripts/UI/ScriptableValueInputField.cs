using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ScriptableValueInputField : MonoBehaviour
{
    [SerializeField]
    protected StringValue stringValue = null;

    protected TMP_InputField inputField;
    
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
    }
    
    protected virtual void OnDisable()
    {
        inputField.onValueChanged.RemoveListener(OnValueChanged);
        inputField.onEndEdit.RemoveListener(OnEndEdit);
    }

    protected virtual void OnValueChanged(string value)
    {
        stringValue.Value = value;
    }

    protected virtual void OnEndEdit(string value)
    {
        stringValue.Value = value;
    }
}
