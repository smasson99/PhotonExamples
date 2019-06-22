using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace OfficialPhotonTutorials.Scripts
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        private const string GameVersion = "1";

        #region:MonobehaviourPunCallbacks

        public override void OnConnectedToMaster()
        {
            Debug.Log("Launcher : OnConnectedToMaster() was called by PUN");
        }

        public override void OnDisconnected(DisconnectCause disconnectCause)
        {
            Debug.LogWarningFormat("Launcher : OnDisconnected() was called by PUN with reason {0}", disconnectCause);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log("Launcher : OnJoinRoomFailed() was called by PUN. No random room available, so we create one.");

            PhotonNetwork.CreateRoom(null, new RoomOptions());
        }

        #endregion

        private void Awake()
        {
            /*Our game will have a resizable arena based on the number
             of players, and to make sure that the loaded scene is the 
             same for every connected player, we'll make use of the very 
             convenient feature provided by Photon: PhotonNetwork.AutomaticallySyncScene 
             When this is true, the MasterClient can call PhotonNetwork.LoadLevel() 
             and all connected players will automatically load that same level.*/
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            Connect();
        }

        private void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.GameVersion = GameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }
    }
}