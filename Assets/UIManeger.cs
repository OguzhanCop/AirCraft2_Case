using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManeger : MonoBehaviour
{
    public FixedJoystick joystick;
    public Slider accerelator;
    public GameData gameData;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI speedoMeterText;
    public GameObject buttonPanel;
    public GameObject controllPanel;
    public GameObject indicatorsPanel;
    public GameObject winPanel;
    public GameObject losePanel;


    private void Start()
    {
        gameData.checkPointCounter = 0;
        gameData.planeAngleCheck = false;
        gameData.planeFlying = false;
       gameData.speed=0;
}
    private void OnEnable()
    {
        EventManager.scoreChange += ScoreChange;
       EventManager.losePanel += LosePanel; 
    }
    private void OnDisable()
    {
        EventManager.scoreChange -= ScoreChange;
        EventManager.losePanel -= LosePanel;
    }
    void Update()
    {
        JoystickControll();
        AccerelationSlider();
        SpeedIndicator();
        
    }

    void JoystickControll()
    {
        if (joystick.Horizontal != 0 && gameData.planeFlying)
        {
            EventManager.planeHorizontalControll.Invoke(joystick.Horizontal);
            EventManager.planeRotationIndicator.Invoke(joystick.Horizontal);

        }
        else
        {
            EventManager.planeHorizontalBaseRotation.Invoke();
            EventManager.planeRotationIndicatorBaseAngle.Invoke();
        }
        if (joystick.Vertical != 0 && gameData.planeFlying)
        {
            EventManager.planeVerticalControll.Invoke(joystick.Vertical);

        }
        else
        {
            EventManager.planeVerticalBaseRotation.Invoke();
        }

    }
    void AccerelationSlider()
    {
        EventManager.speedControll.Invoke(accerelator.value);

    }
    void ScoreChange()
    {

        scoreText.text = "" + gameData.score;
    }
    void SpeedIndicator()
    {
        speedoMeterText.text = "" + gameData.speed;
    }
    public void PauseButton()
    {
        buttonPanel.gameObject.SetActive(true);
        Time.timeScale = 0;

    }
    public void PlayButton()
    {
        buttonPanel.gameObject.SetActive(false);
        Time.timeScale = 1;

    }
    public void QuitButton()
    {
        Application.Quit();

    }
    public void RestartButton()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    void LosePanel()
    {
        indicatorsPanel.gameObject.SetActive(false);
        controllPanel.gameObject.SetActive(false);
        StartCoroutine(LoseGame());

    }
    IEnumerator LoseGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        losePanel.gameObject.SetActive(true);
        Time.timeScale = 0;

    }
}
