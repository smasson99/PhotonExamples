using Photon.Pun;
using Photon.Realtime;
using ScriptableObjects.Values;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Photon.Callbacks
{
    public class MainMenuCallbacks : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private StringValue usernameInputFieldValue = null;
        
        [SerializeField]
        [Range(1, 20)]
        private byte maxAmountOfPlayersPeerRoom = 5;

        [SerializeField]
        private string onlineRoomName = "Room";

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
                    IsOpen = true,
                    MaxPlayers = maxAmountOfPlayersPeerRoom
                };

                PhotonNetwork.LocalPlayer.NickName = usernameInputFieldValue.Value;

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