using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [System.NonSerialized]
    public int currentStageNum = 0; //現在のステージ番号( Title : 1 , GameMode : 2 )

    public string[] stageName; //ステージ名

    //Prefabを読み込む
    public GameObject titleCanvas;       //タイトル
    public GameObject gameOverCanvas;    //ゲームオーバー
    public GameObject rankingCanvas;     //ランキング
    public GameObject settingCanvas;     //設定
    public GameObject fadeCanvasPrefab;  //フェード
    public GameObject localRankingCanvas;//ローカルランキング
    public GameObject worldRankingCanvas;//ワールドランキング

    //生成したクローン用
    GameObject titleCanvasClone;
    GameObject gameOverCanvasClone;
    GameObject rankingCanvasClone;
    GameObject settingCanvasClone;
    GameObject fadeCanvasClone;
    GameObject localRankingCanvasClone;
    GameObject worldRankingCanvasClone;

    public float fadeWaitTime = 1.0f; //フェード時の待ち時間
    FadeCanvas fadeCanvas; //フェード用キャンバス

    Button[] button;

    bool ranking = true;      //ランキング画面切り替え
    bool localRanking = true; //world : localの切り替え
    bool setting = true;      //設定画面切り替え

    //ゲームモードでコンポーネント取得
    PlayerControl playerControl;
    Rigidbody playerRigidbody;
    StageCreate stageCreate;
    StageMove stageMove;

    // Start is called before the first frame update
    void Start()
    {
        //シーンを切り替えてもこのゲームオブジェクトを削除しないようにする
        DontDestroyOnLoad(gameObject);

        //MoveToStage(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //任意のステージに移動する処理
    public void MoveToStage(int stageNum)
    {
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
        if(stageNum == 1)
        {
            TitleDisplay();
        }

        //読み込んだシーンがゲーム画面なら
        if(stageNum == 2)
        {
            playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
            playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody>();
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
            button[1].onClick.AddListener(LocalRanking);
            button[2].onClick.AddListener(WorldRanking);

            localRanking = true;
            localRankingCanvasClone = Instantiate(localRankingCanvas);//ローカルランキング生成

            ranking = false;
        }

        //非表示
        else
        {
            //ランキング画面削除
            Destroy(rankingCanvasClone);
            Destroy(worldRankingCanvasClone);//ワールドランキング削除
            Destroy(localRankingCanvasClone);//ローカルランキング削除

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
        //キャラ停止
        playerControl.enabled = false;
        playerRigidbody.isKinematic = true;

        //ステージ生成停止
        stageCreate.enabled = false;
        stageMove.enabled = false;

        //ゲームオーバー画面生成
        gameOverCanvasClone = Instantiate(gameOverCanvas);

        //ボタンを取得
        button = gameOverCanvasClone.GetComponentsInChildren<Button>();

        //ボタンにイベント設定
        button[0].onClick.AddListener(Ranking);
        button[1].onClick.AddListener(Title);
        button[2].onClick.AddListener(Retry);

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
        QuickRanking.Instance.SaveRanking("New", score);

        //ハイスコアの更新
        HighScoreManager HSM = GameObject.Find("HighScoreManager").GetComponent<HighScoreManager>();
        HSM.newScore = score;
        HSM.HighScore();

        //ハイスコア取得
        int highScore = HSM.highScore;

        //ハイスコア表示
        Text highScoreText = gameOverCanvasClone.transform.FindChild("HighScore").GetComponent<Text>();
        highScoreText.text = "HighScore : " + highScore;
        Debug.Log(highScore);
    }

    //ローカルランキングの表示
    void LocalRanking()
    {
        if(localRanking == false)//ローカルランキングを開いていない
        {

            Destroy(worldRankingCanvasClone);//ワールドランキング削除
            localRankingCanvasClone = Instantiate(localRankingCanvas);//ローカルランキング生成

            localRanking = true;
        }
    }

    //ワールドランキングの表示
    void WorldRanking()
    {
        if (localRanking == true)//ローカルランキングを開いている
        {
            Destroy(localRankingCanvasClone);//ローカルランキング削除
            worldRankingCanvasClone = Instantiate(worldRankingCanvas);//ワールドランキング生成

            localRanking = false;
        }
    }
}
