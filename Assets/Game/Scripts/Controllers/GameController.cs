using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Doozy.Engine;
using UnityEngine;
using Utilities;

public enum GameState {
    Menu,
    Playing
}

public class GameController : SingletonMonoBehaviour<GameController> {

    Coroutine changeGem;
    Coroutine gameOver;

    [Header("Camera Settings")]
    public float cameraWidth = 7;
    public bool autoCameraWidth;
    public GameObject bg;
    public GameObject gemMenu;
    BaseGem gem;


    [Header("Game Settings")]
    public float swapSpeed;
    public float fallSpeed;
    public bool preventInitialMatches;
    [SerializeField] private AchiveManager _achiveManager;

    [Header("Score Data")]

    [SerializeField]
    int _score;
    [SerializeField]

    int _winScore;
    int _totalKnocked;

    public static int winScore
    {
        get => instance._winScore;
        set => instance._winScore = value;
    }
    public static int totalKnocked {
        get { return instance._totalKnocked; }
        set {
            instance._totalKnocked = value;
            UIController.UpdateTotalKnocked(instance._totalKnocked);
        }
    }
    public static int score {
        get { return instance._score; }
        set {
            UIController.UpdateComboScore(
                value - instance._score, BoardController.matchCounter
            );
            instance._score = value;
            UIController.UpdateScore(instance._score);
            CheckWin(instance._score);
            if(value > highscore)
                highscore = value;
        }
    }

    private bool isWin;
    public static void CheckWin(int score)
    {
        if (instance.isWin) return;
        if (score < winScore) return;
        instance.isWin = true;
        LevelCounter.AddLevel("Game1");
        GameEventMessage.SendEvent("Win");
        instance.CloseGame();
    }
    public static int highscore {
        get { return PlayerPrefs.GetInt("match3-highscore", 0); }
        set {
            PlayerPrefs.SetInt("match3-highscore", value);
            UIController.UpdateHighScore(value);
        }
    }

    [SerializeField]
    int _currentGoalScore;

    public static int currentGoalScore {
        get { return instance._currentGoalScore; }
        set {
            instance._currentGoalScore = value;
            UIController.UpdateGoalScore(instance._currentGoalScore);
        }
    }

    [SerializeField]
    float _timeLeft;
    public static float timeLeft {
        get { return instance._timeLeft; }
        set {
            instance._timeLeft = Mathf.Max(value, 0);
            UIController.UpdateTimeLeft(instance._timeLeft);
        }
    }

    public static GameState state = GameState.Menu;

    [SerializeField]
    GameData gameData;

    void Start() {
        
        _winScore = _levelCounter.Level * 20;
        if(autoCameraWidth)
            cameraWidth = BoardController.width + (Camera.main.aspect * 3f);
        
        Miscellaneous.SetCameraOrthographicSizeByWidth(Camera.main, cameraWidth);
        float bgWidth = bg.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        float bgHeight = bg.GetComponent<SpriteRenderer>().sprite.bounds.size.y;

        bg.transform.localScale = Vector3.one * (
            Mathf.Max(
                cameraWidth / bgWidth,
                Camera.main.orthographicSize * 2 / bgHeight
            )
        );

        gemMenu.transform.localScale = Vector3.one * 2 * (cameraWidth / 7f);
        gem = gemMenu.GetComponentInChildren<BaseGem>();

        StartGame();
    }

    [SerializeField] private LevelCounter _levelCounter;
    void Update()
    {
       
        if(state == GameState.Playing) {
            timeLeft -= Time.deltaTime;
            if(score >= currentGoalScore) {
                currentGoalScore += currentGoalScore + currentGoalScore/2;
                timeLeft += 120;
            }

            if(timeLeft <= 0) {
                GameOver();
            }
        }
    #if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(BoardController.instance.ShuffleBoard());
        }

        if(Input.GetKeyDown(KeyCode.H)) {
            if(!HintController.isShowing)
                HintController.ShowHint();
            else
                HintController.StopCurrentHint();
        }
    #endif
    }

    public void StartGame() {
        StartCoroutine(IEStartGame());
    }

    IEnumerator IEStartGame() {
        score = 0;
        currentGoalScore = 50;
        timeLeft = 120;
        BoardController.matchCounter = 0;
        UIController.ShowGameScreen();
        yield return new WaitForSeconds(1f);

        TouchController.cancel = true;
        yield return new WaitForSeconds(BoardController.CreateBoard());
        state = GameState.Playing;
        BoardController.UpdateBoard();
     //   instance._achiveManager.ConfigurateAchive();

    }
    
    public void CheckCurrentAchive(GemType type)
    {
        //_achiveManager.CheckAchive(type);
    }

    void GameOver() {
        if(gameOver == null)
            gameOver = StartCoroutine(IEGameOver());
    }

    public void CloseGame()
    {
        StartCoroutine(IEGameClose());
    }
    
    IEnumerator IEGameClose() {

        yield return new WaitUntil(() => !BoardController.updatingBoard);

        TouchController.cancel = true;
        state = GameState.Menu;
        HintController.StopCurrentHint();
        HintController.StopHinting();
//        instance._achiveManager.HideCells();
        UIController.OpenGameOverView(score);
        //UIController.ShowMsg("Game Over");
        yield return new WaitForSeconds(BoardController.DestroyGems() + .5f);
       // UIController.ShowMainScreen();
        gameOver = null;
    }

    IEnumerator IEGameOver() {

       // yield return new WaitUntil(() => !BoardController.updatingBoard);

        if(timeLeft > 0) {
            gameOver = null;
            yield break;
        }

        TouchController.cancel = true;
        state = GameState.Menu;
        HintController.StopCurrentHint();
        HintController.StopHinting();


        Lose();
    }

    private void Lose()
    {
        GameEventMessage.SendEvent("Lose");
        Debug.Log("Lose"); //TODO
    }
    

    public static void ShowGemMenu(bool show = true)
    {
        if(show) {
            instance.changeGem = instance.StartCoroutine(instance.IEChangeGem());
        } else {
            instance.gem.Matched();
            if(instance.changeGem != null) {
                instance.StopCoroutine(instance.changeGem);
                instance.changeGem = null;            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator IEChangeGem() {
        gemMenu.gameObject.SetActive(true);
        gem.SetType(GameData.RandomGem());
        
        yield return new WaitForSeconds(gem.Creating() * 3);

        yield return new WaitForSeconds(gem.Matched());
        
        changeGem = StartCoroutine(IEChangeGem());
    }
}
