using System.Collections;
using UnityEngine;
using TMPro;  // Import TextMeshPro namespace
using UnityEngine.SceneManagement;  // For scene loading

public class SimplePasswordLogin : MonoBehaviour
{
    public TMP_InputField passwordField;  // Reference to the TMP_InputField for the password
    public TMP_Text errorMessage;  // Reference to the TextMeshPro Text for error message
    public string correctPassword = "secret";  // The correct password
    public string sceneToLoad = "NextScene";  // Name of the scene to load

    // Called when the login button is pressed
    public void OnLoginButtonPress()
    {
        string enteredPassword = passwordField.text;  // Get the password from the TMP_InputField

        if (string.IsNullOrEmpty(enteredPassword))  // Check if the input field is empty
        {
            ShowErrorMessage("Please enter a password.");
            return;
        }

        // Hide the error message before checking the password
        HideErrorMessage();

        // Check if the entered password is correct
        if (enteredPassword == correctPassword)
        {
            // Password is correct, log the debug message and load the new scene
            Debug.Log("Password is correct, loading next scene...");
            SceneManager.LoadScene(sceneToLoad);  // Load the specified scene
        }
        else
        {
            // Password is incorrect, show error message
            ShowErrorMessage("Incorrect password.");
        }
    }

    // Function to show error message
    private void ShowErrorMessage(string message)
    {
        errorMessage.gameObject.SetActive(true);  // Enable the error message text
        errorMessage.text = message;  // Set the error message text
    }

    // Function to hide error message
    private void HideErrorMessage()
    {
        errorMessage.gameObject.SetActive(false);  // Hide the error message text
    }
}
