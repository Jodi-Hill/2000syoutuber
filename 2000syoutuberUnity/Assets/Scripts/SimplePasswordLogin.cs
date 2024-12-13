using UnityEngine;
using TMPro;  // For TextMeshPro
using UnityEngine.SceneManagement;  // For loading scenes

public class SimplePasswordLogin : MonoBehaviour
{
    public TMP_InputField passwordField;  // TMP_InputField for password
    public TMP_Text errorMessage;  // TextMeshPro text object for error message
    public string correctPassword = "emway";  // Correct password
    public string sceneToLoad = "NextScene";  // The scene to load (adjust this name)

    // Called when the login button is pressed
    public void OnLoginButtonPress()
    {
        string enteredPassword = passwordField.text.Trim();  // Get entered password and trim spaces

        Debug.Log("Entered password: '" + enteredPassword + "'");  // Debugging: Log entered password

        if (string.IsNullOrEmpty(enteredPassword))  // Check if the password field is empty
        {
            ShowErrorMessage("Please enter a password.");
            return;
        }

        // Hide error message before checking the password
        HideErrorMessage();

        // Debugging: Log the correct password for comparison (ensure no spaces or hidden characters)
        Debug.Log("Correct password: '" + correctPassword + "'");

        // Check if the entered password matches the correct password in the exact sequence
        if (enteredPassword.Equals(correctPassword))
        {
            Debug.Log("Password is correct! Attempting to load scene...");  // Debugging: Log success message
            SceneManager.LoadScene(sceneToLoad);  // Load the specified scene
        }
        else
        {
            Debug.Log("Incorrect password.");  // Debugging: Log incorrect password message
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
