using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomOnScreenKeyboard : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("Arraste aqui o InputField que esse teclado deve manipular (ex: userNameInputField).")]
    [SerializeField] private TMP_InputField targetInputField;

    [Header("Keys (arraste manualmente A-Z e 0-9)")]
    [Tooltip("Cada botão deve ter um TextMeshProUGUI filho com o caractere da tecla (ex: 'a', 'b', '1'...)")]
    [SerializeField] private List<Button> keyButtons;

    [Header("Special Keys")]
    [SerializeField] private Button backspaceButton;
    [SerializeField] private Button spaceButton;
    [SerializeField] private Button capslockButton;
    private Color capsLockButtonDefaultColor;

    [Header("Settings")]
    [SerializeField] private int maxCharacters = 12;

    private bool isCapsActive = false;

    private void Awake()
    {
        foreach (var btn in keyButtons)
        {
            Button capturedBtn = btn;
            capturedBtn.onClick.AddListener(() => OnKeyPressed(capturedBtn));
        }

        if (backspaceButton != null) backspaceButton.onClick.AddListener(OnBackspace);
        if (spaceButton != null) spaceButton.onClick.AddListener(OnSpace);
        if (capslockButton != null) capslockButton.onClick.AddListener(ToggleCapslock);
    }

    private void Start()
    {
        capsLockButtonDefaultColor = capslockButton.GetComponent<Outline>().effectColor;
    }

    private void OnKeyPressed(Button pressedButton)
    {
        if (targetInputField == null) return;

        TextMeshProUGUI label = pressedButton.GetComponentInChildren<TextMeshProUGUI>();
        if (label == null) return;

        if (!CanInsertCharacter()) return;

        string character = label.text;
        targetInputField.text += isCapsActive ? character.ToUpper() : character.ToLower();
    }

    private void OnBackspace()
    {
        if (targetInputField == null || targetInputField.text.Length == 0) return;
        targetInputField.text = targetInputField.text.Substring(0, targetInputField.text.Length - 1);
    }

    private void OnSpace()
    {
        if (targetInputField == null) return;
        if (!CanInsertCharacter()) return;
        targetInputField.text += " ";
    }

    private void ToggleCapslock()
    {
        isCapsActive = !isCapsActive;
        capslockButton.GetComponent<Outline>().effectColor = isCapsActive ? Color.green : capsLockButtonDefaultColor;
    }

    private bool CanInsertCharacter()
    {
        return targetInputField.text.Length < maxCharacters;
    }
}
