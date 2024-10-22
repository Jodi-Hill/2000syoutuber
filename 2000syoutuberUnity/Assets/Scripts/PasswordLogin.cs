using System.Collections;
using UnityEngine;
using TMPro;  // Import TextMeshPro namespace

public class PasswordLogin : MonoBehaviour
{
    public TMP_InputField passwordField;  // Drag the TextMeshPro password InputField here
    public TMP_Text errorMessage;  // Drag your TextMeshPro error message object here (Make sure it's disabled by default)
    private string correctPassword = "1999";  // The correct password
    private string correctURL = "https://docs.google.com/document/d/e/2PACX-1vQtak0-1CJKRZfumLrjXskovTlpUAWOQsXkeU2_HasQSEDw1qN993fDkgUtB47GfgmYn7eUimJijxfb/pub";  // The URL to open

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

        // Verify the password and open the URL if correct
        if (VerifyPasswordAndRedirect(password))
        {
            Debug.Log("Redirecting...");
        }
        else
        {
            ShowErrorMessage("Invalid password.");
        }
    }

    // Function to verify the password and redirect to the correct URL
    public bool VerifyPasswordAndRedirect(string password)
    {
        if (password == correctPassword)
        {
            Debug.Log("Correct password, opening URL...");
            Application.OpenURL(correctURL);
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
