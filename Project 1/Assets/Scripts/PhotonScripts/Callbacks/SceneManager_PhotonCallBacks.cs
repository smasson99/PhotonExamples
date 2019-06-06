using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_PhotonCallBacks : MonoBehaviourPunCallbacks
{
    private const byte DefaultAmountOfPlayersPeerRoom = 5;
    private const string DefaultOnlineRoomName = "Room";
    private const string DefaultOfflineRoomName = "Offline";

    public const byte DefaultMinAmountOfPlayers = 1;
    public const byte DefaultMaxAmountOfPlayers = 20;

    [SerializeField]
    [Range(1, 20)]
    private byte maxAmountOfPlayersPeerRoom = 5;

    [SerializeField]
    private string onlineRoomName = DefaultOnlineRoomName;
    
    [SerializeField]
    private string offlineRoomName = DefaultOfflineRoomName;
    
    [SerializeField]
    private OnJoinRoomButtonClicked onJoinRoomButtonClicked = null;
    
    [SerializeField]
    private OnLeaveRoomButtonClicked onLeaveRoomButtonClicked = null;

    private bool isConnecting = true;

    private void Awake()
    {
        VerifySerializeFields();
    }

    private void VerifySerializeFields()
    {
        if (onJoinRoomButtonClicked is null)
        {
            throw new NullReferenceException(nameof(onJoinRoomButtonClicked));
        }if (onLeaveRoomButtonClicked is null)
        {
            throw new NullReferenceException(nameof(onLeaveRoomButtonClicked));
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        
        onJoinRoomButtonClicked.OnPublished += OnJoinRoomButtonClicked;
        onLeaveRoomButtonClicked.OnPublished += OnLeaveRoomButtonClicked;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        
        onJoinRoomButtonClicked.OnPublished -= OnJoinRoomButtonClicked;
        onLeaveRoomButtonClicked.OnPublished -= OnLeaveRoomButtonClicked;
    }

    private void OnJoinRoomButtonClicked()
    {
        Debug.Log("Joins the room");
        Connect();
    }
    
    private void OnLeaveRoomButtonClicked()
    {
        Debug.Log("Leaves the Room");
        LeaveRoom();
    }

    public void Connect()
    {
        isConnecting = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            RoomOptions roomOptions = new RoomOptions()
            {
                IsVisible = true,
                MaxPlayers = maxAmountOfPlayersPeerRoom
            };

            PhotonNetwork.JoinOrCreateRoom("Room1", roomOptions, TypedLobby.Default);
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(offlineRoomName);
    }

    public override void OnJoinedRoom()
    {
        isConnecting = false;
        SceneManager.LoadScene(onlineRoomName);
    }
}