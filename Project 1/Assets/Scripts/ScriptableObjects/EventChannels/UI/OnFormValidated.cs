using UnityEngine;

[CreateAssetMenu(menuName = "Event/UI/OnFormValidated", fileName = "OnFormValidated")]
public class OnFormValidated : EventChannels.EventChannel
{
    private string errorMessage;

    public string ErrorMessage => errorMessage;

    public bool HasAnErrorMessage => !string.IsNullOrEmpty(errorMessage) || !string.IsNullOrWhiteSpace(errorMessage);

    public void Publish(string errorMessage)
    {
        this.errorMessage = errorMessage;
        
        base.Publish();
    }

    public void Publish()
    {
        errorMessage = "";
        
        base.Publish();
    }
}
