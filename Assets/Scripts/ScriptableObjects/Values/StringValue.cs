using EventChannels;
using UnityEngine;

namespace ScriptableObjects.Values
{
    [CreateAssetMenu(menuName = "Value/StringValue", fileName = "StringValue")]
    public sealed class StringValue : EventChannel
    {
        public string Value { get; set; }

        public bool IsValueNullOrEmpty => string.IsNullOrEmpty(Value);

        public void ResetValue()
        {
            Value = "";
        }
    }
}