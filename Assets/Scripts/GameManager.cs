using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public GameObject[] m_brickPrefabs;
    public GameObject m_brickHolder;

    public int m_numBricksX, m_numBricksY;

    private int m_score, m_lives;

	void Start() {
	    m_score = 0;
        m_lives = 3;
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

    public int IncrementScore() {
        m_score += 1;
        return m_score;
    }
}
