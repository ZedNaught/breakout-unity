using UnityEngine;
using System.Collections;

public class Breakable : MonoBehaviour {
    private static GameManager m_gameManager;

    void Start() {
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D c) {
        m_gameManager.IncrementScore();
        Destroy(gameObject);
    }
}
