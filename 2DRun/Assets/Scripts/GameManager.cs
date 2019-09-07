using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string playerName = "Unknown";

    [System.NonSerialized]
    public int currentStageNum = 0; //現在のステージ番号( Title : 1 , GameMode : 2 )

    public string[] stageName; //ステージ名

    //Prefabを読み込む
    public GameObject titleCanvas;       //タイトル
    public GameObject gameOverCanvas;    //ゲームオーバー
    public GameObject rankingCanvas;     //ランキング
    public GameObject settingCanvas;     //設定
    public GameObject fadeCanvasPrefab;  //フェード
    public GameObject renameCanvas;      //名前入力

    //生成したクローン用
    GameObject titleCanvasClone;
    GameObject gameOverCanvasClone;
    GameObject rankingCanvasClone;
    GameObject settingCanvasClone;
    GameObject fadeCanvasClone;
    GameObject worldRankingCanvasClone;
    GameObject renameCanvasClone;

    public float fadeWaitTime = 1.0f; //フェード時の待ち時間
    FadeCanvas fadeCanvas; //フェード用キャンバス

    Button[] button;

    bool ranking = true;      //ランキング画面切り替え
    bool setting = true;      //設定画面切り替え
    bool rename = true;       //名前入力画面切り替え

    //ゲームモードでコンポーネント取得
    GameObject player;
    PlayerControl playerControl;
    Rigidbody playerRigidbody;
    Collider playerCollider;
    StageCreate stageCreate;
    StageMove stageMove;

    // Start is called before the first frame update
    void Start()
    {
        //シーンを切り替えてもこのゲームオブジェクトを削除しないようにする
        DontDestroyOnLoad(gameObject);

        MoveToStage(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //任意のステージに移動する処理
    public void MoveToStage(int stageNum)
    {
        Time.timeScale = 1f;

        currentStageNum = stageNum;
        //コルーチンを実行
        StartCoroutine(WaitForLoadScene(stageNum));
    }

    //シーンの読み込みと待機を行うコルーチン
    IEnumerator WaitForLoadScene(int stageNum)
    {
        //フェードオブジェクトを生成
        fadeCanvasClone = Instantiate(fadeCanvasPrefab);

        //コンポーネントを取得
        fadeCanvas = fadeCanvasClone.GetComponent<FadeCanvas>();

        //フェードインさせる
        fadeCanvas.fadeIn = true;

        yield return new WaitForSeconds(fadeWaitTime);

        //シーンを非同期で読込し、読み込まれるまで待機する
        yield return SceneManager.LoadSceneAsync(stageName[stageNum]);

        //フェードアウトさせる
        fadeCanvas.fadeOut = true;

        //読み込んだシーンがタイトルなら
        if(currentStageNum == 1)
        {
            TitleDisplay();
        }

        //読み込んだシーンがゲーム画面なら
        if(currentStageNum == 2)
        {
            //Playerのコンポーネント取得
            player = GameObject.FindGameObjectWithTag("Player");
            playerControl = player.GetComponent<PlayerControl>();
            playerRigidbody = player.GetComponent<Rigidbody>();
            playerCollider = player.GetComponent<Collider>();

            stageCreate = GameObject.Find("StageCreater").GetComponent<StageCreate>();
            stageMove = GameObject.Find("Floor").GetComponent<StageMove>();
        }
    }

    //タイトル
    void Title()
    {
        MoveToStage(1);//タイトルに移行
    }

    //ゲームモード
    void GameMode()
    {
        MoveToStage(2);//ゲームモードに移行
    }

    //タイトル画面表示
    public void TitleDisplay()
    {
        //タイトル画面生成
        titleCanvasClone = Instantiate(titleCanvas);

        // ボタンを取得
        button = titleCanvasClone.GetComponentsInChildren<Button>();

        //ボタンにイベント設定
        button[0].onClick.AddListener(Ranking);
        button[1].onClick.AddListener(GameMode);
        button[2].onClick.AddListener(Setting);
        button[3].onClick.AddListener(ExitGame);
    }

    //ランキング表示
    public void Ranking()
    {
        //表示
        if (ranking)
        {
            //ランキング画面生成
            rankingCanvasClone = Instantiate(rankingCanvas);

            //ボタンを取得
            button = rankingCanvasClone.GetComponentsInChildren<Button>();

            //ボタンにイベント設定
            button[0].onClick.AddListener(Ranking);

            ranking = false;
        }

        //非表示
        else
        {
            //ランキング画面削除
            Destroy(rankingCanvasClone);

            ranking = true;
        }

    }



    //設定表示
    public void Setting()
    {
        //表示
        if (setting)
        {
            //設定画面生成
            settingCanvasClone = Instantiate(settingCanvas);

            //ボタンを取得
            button = settingCanvasClone.GetComponentsInChildren<Button>();

            //ボタンにイベント設定
            button[0].onClick.AddListener(Setting);
            button[1].onClick.AddListener(Rename);
            button[2].onClick.AddListener(Setting);

            setting = false;
        }

        //非表示
        else
        {
            //設定画面削除
            Destroy(settingCanvasClone);

            setting = true;
        }
        
    }

    //ゲーム終了
    public void ExitGame()
    {
        //2.0秒後に呼び出す
        Invoke("Finish", 2.0f);
    }

    //アプリ終了
    void Finish()
    {
        Application.Quit();
    }

    //ゲームオーバー
    public void GameOver()
    {
        //キャラ停止・判定削除
        playerControl.enabled = false;
        Destroy(playerRigidbody);

        //ステージ生成停止
        stageCreate.enabled = false;
        stageMove.enabled = false;

        Destroy(player.gameObject);
        GameObject bulletCreater = GameObject.Find("BulletCreater");
        Destroy(bulletCreater);

        Time.timeScale = 0f;

        //ゲームオーバー画面生成
        gameOverCanvasClone = Instantiate(gameOverCanvas);

        //ボタンを取得
        button = gameOverCanvasClone.GetComponentsInChildren<Button>();

        //ボタンにイベント設定
        button[0].onClick.AddListener(Title);
        button[1].onClick.AddListener(Retry);
        //button[2].onClick.AddListener(Ranking);

        ScoreSave();
    }

    //リトライ
    void Retry()
    {
        //ゲームオーバー画面削除
        Destroy(gameOverCanvasClone);

        //ゲームモード再読み込み
        MoveToStage(2);
    }

    //スコア保存・表示
    void ScoreSave()
    { 
        //スコア取得
        int score = GameObject.Find("ScoreCounter").GetComponent<ScoreCount>().score;

        //スコア表示
        Text scoreText = gameOverCanvasClone.transform.FindChild("Score").GetComponent<Text>();
        scoreText.text = "Score : " + score;

        //ランキング保存
        QuickRanking.Instance.SaveRanking(playerName, score);

        //ハイスコアの更新
        HighScoreManager HSM = GameObject.Find("HighScoreManager").GetComponent<HighScoreManager>();
        HSM.newScore = score;
        HSM.HighScore();

        //ハイスコア取得
        int highScore = HSM.highScore;

        //ハイスコア表示
        Text highScoreText = gameOverCanvasClone.transform.FindChild("HighScore").GetComponent<Text>();
        highScoreText.text = "HighScore : " + highScore;
    }

    //名前入力画面の表示
    public void Rename()
    {
        if (rename)
        {
            renameCanvasClone = Instantiate(renameCanvas);

            //ボタンを取得
            button = renameCanvasClone.GetComponentsInChildren<Button>();

            //ボタンにイベント設定
            button[1].onClick.AddListener(Rename);

            rename = false;
        }
        else
        {
            Destroy(renameCanvasClone);

            rename = true;
        }
    }
}
