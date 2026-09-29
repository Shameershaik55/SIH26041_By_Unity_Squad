using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainingDoor : MonoBehaviour
{
    [Header("Next Scene")]
    public string nextSceneName;

    [Header("PPE Manager")]
    public PPETrainingManager ppeManager;

    public void OpenDoor()
    {
        if (ppeManager == null)
        {
            Debug.LogError("PPE Training Manager is not assigned.");
            return;
        }

        if (ppeManager.AllPPECollected())
        {
            Debug.Log("PPE complete. Entering next training scene.");

            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.Log("PPE incomplete.");

            ppeManager.ShowMissingPPEMessage();
        }
    }
}