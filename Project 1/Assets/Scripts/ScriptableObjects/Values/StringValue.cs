using UnityEngine;

[CreateAssetMenu(menuName = "Value/UI/StringValue", fileName = "StringValue")]
public class StringValue : EventChannels.EventChannel
{
    public string Value { get; set; }

    public bool IsValueNullOrEmpty => string.IsNullOrEmpty(Value);
}