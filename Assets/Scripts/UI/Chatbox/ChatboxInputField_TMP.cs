using ScriptableObjects.EventChannels.UI;
using ScriptableObjects.Values;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Chatbox
{
    public class ChatboxInputField_TMP : MonoBehaviour
    {
        private string defaultValue;

        private TMP_InputField inputField;

        [SerializeField]
        private InputFieldUsed onInputFieldUsed = null;

        public UnityEvent OnSubmit;

        [SerializeField]
        private StringValue usernameValue = null;

        private void Awake()
        {
            inputField = GetComponent<TMP_InputField>();
            defaultValue = inputField.text;

            UpdateInputFieldDefaultValue();
        }

        private void OnEnable()
        {
            onInputFieldUsed.OnPublished += OnUsed;
            inputField.onSubmit.AddListener(OnSubmitted);
        }

        private void OnDisable()
        {
            onInputFieldUsed.OnPublished -= OnUsed;
            inputField.onSubmit.RemoveListener(OnSubmitted);
        }

        private void OnSubmitted(string value)
        {
            OnSubmit.Invoke();
            
            inputField.ActivateInputField();
        }

        private void OnUsed()
        {
            inputField.text = defaultValue;
        }

        private void UpdateInputFieldDefaultValue()
        {
            inputField.placeholder.GetComponent<TextMeshProUGUI>().text = usernameValue.Value + " : ...";
        }
    }
}