using System;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    private const string DefaultUserNameEmptyErrorMessage = "Username empty, please enter something.";

    [SerializeField]
    private OnFormValidated onMenuValidated = null;

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
        if (onMenuValidated is null)
        {
            throw new NullReferenceException(nameof(onMenuValidated));
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
            NotifyFormError(userNameEmptyErrorMessage);
            ShowErrorTitle();
        }
        else
        {
            HideErrorTitle();
            NotifyFormValidated();
            NotifyUserJoinsRoom();
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
}