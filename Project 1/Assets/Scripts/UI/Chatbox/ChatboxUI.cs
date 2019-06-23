using System;
using Photon.Pun;
using ScriptableObjects.EventChannels.UI;
using TMPro;
using UnityEngine;

namespace UI.Chatbox
{
    public class ChatboxUI : MonoBehaviourPun, IPunObservable
    {
        private const string NewLine = "\n";
        private const string Space = " ";

        [SerializeField]
        private StringValue chatboxInputFieldValue = null;

        [SerializeField]
        private TMP_Text chatBoxText = null;

        [SerializeField]
        private InputFieldUsed onChatBoxInputFieldUsed = null;

        private string UserNameText => PhotonNetwork.LocalPlayer.NickName + " : ";

        private void Awake()
        {
            chatboxInputFieldValue.ResetValue();
        }

        private void Start()
        {
            AddLine(UserNameText, "Roomname : " + PhotonNetwork.CurrentRoom.Name);
            AddLine(UserNameText, "NumberOfPlayers : " + PhotonNetwork.PlayerList.Length);
            AddLine(UserNameText, "NumberOfPlayers in the room : " + PhotonNetwork.CountOfPlayersInRooms);
            AddLine(UserNameText, "Full string room : " + PhotonNetwork.CurrentRoom.ToStringFull());
            AddLine(UserNameText, "Number of rooms : " + PhotonNetwork.CountOfRooms);
        }

        [PunRPC]
        private void AddLine(string usernameText, string message)
        {
            Debug.Log("AddlineCalled");
            chatBoxText.text += usernameText + Space + message + NewLine;
        }

        private void NotifyChatboxInputFieldUsed()
        {
            onChatBoxInputFieldUsed.Publish();
        }

        public void OnChatboxInputFieldValueChanged(string newValue)
        {
            chatboxInputFieldValue.Value = newValue;
        }

        public void OnChatboxButtonClicked()
        {
            SendMessage();
        }

        public void OnSubmitted()
        {
            SendMessage();
        }

        private void SendMessage()
        {
            if (!chatboxInputFieldValue.IsValueNullOrEmpty)
            {
                if (PhotonNetwork.IsConnected)
                {
                    Debug.Log("Online");
                    AddALineForAllUsers();
                }
                else
                {
                    Debug.Log("Offline");
                    SendOfflineMessage();
                }
            }
        }

        private void AddALineForAllUsers()
        {
            photonView.RPC(nameof(AddLine), RpcTarget.All, UserNameText, chatboxInputFieldValue.Value);

            NotifyChatboxInputFieldUsed();
        }

        private void SendOfflineMessage()
        {
            AddLine(UserNameText, chatboxInputFieldValue.Value);
            NotifyChatboxInputFieldUsed();
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(chatBoxText.text);
            }
            else if (stream.IsReading)
            {
                chatBoxText.text = stream.ReceiveNext().ToString();
            }
        }
    }
}