using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SampleScore : MonoBehaviour
{
    public InputField playerName;
    public Text scoreText;
    private int score = 9;
    public Button saveScoreButton;
    public Button showRankingButton;
    public Text scoreBoardText;

    public Text message;

    private void Start()
    {
        QuickRanking.Instance.SaveRanking("N", score, a);
    }

    void a()
    {
        message.text = "Your score saved!";
        scoreBoardText.text = QuickRanking.Instance.GetRankingByText();
    }
}