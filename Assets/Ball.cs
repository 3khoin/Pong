using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    float speed;

    float radius;
    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        BallStart();
        radius = transform.localScale.x / 2; // half the width
        transform.name = "PongBall";
        this.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
        this.GetComponent<SpriteRenderer>().sortingOrder = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameWon == false)
        {
            transform.Translate(direction * speed * Time.deltaTime);

            // Bounce off top and bottom
            if (transform.position.y < GameManager.bottomLeft.y + radius && direction.y < 0)
            {
                direction.y = -direction.y;
            }

            if (transform.position.y > GameManager.topRight.y - radius && direction.y > 0)
            {
                direction.y = -direction.y;
            }

            // Score
            if (transform.position.x < GameManager.bottomLeft.x - radius && direction.x < 0)
            {
                Debug.Log("Right player scores!!");
                GameObject.Find("GameManager").GetComponent<GameManager>().RightScored();
                BallStart();
            }

            if (transform.position.x > GameManager.topRight.x + radius && direction.x > 0)
            {
                Debug.Log("Left player scores!!");
                GameObject.Find("GameManager").GetComponent<GameManager>().LeftScored();
                BallStart();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Paddle")
        {
            bool isRight = other.GetComponent<Paddle>().isRight;

            // If hitting right paddle and moving right, flip direction
            if(isRight == true && direction.x > 0)
            {
                direction.x = -direction.x;
            }

            // If hitting left paddle and moving left, flip direction
            if(isRight == false && direction.x < 0)
            {
                direction.x = -direction.x;
            }
        }
    }

    void BallStart()
    {
        // Place ball in center of screen
        transform.position = new Vector2(GameManager.center.x, GameManager.center.y);

        // Randomize starting direction of ball
        int x = Random.Range(0, 6);
        switch (x)
        {
            case 0:
                direction = new Vector2(-1, 1).normalized;
                break;
            case 1:
                direction = new Vector2(-1, 0).normalized;
                break;
            case 2:
                direction = new Vector2(-1, 1).normalized;
                break;
            case 3:
                direction = new Vector2(1, 1).normalized;
                break;
            case 4:
                direction = new Vector2(1, 0).normalized;
                break;
            case 5:
                direction = new Vector2(1, -1).normalized;
                break;
        }
    }
}