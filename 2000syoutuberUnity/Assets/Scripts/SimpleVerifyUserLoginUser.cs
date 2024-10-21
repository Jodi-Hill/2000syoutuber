using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Import TextMeshPro namespace
using System.IO;  // For reading the file

public class SimpleVerifyUserLoginUser : MonoBehaviour
{
    public TMP_InputField usernameField;  // Drag the TextMeshPro username InputField here
    public TMP_InputField passwordField;  // Drag the TextMeshPro password InputField here
    public string filepath = "StreamingAssets/logins.txt";  // Path to your login file in StreamingAssets

    // Called when the login button is pressed
    public void OnLoginButtonPress()
    {
        string username = usernameField.text;  // Get the username from the TMP_InputField
        string password = passwordField.text;  // Get the password from the TMP_InputField

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Debug.Log("Please enter both username and password.");
            return;
        }

        // Verify the login credentials and open the corresponding URL if successful
        if (VerifyLoginAndRedirect(username, password))
        {
            Debug.Log("Redirecting...");
        }
        else
        {
            Debug.Log("Invalid username or password.");
        }
    }

    // Function to verify login credentials and redirect to a specific URL
    public bool VerifyLoginAndRedirect(string username, string password)
    {
        string fullPath = Path.Combine(Application.dataPath, "StreamingAssets/logins.txt");
        Debug.Log("Looking for login file at: " + fullPath);

        // Check if the file exists
        if (!File.Exists(fullPath))
        {
            Debug.LogError("Login file not found: " + fullPath);
            return false;
        }

        // Read all lines from the login file
        string[] lines = File.ReadAllLines(fullPath);
        Debug.Log("Login file found. Number of lines: " + lines.Length);

        // Iterate over each line to check the username and password combinations
        foreach (string line in lines)
        {
            string[] field = line.Split(',');

            // Check for username and password combination
            if (field[0].Equals(username) && field[1].Equals(password))
            {
                Debug.Log("Username and password matched.");

                // Link URLs to your username-password combinations
                if (username == "Nathan_Vaughn" && password == "1707")
                {
                    Debug.Log("Forwarding to Nathan's URL.");
                    Application.OpenURL("https://docs.google.com/document/d/e/2PACX-1vRgnVhclM4Doni0g7S1G6P_TTyRpdeFaoPFNMlXf_pRWi89NUrDICNMshe-2SkjRXXcj6ZsNEP9xsgz/pub");
                }
                else if (username == "BenBoy123" && password == "Mazda6")
                {
                    Debug.Log("Forwarding to Ben's URL.");
                    Application.OpenURL("https://docs.google.com/document/d/e/2PACX-1vRhjMK0xRYvYjOaS1fSxvmU4nTURVGU0Ccl7-dmHQfp48P2siawtEGvsj1DeN-qEyZDLHwD3P-eA8BP/pub");
                }
                else if (username == "xxxEmWayxxx" && password == "U2Be")
                {
                    Debug.Log("Forwarding to Emily's URL.");
                    Application.OpenURL("https://docs.google.com/document/d/e/2PACX-1vQaOjD6qo3Yvb6hEaJj-HenWU345MeeIgkqgZmG-D6X8WGXfg4oV3SVjOfdWZMMrvOleYpY7Ffewp91/pub");
                }
                else
                {
                    Debug.Log("No specific URL match found, forwarding to default URL.");
                    Application.OpenURL("https://yourwebsite.com/default");  // Optional: default URL
                }

                return true;  // Credentials were correct, and a URL was opened
            }
        }

        Debug.Log("No matching username and password found.");
        return false;  // Credentials did not match
    }
}
