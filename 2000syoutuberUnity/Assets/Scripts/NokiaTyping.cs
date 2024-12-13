using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Make sure to import this for TextMeshPro functionality

public class NokiaTyping : MonoBehaviour
{
    public TMP_InputField displayInputField; // The TMP_InputField where the text appears
    public TMP_Text debugText; // Temporary TMP_Text for debugging (assign in Inspector)

    private string typedText = ""; // The final text
    private string currentLetter = ""; // The currently previewed letter
    private float lastButtonPressTime = 0f; // Time of the last button press
    private int currentIndex = 0; // Current position in the key mapping
    private float timeToCycle = 1.0f; // How long to wait before committing the current letter

    private Dictionary<int, string[]> keyMappings = new Dictionary<int, string[]>()
    {
        { 2, new string[] { "A", "B", "C" } },
        { 3, new string[] { "D", "E", "F" } },
        { 4, new string[] { "G", "H", "I" } },
        { 5, new string[] { "J", "K", "L" } },
        { 6, new string[] { "M", "N", "O" } },
        { 7, new string[] { "P", "Q", "R", "S" } },
        { 8, new string[] { "T", "U", "V" } },
        { 9, new string[] { "W", "X", "Y", "Z" } },
        { 0, new string[] { " " } } // Space on 0
    };

    public void OnButtonPress(int keyNumber)
    {
        if (!keyMappings.ContainsKey(keyNumber)) return;

        float timeSinceLastPress = Time.time - lastButtonPressTime;

        // If enough time has passed since the last press, reset the cycle to start from the first letter
        if (timeSinceLastPress > timeToCycle)
        {
            // Commit current letter if any, and reset index to start fresh
            CommitCurrentLetter();
            currentIndex = 0;
        }

        // Set the current letter from the key mapping
        currentLetter = keyMappings[keyNumber][currentIndex];

        // After the first press, increment currentIndex for next press
        currentIndex = (currentIndex + 1) % keyMappings[keyNumber].Length;

        lastButtonPressTime = Time.time;

        // Update the InputField
        UpdateInputFieldPreview();
    }

    private void CommitCurrentLetter()
    {
        if (!string.IsNullOrEmpty(currentLetter))
        {
            typedText += currentLetter;
            currentLetter = "";
            UpdateInputFieldPreview();
        }
    }

    public void Backspace()
    {
        if (typedText.Length > 0)
        {
            typedText = typedText.Substring(0, typedText.Length - 1);
            UpdateInputFieldPreview();
        }
    }

    private void UpdateInputFieldPreview()
    {
        // Update the TMP_InputField
        displayInputField.text = typedText + currentLetter;
        displayInputField.ForceLabelUpdate(); // Ensure the text is updated visually

        // Update the debug TMP_Text
        if (debugText != null)
        {
            debugText.text = $"DEBUG: {typedText + currentLetter}";
        }

        Debug.Log($"Preview text updated: {typedText + currentLetter}");
    }
}
