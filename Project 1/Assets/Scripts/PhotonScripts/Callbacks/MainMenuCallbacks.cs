using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCallbacks : MonoBehaviourPunCallbacks
{
    private const byte DefaultAmountOfPlayersPeerRoom = 5;
    private const string DefaultOnlineRoomName = "Room";

    public const byte DefaultMinAmountOfPlayers = 1;
    public const byte DefaultMaxAmountOfPlayers = 20;

    [SerializeField]
    [Range(DefaultMinAmountOfPlayers, DefaultMaxAmountOfPlayers)]
    private byte maxAmountOfPlayersPeerRoom = DefaultAmountOfPlayersPeerRoom;

    [SerializeField]
    private string onlineRoomName = DefaultOnlineRoomName;

    [SerializeField]
    private OnJoinRoomButtonClicked onJoinRoomButtonClicked = null;

    private bool isConnecting;

    private void Awake()
    {
        VerifySerializeFields();
    }

    private void VerifySerializeFields()
    {
        if (onJoinRoomButtonClicked is null)
        {
            throw new NullReferenceException(nameof(onJoinRoomButtonClicked));
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();

        onJoinRoomButtonClicked.OnPublished += OnJoinRoomButtonClicked;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        onJoinRoomButtonClicked.OnPublished -= OnJoinRoomButtonClicked;
    }

    private void OnJoinRoomButtonClicked()
    {
        Connect();
    }

    public void Connect()
    {
        Debug.Log(PhotonNetwork.IsConnectedAndReady);
        
        if (!isConnecting)
        {
            isConnecting = true;
        
            PhotonNetwork.ConnectUsingSettings();
        }
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

            PhotonNetwork.JoinOrCreateRoom("ChatRoom", roomOptions, TypedLobby.Default);
        }
    }

    public override void OnJoinedRoom()
    {
        isConnecting = false;
        SceneManager.LoadScene(onlineRoomName);
    }
}