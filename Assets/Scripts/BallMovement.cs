using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {

    public float m_ballSpeedY;  // velocity.y will always be this
    public float m_maxSpeedX;  // velocity.x will always be <= this
    public GameObject m_bat;  // player bat

    private Rigidbody2D m_rb;  // reference to ball's rigidbody
    private bool m_onBat;  // is the ball currently on the bat awaiting launch?
    private float m_verticalBallOffset;  // store calculation of ball offset from bat when "on bat"

    void Start() {
        // grab ball's rigidbody
        m_rb = GetComponent<Rigidbody2D>();

        // cache "on bat" offset calculation
        float m_ballRadius = GetComponent<CircleCollider2D>().radius;
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
        if (!m_onBat) {
            LimitVelocity();
        }
    }

    void LimitVelocity() {
        // clamp x velocity and keep y velocity constant

        Vector2 currentVelocity = m_rb.velocity;
        Vector2 targetVelocity = new Vector2(Mathf.Clamp(currentVelocity.x, -m_maxSpeedX, m_maxSpeedX), Mathf.Sign(currentVelocity.y) * m_ballSpeedY);
        if (currentVelocity != targetVelocity) {
            Vector2 slowdownForce = Utilities.GetDeltaVForce(m_rb.mass, currentVelocity, targetVelocity);
            m_rb.AddForce(slowdownForce);
        }
    }

    public void ResetToBat() {
        // tell ball to track bat and adjust rigidbody accordingly

        m_onBat = true;
        m_rb.isKinematic = true;
        m_rb.velocity = Vector3.zero;
        TrackBat();
    }

    void Launch() {
        // tell ball to leave the bat and adjust rigidbody accordingly

        m_onBat = false;
        m_rb.isKinematic = false;

        // ball inherits 100% of bat's x velocity on launch
        Vector2 batVelocity = m_bat.GetComponent<Rigidbody2D>().velocity;
        m_rb.velocity = batVelocity + m_ballSpeedY * Vector2.up;
    }

    void TrackBat() {
        // move ball to bat position + vertical offset

        Vector3 batPosition = m_bat.transform.position;
        transform.position = new Vector2(batPosition.x, batPosition.y + m_verticalBallOffset);
    }
}
