using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {

    public float m_ballSpeedY;
    public GameObject m_bat;

    private Rigidbody2D m_rb;
    private bool m_onBat;
    private float m_ballRadius;
    private float m_verticalBallOffset;

    void Start() {
        m_rb = GetComponent<Rigidbody2D>();
        m_ballRadius = GetComponent<CircleCollider2D>().radius;
        m_verticalBallOffset = m_ballRadius + m_bat.GetComponent<BoxCollider2D>().size.y / 2.0f + 0.2f;

        ResetToBat();
    }

    void Update() {
        if (m_onBat && Input.GetButtonDown("Jump")) {
            Launch();
        }
        if (m_onBat) {
            TrackBat();
        }
    }

//    void StartMovement() {
////        m_rb.velocity = m_ballSpeed * (new Vector2(-1.0f, 1.0f)).normalized;
//    }

    void ResetToBat() {
        m_onBat = true;
        m_rb.isKinematic = true;
        m_rb.velocity = Vector3.zero;
    }

    void Launch() {
        m_onBat = false;
        m_rb.isKinematic = false;
        Vector2 batVelocity = m_bat.GetComponent<Rigidbody2D>().velocity;
        m_rb.velocity = batVelocity + m_ballSpeedY * Vector2.up;
//        StartMovement();
    }

    void TrackBat() {
        Vector3 batPosition = m_bat.transform.position;
        transform.position = new Vector2(batPosition.x, batPosition.y + m_verticalBallOffset);
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other);
    }

    void Hit() {
        
    }
}
