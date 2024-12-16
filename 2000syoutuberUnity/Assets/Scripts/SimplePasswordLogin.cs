using UnityEngine;
using TMPro;  // For TextMeshPro
using UnityEngine.SceneManagement;  // For loading scenes

public class SimplePasswordLogin : MonoBehaviour
{
    public TMP_InputField passwordField;  // TMP_InputField for password
    public TMP_Text errorMessage;  // TextMeshPro text object for error message
    public string correctPassword = "EMWAY";  // Correct password
    public string sceneToLoad = "NextScene";  // The scene to load (adjust this name)

    // Called when the login button is pressed
    public void OnLoginButtonPress()
    {
        // Get the entered password from the InputField
        string enteredPassword = passwordField.text;

        // Debugging: Log the raw input
        Debug.Log("Raw entered password: '" + enteredPassword + "'");

        // Trim the input to remove leading/trailing spaces
        enteredPassword = enteredPassword.Trim();

        // Debugging: Log the trimmed input
        Debug.Log("Trimmed entered password: '" + enteredPassword + "'");

        // Check if the input is empty
        if (string.IsNullOrEmpty(enteredPassword))
        {
            ShowErrorMessage("Please enter a password.");
            return;
        }

        // Check if the password matches
        if (enteredPassword.Equals(correctPassword))
        {
            Debug.Log("Password is correct! Attempting to load scene...");
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.Log("Incorrect password.");
            ShowErrorMessage("Incorrect password.");
        }
    }

    // Function to show error message
    private void ShowErrorMessage(string message)
    {
        errorMessage.gameObject.SetActive(true);  // Show the error message
        errorMessage.text = message;  // Set the error message text
    }

    // Function to hide error message
    private void HideErrorMessage()
    {
        errorMessage.gameObject.SetActive(false);  // Hide the error message
    }
}
