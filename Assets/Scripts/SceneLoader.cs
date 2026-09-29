using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void OpenAdmin()
    {
        SceneManager.LoadScene("Navigation");
    }

    public void OpenUser()
    {
        SceneManager.LoadScene("Navigation 1");
    }
}