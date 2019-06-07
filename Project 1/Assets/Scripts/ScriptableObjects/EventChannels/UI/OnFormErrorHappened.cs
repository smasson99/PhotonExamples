using UnityEngine;

[CreateAssetMenu(menuName = "Event/UI/OnFormErrorHappened", fileName = "OnFormErrorHappened")]
public class OnFormErrorHappened : EventChannels.EventChannel
{
    private string errorMessage;

    public string ErrorMessage => errorMessage;

    public void Publish(string errorMessage)
    {
        this.errorMessage = errorMessage;
        
        base.Publish();
    }
}
