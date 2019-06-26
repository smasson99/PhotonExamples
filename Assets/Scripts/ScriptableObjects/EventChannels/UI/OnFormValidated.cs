using EventChannels;
using UnityEngine;

namespace ScriptableObjects.EventChannels.UI
{
    [CreateAssetMenu(menuName = "Event/UI/OnFormValidated", fileName = "OnFormValidated")]
    public sealed class OnFormValidated : EventChannel
    {
        private string errorMessage;

        public string ErrorMessage => errorMessage;

        public bool HasAnErrorMessage => !string.IsNullOrEmpty(errorMessage) || !string.IsNullOrWhiteSpace(errorMessage);

        public void Publish(string errorMessage)
        {
            this.errorMessage = errorMessage;
        
            base.Publish();
        }

        public new void Publish()
        {
            errorMessage = "";
        
            base.Publish();
        }
    }
}
