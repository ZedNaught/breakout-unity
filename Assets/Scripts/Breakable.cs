using UnityEngine;
using System.Collections;

public class Breakable : MonoBehaviour {
    private static GameManager m_gameManager;

    void Start() {
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D c) {
        // increase score and destroy self on collision with ball

        if (c.gameObject.tag == "Ball") {
            m_gameManager.IncrementScore();
            Destroy(gameObject);
        }
    }
}
