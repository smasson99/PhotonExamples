using Photon.Pun;
using ScriptableObjects.EventChannels.PhotonEvents;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Photon.Callbacks
{
    public class ChatRoomCallbacks : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private string offlineRoomName = "Offline";

        [SerializeField]
        private OnLeaveRoomButtonClicked onLeaveRoomButtonClicked = null;

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
            PhotonNetwork.Disconnect();
        }

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(offlineRoomName);
        }
    }
}