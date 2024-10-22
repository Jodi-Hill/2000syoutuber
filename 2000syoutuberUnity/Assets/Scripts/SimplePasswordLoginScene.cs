using System.Collections;
using UnityEngine;
using TMPro;  // Import TextMeshPro namespace
using UnityEngine.SceneManagement;  // For scene loading

public class SimplePasswordLoginScene : MonoBehaviour
{
    public TMP_InputField passwordField;  // Drag the TextMeshPro password InputField here
    public TMP_Text errorMessage;  // Drag your TextMeshPro error message object here (Make sure it's disabled by default)
    private string correctPassword = "emway";  // The correct password
    public string sceneToLoad = "NextScene";  // The name of the scene to load

    // Called when the login button is pressed
    public void OnLoginButtonPress()
    {
        string password = passwordField.text;  // Get the password from the TMP_InputField

        if (string.IsNullOrEmpty(password))
        {
            ShowErrorMessage("Please enter a password.");
            return;
        }

        // Hide any previous error message before checking the password
        HideErrorMessage();

        // Verify the password and load the scene if correct
        if (VerifyPasswordAndLoadScene(password))
        {
            Debug.Log("Loading scene...");
        }
        else
        {
            ShowErrorMessage("Invalid password.");
        }
    }

    // Function to verify the password and load the new scene
    public bool VerifyPasswordAndLoadScene(string password)
    {
        if (password == correctPassword)
        {
            Debug.Log("Correct password, loading scene...");
            SceneManager.LoadScene(sceneToLoad);  // Load the specified scene
            return true;  // Password was correct
        }
        else
        {
            return false;  // Password was incorrect
        }
    }

    // Function to show error message
    void ShowErrorMessage(string message)
    {
        errorMessage.gameObject.SetActive(true);  // Activate the error message object
        errorMessage.text = message;  // Set the error message text
    }

    // Function to hide error message
    void HideErrorMessage()
    {
        errorMessage.gameObject.SetActive(false);  // Deactivate the error message object
    }
}
