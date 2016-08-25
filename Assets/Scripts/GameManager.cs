using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject[] m_brickPrefabs;  // list of available brick prefabs
    public GameObject m_brickHolder;  // organizational GameObject to hold bricks
    public GameObject m_ball, m_bat;  // references to ball and bat
    public int m_startLives;  // how many lives the player starts with
    public Text m_scoreText, m_livesText, m_gameOverText;  // various UI text objects
    public int m_numBricksX, m_numBricksY;  // how may bricks in each row and "column"

    private int m_score, m_lives;  // track current score and number of lives

    private int Score {
        get {
            return m_score;
        }
        set {
            m_score = value;
            m_scoreText.text = string.Format("Score: {0}", m_score);
        }
    }

    private int Lives {
        get {
            return m_lives;
        }
        set {
            m_lives = Mathf.Max(0, value);
            m_livesText.text = string.Format("Lives: {0}", m_lives);
        }
    }

	void Start() {
	    Score = 0;
        Lives = m_startLives;
        m_gameOverText.transform.parent.gameObject.SetActive(false);
        SetupBricks();
	}

    void SetupBricks() {
        Vector2 brickStart = new Vector2(-4.86f, 3.31f);
        Vector2 brickSizeUnscaled = m_brickPrefabs[0].GetComponent<BoxCollider2D>().size;
        Vector3 brickScale = m_brickPrefabs[0].transform.localScale;
        Vector2 brickSize = new Vector2(brickSizeUnscaled.x * brickScale.x, brickSizeUnscaled.y * brickScale.y);

        float brickX, brickY, xOffset;
        int brickPrefabIndex = 0;
        for (int y = 0; y < m_numBricksY; y++) {
            brickY = brickStart.y - y * brickSize.y;
            xOffset = y % 2 == 0 ? 0f : brickSize.x / 2f;
            for (int x = 0; x < m_numBricksX; x++) {
                brickX = xOffset + brickStart.x + x * brickSize.x;
                Instantiate(m_brickPrefabs[brickPrefabIndex], new Vector3(brickX, brickY, 0f), Quaternion.identity, m_brickHolder.transform);
                brickPrefabIndex = (brickPrefabIndex + 1) % m_brickPrefabs.Length;
            }
        }
    }

    void DestroyBricks() {
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
        foreach(GameObject brick in bricks) {
            Destroy(brick);
        }
    }

    public int IncrementScore() {
        Score += 1;
        return Score;
    }

    public void Death() {
        if (Lives > 0) {
            Lives -= 1;
            ResetBall();
        }
        else {
            EndGame();
        }
    }

    void ResetBall() {
        m_ball.GetComponent<BallMovement>().ResetToBat();
    }

    public void RestartGame() {
        DestroyBricks();
        SetupBricks();
        Score = 0;
        Lives = m_startLives;
        m_gameOverText.transform.parent.gameObject.SetActive(false);
        m_bat.GetComponent<BatMovement>().EnableMovement();
        m_ball.GetComponent<BallMovement>().ResetToBat();
        m_ball.SetActive(true);
    }

    void EndGame() {
        m_ball.SetActive(false);
        m_bat.GetComponent<BatMovement>().DisableMovement();
        m_gameOverText.text = string.Format("Game Over\nFinal Score: {0}", Score);
        m_gameOverText.transform.parent.gameObject.SetActive(true);
    }
}
