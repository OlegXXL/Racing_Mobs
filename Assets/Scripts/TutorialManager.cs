using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject startPanel;

    void Start()
    {
        startPanel.gameObject.SetActive(true);
        tutorialPanel.SetActive(false);
        CameraController.Instance.SwitchToStartCamera();
        RacingController.Instance.StopMovement();
        TurretShooting.Instance.StopShooting();
    }

    public void PressStartGame()
    {
        StartCoroutine(StartGame());
    }
    public void PressCloseTutorial()
    {
        CloseTutorial();
    }
    IEnumerator StartGame()
    {
        startPanel.gameObject.SetActive(false);
        yield return StartCoroutine(CameraController.Instance.SmoothTransitionToGameCamera(2.0f));
        tutorialPanel.SetActive(true);
    }

    void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
        RacingController.Instance.StartMovement();
        TurretShooting.Instance.StartShooting();
    }
}