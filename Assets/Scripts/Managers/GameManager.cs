using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private int tapsToWin = 10;
    [SerializeField] private float meetPointX = 0f;
    [SerializeField] private float maxChaseTime = 25f;

    [SerializeField] private BirdController bird;
    [SerializeField] private PigController pig;
    [SerializeField] private ProgressSliderController progressSlider;
    [SerializeField] private UIManager uiManager;

    private int currentTaps;
    private float timer;

public bool IsgameEnded { get; private set; }
    public bool IsGameStarted { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InputManager.Instance.OnTap += HandleTap;
    }

    private void Update()
    {
        if (!IsGameStarted || IsgameEnded)
            return;

        timer += Time.deltaTime;
        uiManager.UpdateTimer(maxChaseTime - timer);

        if (timer >= maxChaseTime)
        {
            PigWins();
        }
    }

    public void StartGame()
    {
        IsGameStarted = true;
        IsgameEnded = false;
        currentTaps = 0;
        timer = 0f;

        bird.InitMovement(meetPointX);
        pig.InitMovement(meetPointX);
        progressSlider.UpdateProgress(0, tapsToWin);
    }

    private void HandleTap()
    {
        if (!IsGameStarted || IsgameEnded)
            return;

        currentTaps++;

        bird.OnTapMove(currentTaps, tapsToWin);
        pig.OnTapMove(currentTaps, tapsToWin);
        progressSlider.UpdateProgress(currentTaps, tapsToWin);

        if (currentTaps == tapsToWin)
            BirdWins();
    }

    private void BirdWins()
    {
        IsgameEnded = true;

        AudioManager.Instance.StopBackground();
        pig.Defeated();
        bird.JumpOnPig(pig.transform);
        AudioManager.Instance.PlayHappyLoop();

        uiManager.ShowRewardScreen(true);
    }

    private void PigWins()
    {
        IsgameEnded = true;

        AudioManager.Instance.StopBackground();
        pig.Escape();
        uiManager.ShowRewardScreen(false);
    }

}
