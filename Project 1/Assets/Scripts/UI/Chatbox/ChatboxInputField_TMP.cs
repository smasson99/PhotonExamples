using System;
using TMPro;
using UnityEngine;

namespace UI.Chatbox
{
    public class ChatboxInputField_TMP : MonoBehaviour
    {
        [SerializeField]
        private StringValue usernameValue = null;

        private TMP_InputField inputField;

        private void Awake()
        {
            inputField = GetComponent<TMP_InputField>();
            
            UpdateInputFieldDefaultValue();
        }

        private void UpdateInputFieldDefaultValue()
        {
            inputField.placeholder.GetComponent<TextMeshProUGUI>().text = usernameValue.Value + " : ...";
        }
    }
}