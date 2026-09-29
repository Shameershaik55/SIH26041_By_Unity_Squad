using UnityEngine;
using UnityEngine.Video;

public class SafetyVideoController : MonoBehaviour
{
    [Header("Video")]
    public VideoPlayer videoPlayer;

    [Header("UI")]
    public GameObject skipButton;
    public GameObject rewatchButton;

    [Header("Joystick")]
    public VirtualCameraJoystick joystick;

    [Header("Screen")]
    public GameObject videoScreen;

    private bool videoCompleted = false;

    private void Start()
    {
        if (skipButton != null)
            skipButton.SetActive(true);

        if (rewatchButton != null)
            rewatchButton.SetActive(false);

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += VideoFinished;
        }
    }

    public void StartVideo()
    {
        videoCompleted = false;

        if (rewatchButton != null)
            rewatchButton.SetActive(false);

        if (skipButton != null)
            skipButton.SetActive(true);

        if (joystick != null)
            joystick.LockMovement();

        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            videoPlayer.Play();
        }

        Debug.Log("SAFETY VIDEO STARTED");
    }

    public void SkipVideo()
    {
        Debug.Log("SAFETY VIDEO SKIPPED");

        FinishVideo();
    }

    private void VideoFinished(VideoPlayer vp)
    {
        Debug.Log("SAFETY VIDEO FINISHED");

        FinishVideo();
    }

   /* private void FinishVideo()
    {
        if (videoCompleted)
            return;

        videoCompleted = true;

        if (videoPlayer != null)
            videoPlayer.Stop();

        if (skipButton != null)
            skipButton.SetActive(false);

        if (rewatchButton != null)
            rewatchButton.SetActive(true);

        if (joystick != null)
            joystick.UnlockMovement();

        Debug.Log("MOVEMENT UNLOCKED");
    }*/
    private void FinishVideo()
{
    if (videoCompleted)
        return;

    videoCompleted = true;

    // Stop video
    if (videoPlayer != null)
    {
        videoPlayer.Stop();
    }

    // Hide video screen
    if (videoScreen != null)
    {
        videoScreen.SetActive(false);
    }

    // Hide Skip button
    if (skipButton != null)
    {
        skipButton.SetActive(false);
    }

    // Unlock joystick
    if (joystick != null)
    {
        joystick.UnlockMovement();
    }

    // Show Rewatch button
    if (rewatchButton != null)
    {
        rewatchButton.SetActive(true);
    }

    Debug.Log("VIDEO FINISHED");
    Debug.Log("REWATCH BUTTON SHOULD STAY VISIBLE");
}

  /*  public void RewatchVideo()
    {
        Debug.Log("REWATCHING SAFETY VIDEO");

        videoCompleted = false;

        if (rewatchButton != null)
            rewatchButton.SetActive(false);

        if (skipButton != null)
            skipButton.SetActive(true);

        if (joystick != null)
            joystick.LockMovement();

        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            videoPlayer.Play();
        }
    }*/
    public void RewatchVideo()
{
    Debug.Log("REWATCH BUTTON PRESSED");

    videoCompleted = false;

    // Show video screen
    if (videoScreen != null)
    {
        videoScreen.SetActive(true);
    }

    // Hide rewatch button
    if (rewatchButton != null)
    {
        rewatchButton.SetActive(false);
    }

    // Show skip button
    if (skipButton != null)
    {
        skipButton.SetActive(true);
    }

    // Lock movement
    if (joystick != null)
    {
        joystick.LockMovement();
    }

    // Restart video
    if (videoPlayer != null)
    {
        videoPlayer.Stop();
        videoPlayer.Play();
    }
}
}