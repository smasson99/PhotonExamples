using System;
using Photon.Pun;
using Photon.Realtime;
using ScriptableObjects.EventChannels.UI;
using ScriptableObjects.Values;
using TMPro;
using UnityEngine;

namespace UI.Chatbox
{
    public class ChatboxUI : MonoBehaviourPunCallbacks, IPunObservable
    {
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
            SendOfflineMessage($"{PhotonNetwork.LocalPlayer.NickName} joined!");
        }

        [PunRPC]
        private void AddLine(string usernameText, string message)
        {
            chatBoxText.text += $"{usernameText} {message} {Environment.NewLine}";
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
            SendMessage(UserNameText, chatboxInputFieldValue.Value);
        }

        public void OnSubmitted()
        {
            SendMessage(UserNameText, chatboxInputFieldValue.Value);
        }
        
        #region:PhotonEvents
        
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

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            if (!newPlayer.IsLocal)
            {
                SendOfflineMessage($"{newPlayer.NickName} joined the room!");
            }
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            SendOfflineMessage($"{otherPlayer.NickName} left the room :(");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            SendOfflineMessage("Looks like you just got disconnected from the server. Cause : " + cause);
        }

        #endregion

        #region ChatboxFunctions
        
        private void SendMessage(string usernameText, string message)
        {
            if (!chatboxInputFieldValue.IsValueNullOrEmpty)
            {
                if (PhotonNetwork.IsConnected)
                {
                    SendOnlineMessage(usernameText, message);
                }
                else
                {
                    SendOfflineMessage(usernameText, message);
                }
            }
        }

        private void SendMessage(string message, RpcTarget target)
        {
            if (PhotonNetwork.IsConnected)
            {
                SendOnlineMessage(message, target);
            }
            else
            {
                SendOfflineMessage(message);
            }
        }

        private void SendOnlineMessage(string usernameText, string message)
        {
            photonView.RPC(nameof(AddLine), RpcTarget.All, usernameText, message);

            NotifyChatboxInputFieldUsed();
        }

        private void SendOfflineMessage(string usernameText, string message)
        {
            AddLine(usernameText, message);
            NotifyChatboxInputFieldUsed();
        }
        
        private void SendOnlineMessage(string message, RpcTarget target)
        {
            photonView.RPC(nameof(AddLine), target, "", message);
        }

        private void SendOfflineMessage(string message)
        {
            AddLine("", message);
        }
        
        #endregion
    }
}