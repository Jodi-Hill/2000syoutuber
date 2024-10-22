using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.Networking;
using System.Runtime.InteropServices;

public class SimpleVerifyUserLoginUser : MonoBehaviour
{
    public TMP_InputField usernameField;  // Drag the TextMeshPro username InputField here
    public TMP_InputField passwordField;  // Drag the TextMeshPro password InputField here
    public TMP_Text errorMessage;  // Drag your TextMeshPro error message object here (Make sure it's disabled by default)
    private string filePath;  // Path to the logins.txt file

    // Called when the login button is pressed
    public void OnLoginButtonPress()
    {
        string username = usernameField.text;  // Get the username from the TMP_InputField
        string password = passwordField.text;  // Get the password from the TMP_InputField

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ShowErrorMessage("Please enter both username and password.");
            return;
        }

        // Hide any previous error message before checking login credentials
        HideErrorMessage();

        // Set the path to the logins.txt file in the StreamingAssets folder
        filePath = Application.streamingAssetsPath + "/logins.txt";

        // Start coroutine to load and verify login credentials from the local file
        StartCoroutine(LoadLoginData(username, password));
    }

    // Function to load login data from the StreamingAssets folder and verify credentials
    IEnumerator LoadLoginData(string username, string password)
    {
        Debug.Log("Attempting to load login data from: " + filePath);

        // Load the file using UnityWebRequest for WebGL
        UnityWebRequest request = UnityWebRequest.Get(filePath);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to load login data: " + request.error);
            ShowErrorMessage("Failed to load login data.");
            yield break;
        }

        string[] lines = request.downloadHandler.text.Split('\n');
        bool loginSuccess = false;

        for (int i = 0; i < lines.Length; i++)
        {
            string[] field = lines[i].Split(',');

            // Check if the username and password match
            if (field.Length >= 2 && field[0].Equals(username) && field[1].Trim().Equals(password))
            {
                loginSuccess = true;

                // Link URLs to your username-password combinations
                if (username == "Nathan_Vaughn" && password == "1707")
                {
                    Debug.Log("Redirecting to Nathan's URL.");
                    OpenURL("https://docs.google.com/document/d/e/2PACX-1vRgnVhclM4Doni0g7S1G6P_TTyRpdeFaoPFNMlXf_pRWi89NUrDICNMshe-2SkjRXXcj6ZsNEP9xsgz/pub");
                }
                else if (username == "BenBoy123" && password == "Mazda6")
                {
                    Debug.Log("Redirecting to Ben's URL.");
                    OpenURL("https://docs.google.com/document/d/e/2PACX-1vRhjMK0xRYvYjOaS1fSxvmU4nTURVGU0Ccl7-dmHQfp48P2siawtEGvsj1DeN-qEyZDLHwD3P-eA8BP/pub");
                }
                else if (username == "xxxEmWayxxx" && password == "U2Be")
                {
                    Debug.Log("Redirecting to Emily's URL.");
                    OpenURL("https://docs.google.com/document/d/e/2PACX-1vQaOjD6qo3Yvb6hEaJj-HenWU345MeeIgkqgZmG-D6X8WGXfg4oV3SVjOfdWZMMrvOleYpY7Ffewp91/pub");
                }
                else
                {
                    Debug.Log("Redirecting to default URL.");
                    OpenURL("https://yourwebsite.com/default");
                }

                yield break;  // End the coroutine after a successful login
            }
        }

        // If no match was found, show error message
        if (!loginSuccess)
        {
            ShowErrorMessage("Invalid username or password.");
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

    // Function to open a URL for WebGL builds
    public static void OpenURL(string url)
    {
#if !UNITY_EDITOR && UNITY_WEBGL
            OpenTab(url);
#else
        Application.OpenURL(url);  // Fallback for non-WebGL builds
#endif
    }

    // WebGL specific function to open a new browser tab
    [DllImport("__Internal")]
    private static extern void OpenTab(string url);
}
