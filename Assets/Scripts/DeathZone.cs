using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour {
    public static GameManager m_gameManager;

    void Start() {
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        m_gameManager.Death();
    }
}