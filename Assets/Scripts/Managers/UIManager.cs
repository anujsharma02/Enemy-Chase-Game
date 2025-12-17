using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject rewardScreen;
    [SerializeField] private Text rewardText;
    [SerializeField] private Text timerText;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button playAgainButton;
     [SerializeField] private Button exitButton;

 private void Awake()
    {
      // ✅ Assign callbacks via code (NO Inspector)
        playButton.onClick.AddListener(() => PlayGame(isRestart: false));
        playAgainButton.onClick.AddListener(() => RestartGame());
         exitButton.onClick.AddListener(() => QuitGame());
    }

     private void PlayGame(bool isRestart)
    {
       
            startScreen.SetActive(false);
        GameManager.Instance.StartGame();
    }

 // RESTART GAME (SCENE RELOAD)
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Quit Game
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowRewardScreen(bool won)
    {
        rewardScreen.SetActive(true);
        rewardText.text = won ? "You Won!" : "Pig Escaped!";
    }

    public void UpdateTimer(float timeLeft)
    {
        timerText.text = "Time Left: " + Mathf.CeilToInt(timeLeft);
    }

}
