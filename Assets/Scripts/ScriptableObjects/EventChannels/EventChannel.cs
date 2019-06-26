using System;
using UnityEngine;

namespace EventChannels
{
    public abstract class EventChannel : ScriptableObject
    {
        public event Action OnPublished;

        public void Publish()
        {
            OnPublished?.Invoke();
        }
    }
}
