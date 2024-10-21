using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PasswordCheck : MonoBehaviour
{
    private InputField inputTextField;

    void Start()
    {
        inputTextField = GetComponent<InputField>();

        // Add listener for the submit event
        inputTextField.onEndEdit.AddListener(delegate { CheckPasswordAndLoadScene(); });
    }

    public void CheckPasswordAndLoadScene()
    {
        switch (inputTextField.text)
        {
            case "1707":
                SceneManager.LoadScene("Nathan's social media");
                break;
            case "Mazda6":
                SceneManager.LoadScene("Ben's social media");
                break;
            case "U2Be":
                SceneManager.LoadScene("Emily's social media");
                break;
            case "emway":
                SceneManager.LoadScene("Emily's Phone");
                break;
            case "1999":
                SceneManager.LoadScene("YASA website");
                break;

            default:
                inputTextField.text = ""; // Clear input on wrong password
                break;
        }
    }
}
