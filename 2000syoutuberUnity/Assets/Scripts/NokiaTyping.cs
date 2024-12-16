using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // For TextMeshPro functionality
using UnityEngine.SceneManagement; // For scene transitions

public class NokiaTyping : MonoBehaviour
{
    public TMP_InputField displayInputField; // The TMP_InputField where the text appears
    public TMP_Text debugText; // Optional debug TMP_Text for testing
    public TMP_Text errorMessage; // Error message TextMeshPro element
    public string correctPassword = "EMWAY"; // Correct password to proceed
    public string sceneToLoad = "NextScene"; // Scene to load on success

    private string typedText = ""; // The final typed text
    private string currentLetter = ""; // The currently previewed letter
    private float lastButtonPressTime = 0f; // Time of the last button press
    private int lastKeyPressed = -1; // Tracks the last key pressed
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

        // Check if the key pressed is different from the last key
        if (keyNumber != lastKeyPressed)
        {
            // Commit the current letter immediately if a new key is pressed
            CommitCurrentLetter();
            currentIndex = 0; // Reset index for the new key
        }
        else if (timeSinceLastPress > timeToCycle)
        {
            // If the same key is pressed but time has passed, commit and reset
            CommitCurrentLetter();
            currentIndex = 0; // Reset index for the same key
        }

        // Set the current letter from the key mapping
        currentLetter = keyMappings[keyNumber][currentIndex];

        // After the first press, increment currentIndex for next press
        currentIndex = (currentIndex + 1) % keyMappings[keyNumber].Length;

        // Update tracking variables
        lastKeyPressed = keyNumber;
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

        // Reset last key to avoid incorrect behavior
        lastKeyPressed = -1;
    }

    public void Backspace()
    {
        // Remove the preview letter if it exists
        if (!string.IsNullOrEmpty(currentLetter))
        {
            currentLetter = "";
        }
        // If no preview letter exists, delete the last letter from typedText
        else if (typedText.Length > 0)
        {
            typedText = typedText.Substring(0, typedText.Length - 1);
        }

        UpdateInputFieldPreview();
    }

    private void UpdateInputFieldPreview()
    {
        // Update the TMP_InputField
        displayInputField.text = typedText + currentLetter;
        displayInputField.ForceLabelUpdate(); // Ensure the text is updated visually

        // Optional: Update the debug TMP_Text
        if (debugText != null)
        {
            debugText.text = $"DEBUG: {typedText + currentLetter}";
        }

        Debug.Log($"Preview text updated: {typedText + currentLetter}");
    }

    public void OnOkButtonPress()
    {
        CommitCurrentLetter(); // Commit any previewed letter before checking password

        if (typedText == correctPassword)
        {
            Debug.Log("Password correct! Loading next scene...");
            if (errorMessage != null)
            {
                errorMessage.gameObject.SetActive(false); // Hide error message if it's visible
            }

            SceneManager.LoadScene(sceneToLoad); // Load the next scene
        }
        else
        {
            Debug.Log("Incorrect password!");
            ShowErrorMessage("Incorrect password.");
        }
    }

    private void ShowErrorMessage(string message)
    {
        if (errorMessage != null)
        {
            errorMessage.gameObject.SetActive(true);
            errorMessage.text = message;
        }
    }
}
