using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {

    public float m_ballSpeedY, m_maxSpeedX;
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

    void FixedUpdate() {
        // clamp x velocity and keep y velocity constant
        if (!m_onBat) {
            Vector2 currentVelocity = m_rb.velocity;
            Vector2 targetVelocity = new Vector2(Mathf.Clamp(currentVelocity.x, -m_maxSpeedX, m_maxSpeedX), Mathf.Sign(currentVelocity.y) * m_ballSpeedY);
            if (currentVelocity != targetVelocity) {
                Vector2 slowdownForce = Utilities.GetDeltaVForce(m_rb.mass, currentVelocity, targetVelocity);
                m_rb.AddForce(slowdownForce);
            }
        }
    }

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
    }

    void TrackBat() {
        Vector3 batPosition = m_bat.transform.position;
        transform.position = new Vector2(batPosition.x, batPosition.y + m_verticalBallOffset);
    }
}
