using System;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    private const string DefaultUserNameEmptyErrorMessage = "Username empty, please enter something.";

    [SerializeField]
    private OnFormErrorHappened onMenuErrorHappened = null;

    [SerializeField]
    private InputFieldValue userNameInputFieldValue = null;

    [SerializeField]
    private OnUserJoinsRoom onUserJoinsRoom = null;

    [SerializeField]
    private OnJoinRoomButtonClicked onJoinRoomButtonClicked = null;

    [SerializeField]
    private string userNameEmptyErrorMessage = DefaultUserNameEmptyErrorMessage;

    [SerializeField]
    private GameObject errorTitle = null;

    private void Awake()
    {
        VerifySerializeFields();
    }

    private void VerifySerializeFields()
    {
        if (onMenuErrorHappened is null)
        {
            throw new NullReferenceException(nameof(onMenuErrorHappened));
        }

        if (onUserJoinsRoom is null)
        {
            throw new NullReferenceException(nameof(onUserJoinsRoom));
        }

        if (onJoinRoomButtonClicked is null)
        {
            throw new NullReferenceException(nameof(onJoinRoomButtonClicked));
        }
    }

    private void Start()
    {
        HideErrorTitle();
    }

    private void NotifyErrorHappened(string errorMessage)
    {
        onMenuErrorHappened.Publish(errorMessage);
    }

    private void NotifyUserJoinsRoom()
    {
        onUserJoinsRoom.Publish();
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
        if (userNameInputFieldValue.IsValueNullOrEmpty)
        {
            NotifyErrorHappened(userNameEmptyErrorMessage);
            ShowErrorTitle();
        }
        else
        {
            HideErrorTitle();
            NotifyUserJoinsRoom();
        }
    }

    private void HideErrorTitle()
    {
        NotifyErrorHappened("");
        errorTitle.gameObject.SetActive(false);
    }
    
    private void ShowErrorTitle()
    {
        errorTitle.gameObject.SetActive(true);
    }
}