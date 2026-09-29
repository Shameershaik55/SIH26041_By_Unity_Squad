using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class PPETrainingManager : MonoBehaviour
{
    [Header("Required PPE")]
    public int totalPPE = 4;

    private int collectedPPE = 0;

    [Header("Instruction UI")]
    public TMP_Text instructionText;

    [Header("Error Message")]
    public GameObject errorPanel;
    public TMP_Text errorText;

    [Header("Safety Video")]
    public GameObject safetyVideoDisplay;
    public VideoPlayer errorVideoPlayer;

    private void Start()
    {
        collectedPPE = 0;

        if (errorPanel != null)
            errorPanel.SetActive(false);

        if (safetyVideoDisplay != null)
            safetyVideoDisplay.SetActive(false);

        if (errorVideoPlayer != null)
        {
            errorVideoPlayer.Stop();

            // Detect when video finishes
            errorVideoPlayer.loopPointReached += OnVideoFinished;
        }

        UpdateInstruction();
    }

    public void CollectPPE()
    {
        collectedPPE++;

        Debug.Log(
            "PPE Collected: " +
            collectedPPE +
            "/" +
            totalPPE
        );

        UpdateInstruction();
    }

    public bool AllPPECollected()
    {
        return collectedPPE >= totalPPE;
    }

    public void ShowMissingPPEMessage()
    {
        // Show error panel
        if (errorPanel != null)
            errorPanel.SetActive(true);

        // Show error message
        if (errorText != null)
        {
            errorText.text =
                "⚠ SAFETY CHECK FAILED\n\n" +
                "Please collect all required PPE\n" +
                "before entering the training area.";
        }

        // Show video
        if (safetyVideoDisplay != null)
            safetyVideoDisplay.SetActive(true);

        // Play video
        if (errorVideoPlayer != null)
        {
            errorVideoPlayer.Stop();
            errorVideoPlayer.Play();

            Debug.Log("PPE SAFETY VIDEO STARTED");
        }
    }

    // Called automatically when video reaches the end
    private void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("PPE SAFETY VIDEO FINISHED");

        // Stop video
        vp.Stop();

        // Disable video display
        if (safetyVideoDisplay != null)
        {
            safetyVideoDisplay.SetActive(false);

            Debug.Log("PPE SAFETY VIDEO DISABLED");
        }
    }

    public void CloseErrorPanel()
    {
        // Stop video
        if (errorVideoPlayer != null)
        {
            errorVideoPlayer.Stop();
        }

        // Disable video display
        if (safetyVideoDisplay != null)
        {
            safetyVideoDisplay.SetActive(false);
        }

        // Hide entire error panel
        if (errorPanel != null)
        {
            errorPanel.SetActive(false);
        }

        Debug.Log("PPE ERROR PANEL CLOSED");
    }

    private void UpdateInstruction()
    {
        if (instructionText != null)
        {
            instructionText.text =
                "PPE CHECK\n\n" +
                "Collected: " +
                collectedPPE +
                "/" +
                totalPPE;
        }
    }

    private void OnDestroy()
    {
        if (errorVideoPlayer != null)
        {
            errorVideoPlayer.loopPointReached -= OnVideoFinished;
        }
    }
}