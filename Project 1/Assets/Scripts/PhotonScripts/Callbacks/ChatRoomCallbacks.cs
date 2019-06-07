using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChatRoomCallbacks : MonoBehaviourPunCallbacks
{
    private const string DefaultOfflineRoomName = "Offline";

    [SerializeField]
    private string offlineRoomName = DefaultOfflineRoomName;

    [SerializeField]
    private OnLeaveRoomButtonClicked onLeaveRoomButtonClicked = null;

    private void Awake()
    {
        VerifySerializeFields();
    }

    private void VerifySerializeFields()
    {
        if (onLeaveRoomButtonClicked is null)
        {
            throw new NullReferenceException(nameof(onLeaveRoomButtonClicked));
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();

        onLeaveRoomButtonClicked.OnPublished += OnLeaveRoomButtonClicked;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        onLeaveRoomButtonClicked.OnPublished -= OnLeaveRoomButtonClicked;
    }

    private void OnLeaveRoomButtonClicked()
    {
        LeaveRoom();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(offlineRoomName);
    }
}
