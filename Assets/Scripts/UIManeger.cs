using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

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
    public GameObject tutorialPanel;
    public GameObject tutorialHandle;
    public bool tutorialActive=true;


    private void Start()
    {
        scoreText.text = "" + gameData.score;
        gameData.checkPointCounter = 0;
        gameData.planeAngleCheck = false;
        gameData.planeFlying = false;
       gameData.speed=0;
        Tutorial();

    }
    private void OnEnable()
    {
        EventManager.scoreChange += ScoreChange;
       EventManager.losePanel += LosePanel;
        EventManager.winPanel += WinPanel;
    }
    private void OnDisable()
    {
        EventManager.scoreChange -= ScoreChange;
        EventManager.losePanel -= LosePanel;
        EventManager.winPanel -= WinPanel;
    }
    void Update()
    {
        JoystickControll();
        AccerelationSlider();
        SpeedIndicator();
        
    }

    void JoystickControll()
    {
        if (gameData.planeFlying)
        {
            if (joystick.Horizontal != 0 )
            {
                EventManager.planeHorizontalControll.Invoke(joystick.Horizontal);
                EventManager.planeRotationIndicator.Invoke(joystick.Horizontal);

            }
            else
            {
                EventManager.planeHorizontalBaseRotation.Invoke();
                EventManager.planeRotationIndicatorBaseAngle.Invoke();
            }
            if (joystick.Vertical != 0 )
            {
                EventManager.planeVerticalControll.Invoke(joystick.Vertical);

            }
            else
            {
                EventManager.planeVerticalBaseRotation.Invoke();
            }
        }
       

    }
    void AccerelationSlider()
    {
        EventManager.speedControll.Invoke(accerelator.value);

    }
    void Tutorial()
    {
        if (gameData.speed == 0)
        {
            tutorialHandle.transform.localPosition = new Vector3(tutorialHandle.transform.localPosition.x, -424.4F, tutorialHandle.transform.localPosition.z);
            tutorialHandle.transform.DOLocalMoveY(tutorialHandle.transform.localPosition.y + 450, 2f).OnComplete(() => Tutorial());

        }
        else
            tutorialPanel.gameObject.SetActive(false);
        

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
        controllPanel.gameObject.SetActive(false);
        Time.timeScale = 0;

    }
    public void PlayButton()
    {
        buttonPanel.gameObject.SetActive(false);
        controllPanel.gameObject.SetActive(true);
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
    void WinPanel()
    {
        indicatorsPanel.gameObject.SetActive(false);
        controllPanel.gameObject.SetActive(false);
        StartCoroutine(WinGame());

    }
    IEnumerator WinGame()
    {
        yield return new WaitForSecondsRealtime(4f);
        winPanel.gameObject.SetActive(true);
        Time.timeScale = 0;

    }
}
