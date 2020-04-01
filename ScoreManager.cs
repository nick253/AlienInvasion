using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score;
    public TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // this script is updating the score textbox attatched to the players left hand.
        textMesh.text = score.ToString();
    }

    /// <summary>
    /// Thic function is called when enemies die in order to reward the player with points.
    /// </summary>
    /// <param name="addScore"></param>
    public void addScore(int addScore)
    {
        score += addScore;
    }
}
