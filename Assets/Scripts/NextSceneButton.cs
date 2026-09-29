using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneButton : MonoBehaviour
{
    [Header("Next Scene")]
    public string nextSceneName;

    public void GoToNextScene()
    {
        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogError("Next Scene Name is not assigned!");
            return;
        }

        SceneManager.LoadScene(nextSceneName);
    }
}