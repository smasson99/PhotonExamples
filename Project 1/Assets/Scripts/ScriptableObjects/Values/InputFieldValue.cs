using UnityEngine;

[CreateAssetMenu(menuName = "Value/UI/InputFieldValue", fileName = "InputFieldValue")]
public class InputFieldValue : EventChannels.EventChannel
{
    public string Value { get; set; }

    public bool IsValueNullOrEmpty => string.IsNullOrEmpty(Value);
}