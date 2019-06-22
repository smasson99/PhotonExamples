using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhotonScripts.Callbacks
{
    public class MainMenuCallbacks : MonoBehaviourPunCallbacks
    {
        private const byte DefaultAmountOfPlayersPeerRoom = 5;
        private const string DefaultOnlineRoomName = "Room";

        public const byte DefaultMinAmountOfPlayers = 1;
        public const byte DefaultMaxAmountOfPlayers = 20;

        [SerializeField]
        private StringValue usernameValue = null;
        
        [SerializeField]
        [Range(DefaultMinAmountOfPlayers, DefaultMaxAmountOfPlayers)]
        private byte maxAmountOfPlayersPeerRoom = DefaultAmountOfPlayersPeerRoom;

        [SerializeField]
        private string onlineRoomName = DefaultOnlineRoomName;

        private bool isConnecting;
    
        public void Connect()
        {
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

                PhotonNetwork.LocalPlayer.NickName = usernameValue.Value;

                PhotonNetwork.JoinOrCreateRoom("ChatRoom", roomOptions, TypedLobby.Default);
            }
        }

        public override void OnJoinedRoom()
        {
            isConnecting = false;
        
            SceneManager.LoadScene(onlineRoomName);
        }
    }
}