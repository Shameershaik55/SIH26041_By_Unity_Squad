using UnityEngine;

public class FireTrainingManager : MonoBehaviour
{
    [Header("All Fires")]
    public FireController[] fires;

    [Header("Success UI")]
    public GameObject successPanel;

    private bool trainingCompleted = false;

    private void Start()
    {
        if (successPanel != null)
            successPanel.SetActive(false);
    }

    public void CheckAllFires()
    {
        if (trainingCompleted)
            return;

        foreach (FireController fire in fires)
        {
            if (fire == null)
                continue;

            if (!fire.IsExtinguished())
            {
                return;
            }
        }

        // All fires are extinguished
        trainingCompleted = true;

        if (successPanel != null)
        {
            successPanel.SetActive(true);
        }

        Debug.Log("🔥🔥 ALL FIRES EXTINGUISHED!");
        Debug.Log("🎉 FIRE TRAINING COMPLETED!");
    }
}