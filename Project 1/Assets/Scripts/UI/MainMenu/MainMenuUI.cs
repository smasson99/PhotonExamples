using System;
using UnityEngine;
using UnityEngine.Events;

public class MainMenuUI : MonoBehaviour
{
    private const string DefaultUserNameEmptyErrorMessage = "Username empty, please enter something.";

    [SerializeField]
    private OnFormValidated onMenuValidated = null;

    [SerializeField]
    private StringValue userNameStringValue = null;

    [SerializeField]
    private MainMenuCallbacks callbacks = null;

    [SerializeField]
    private OnJoinRoomButtonClicked onJoinRoomButtonClicked = null;

    [SerializeField]
    private string userNameEmptyErrorMessage = DefaultUserNameEmptyErrorMessage;

    [SerializeField]
    private GameObject titleGameObject = null;

    [SerializeField]
    private GameObject usernameInputField = null;

    [SerializeField]
    private GameObject errorTitle = null;

    [SerializeField]
    private GameObject connectButton = null;

    private void Start()
    {
        HideErrorTitle();
        NotifyFormValidated();
    }

    private void NotifyFormError(string errorMessage)
    {
        onMenuValidated.Publish(errorMessage);
    }

    private void NotifyFormValidated()
    {
        onMenuValidated.Publish();
    }

    private void JoinTheRoom()
    {
        callbacks.Connect();
    }

    private void OnEnable()
    {
        onJoinRoomButtonClicked.OnPublished += OnJoinRoomButtonClicked;
    }

    private void OnDisable()
    {
        onJoinRoomButtonClicked.OnPublished -= OnJoinRoomButtonClicked;
    }

    private void OnJoinRoomButtonClicked()
    {
        TryToEnterTheRoom();
    }

    private void TryToEnterTheRoom()
    {
        if (userNameStringValue.IsValueNullOrEmpty)
        {
            NotifyFormError(userNameEmptyErrorMessage);
            ShowErrorTitle();
        }
        else
        {
            HideTitle();
            HideErrorTitle();
            HideUsernameInputField();
            HideConnectButton();
            NotifyFormValidated();
            JoinTheRoom();
        }
    }

    private void HideErrorTitle()
    {
        errorTitle.gameObject.SetActive(false);
    }

    private void ShowErrorTitle()
    {
        errorTitle.gameObject.SetActive(true);
    }

    private void HideConnectButton()
    {
        connectButton.SetActive(false);
    }

    private void HideUsernameInputField()
    {
        usernameInputField.SetActive(false);
    }

    private void HideTitle()
    {
        titleGameObject.SetActive(false);
    }

    public void OnFormSubmitted()
    {
        TryToEnterTheRoom();
    }
}