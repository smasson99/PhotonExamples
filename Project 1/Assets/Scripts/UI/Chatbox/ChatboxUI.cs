﻿using ScriptableObjects.EventChannels.UI;
using TMPro;
using UnityEngine;

namespace UI.Chatbox
{
    public class ChatboxUI : MonoBehaviour
    {
        private const string NewLine = "\n";
        private const string Space = " ";
        
        [SerializeField]
        private StringValue usernameValue = null;
        
        [SerializeField]
        private StringValue chatboxInputFieldValue = null;

        [SerializeField]
        private TMP_Text chatBoxText = null;

        [SerializeField]
        private InputFieldUsed onChatBoxInputFieldUsed = null;

        private void Awake()
        {
            chatboxInputFieldValue.ResetValue();
        }
        
        private void AddLine(string contentString)
        {
            chatBoxText.text += usernameValue.Value + Space + contentString + NewLine;
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
            AddLine(chatboxInputFieldValue.Value);
            NotifyChatboxInputFieldUsed();
        }
    }
}
