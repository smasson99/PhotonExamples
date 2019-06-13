using UnityEngine;

[CreateAssetMenu(menuName = "Value/StringValue", fileName = "StringValue")]
public class StringValue : EventChannels.EventChannel
{
    public string Value { get; set; }

    public bool IsValueNullOrEmpty => string.IsNullOrEmpty(Value);

    public void ResetValue()
    {
        Value = "";
    }
}