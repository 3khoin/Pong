using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    public Paddle paddle;

    // Text renders and values for scores of left and right paddle
    public GameObject objectScoreLeft;
    public GameObject objectScoreRight;
    public TextMeshPro textScoreLeft;
    public TextMeshPro textScoreRight;
    public int scoreLeft;
    public int scoreRight;

    // Renders for help text and victory text
    public GameObject objectHelpText;
    public GameObject objectVictoryText;
    public TextMeshPro helpText;
    public TextMeshPro victoryText;
    public string helpMessage;
    public string victoryMessage;

    public static bool gameWon;
    public static bool gameLogPrinted;

    public static Vector2 bottomLeft;
    public static Vector2 topRight;
    public static Vector2 center;

    // Start is called before the first frame update
    void Start()
    {
        // Convert screen pixel coordinates into game coordinates
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        center = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));

        // Create ball
        Instantiate(ball);

        // Create two paddles
        Paddle paddle1 = Instantiate(paddle) as Paddle;
        Paddle paddle2 = Instantiate(paddle) as Paddle;
        paddle1.Init(true); // right paddle
        paddle2.Init(false); // left paddle

        // Initialize scores to 0
        scoreLeft = 0;
        scoreRight = 0;

        // Initialize help and victory text
        helpMessage = "Press H for help";
        victoryMessage = " ";

        gameWon = false;
        gameLogPrinted = false;

        // Create left score text
        objectScoreLeft = new GameObject();
        objectScoreLeft.name = "ScoreLeft";
        objectScoreLeft.tag = "ScoreText";
        textScoreLeft = objectScoreLeft.AddComponent<TextMeshPro>();
        textScoreLeft.text = scoreLeft.ToString();
        textScoreLeft.alignment = TextAlignmentOptions.Center;
        textScoreLeft.fontSize = 18;
        objectScoreLeft.GetComponent<RectTransform>().anchoredPosition = new Vector2(-5, -3);

        // Create right score text
        objectScoreRight = new GameObject();
        objectScoreRight.name = "ScoreRight";
        objectScoreRight.tag = "ScoreText";
        textScoreRight = objectScoreRight.AddComponent<TextMeshPro>();
        textScoreRight.text = scoreRight.ToString();
        textScoreRight.alignment = TextAlignmentOptions.Center;
        textScoreRight.fontSize = 18;
        objectScoreRight.GetComponent<RectTransform>().anchoredPosition = new Vector2(5, -3);

        // Create help text
        objectHelpText = new GameObject();
        objectHelpText.name = "HelpText";
        objectHelpText.tag = "HelpText";
        helpText = objectHelpText.AddComponent<TextMeshPro>();
        helpText.text = helpMessage;
        helpText.alignment = TextAlignmentOptions.Center;
        helpText.fontSize = 12;
        helpText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        // Create victory text
        objectVictoryText = new GameObject();
        objectVictoryText.name = "VictoryText";
        objectVictoryText.tag = "VictoryText";
        victoryText = objectVictoryText.AddComponent<TextMeshPro>();
        victoryText.text = victoryMessage;
        victoryText.alignment = TextAlignmentOptions.Center;
        victoryText.fontSize = 12;
        victoryText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 3);

    }

    // Update is called once per frame
    void Update()
    {
        if (gameLogPrinted == false)
        {
            if (scoreLeft > 9)
            {
                gameWon = true;
                if (scoreLeft > scoreRight)
                {
                    Time.timeScale = 0;
                    Debug.Log("Left Side wins!!");
                    victoryMessage = "LEFT SIDE WINS!";
                    victoryText.text = victoryMessage;
                    gameLogPrinted = true;
                }
            }
            if (scoreRight > 9)
            {
                gameWon = true;
                if (scoreRight > scoreLeft)
                {
                    Time.timeScale = 0;
                    Debug.Log("Right Side wins!!");
                    victoryMessage = "RIGHT SIDE WINS!";
                    victoryText.text = victoryMessage;
                    gameLogPrinted = true;
                }
            }
        }
    }

    public void RightScored()
    {
        scoreRight += 1;
        textScoreRight.text = scoreRight.ToString();
    }

    public void LeftScored()
    {
        scoreLeft += 1;
        textScoreLeft.text = scoreLeft.ToString();
    }
}