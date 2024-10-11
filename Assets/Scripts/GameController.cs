using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public GameObject losePanel;
    public GameObject winPanel; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        StartCoroutine(CameraController.Instance.SmoothTransitionToLoseCamera(1.0f)); 
        RacingController.Instance.StopMovement();
        TurretShooting.Instance.StopShooting();

        TurretControll turretControll = FindObjectOfType<TurretControll>();
        if (turretControll != null)
        {
            turretControll.isGameOver = true;
        }

        StartCoroutine(ShowGameOverPanel());
    }

    IEnumerator ShowGameOverPanel()
    {
        yield return new WaitForSeconds(5);
        losePanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameFinished()
    {
        StartCoroutine(CameraController.Instance.SmoothTransitionToLoseCamera(1.0f));
        StartCoroutine(StopMovementAndShowWinPanel());
    }

    IEnumerator StopMovementAndShowWinPanel()
    {
        yield return new WaitForSeconds(3);
        RacingController.Instance.StopMovement();
        winPanel.SetActive(true);
    }
}