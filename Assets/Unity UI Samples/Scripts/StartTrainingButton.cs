using UnityEngine;

public class StartTrainingButton : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject trainingUI;

    public void StartTraining()
    {
        Debug.Log("START TRAINING BUTTON CLICKED");

        if (startPanel != null)
            startPanel.SetActive(false);

        if (trainingUI != null)
            trainingUI.SetActive(true);
    }
}