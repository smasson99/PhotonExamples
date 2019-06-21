using EventChannels;
using UnityEngine;

namespace ScriptableObjects.Events.Inputs
{
    [CreateAssetMenu(menuName = "Event/Input/KeyboardKeyIsPressed", fileName = nameof(KeyboardKeyIsPressed))]
    public class KeyboardKeyIsPressed : EventChannel
    {
        private KeyCode keyCode;

        public KeyCode KeyCode => keyCode;

        public void Publish(KeyCode keyCode)
        {
            this.keyCode = keyCode;
        
            base.Publish();
        }
    }
}
