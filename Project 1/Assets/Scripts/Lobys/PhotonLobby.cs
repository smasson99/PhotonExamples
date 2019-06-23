using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Lobys
{
    public class PhotonLobby : MonoBehaviourPunCallbacks
    {
        private static PhotonLobby lobby;

        public static PhotonLobby Lobby => lobby;

        public GameObject cancelButton;
        public GameObject battleButton;
        
        private void Awake()
        {
            if (lobby == null)
            {
                lobby = this;
            }
        }

        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to master...");
            battleButton.SetActive(true);
        }

        public void OnJoinRoomButtonClicked()
        {
            Debug.Log("Trying to join a random room...");
            battleButton.SetActive(false);
            cancelButton.SetActive(true);
            PhotonNetwork.JoinRandomRoom();
        }

        public void OnCancelButtonClicked()
        {
            Debug.Log("Leaving the room...");
            cancelButton.SetActive(false);
            battleButton.SetActive(true);
            PhotonNetwork.LeaveRoom();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("Joining a random room failed, now creating one...");
            CreateRoom();
        }

        private static void CreateRoom()
        {
            Debug.Log("Creating a room...");
            
            int randomRoomName = Random.Range(0, 1000);

            RoomOptions roomOptions = new RoomOptions()
            {
                IsVisible = true, IsOpen = true, MaxPlayers = 5
            };

            PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOptions);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log("Failed to create a room, retrying...");
            CreateRoom();
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Joined a room!!!");
            Debug.Log("PlayerID = " + PhotonNetwork.LocalPlayer.UserId);
            Debug.Log("Number of players : " + PhotonNetwork.CurrentRoom.Players.Count);
        }
    }
}
