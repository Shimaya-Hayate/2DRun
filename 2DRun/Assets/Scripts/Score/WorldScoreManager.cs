using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldScoreManager : MonoBehaviour
{
    public Text message;
    public Text topScoreBoard;
    public Text nearScoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        ScoreDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ランキングの表示
    void ScoreDisplay()
    {
        message.text = "";
        topScoreBoard.text = QuickRanking.Instance.GetRankingByText();
        nearScoreBoard.text = QuickRanking.Instance.GetNearRankingByText();
    }
}
