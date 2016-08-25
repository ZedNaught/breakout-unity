using UnityEngine;
using System.Collections;

public class BatMovement : MonoBehaviour {
    public float m_batSpeed;  // horizontal axis input scaling
    public float m_batLeftBound, m_batRightBound;  // bat will be constrained between these x values
    public float m_ballXVelocityInheritance;  // fraction of bat's x velocity inherited on hit
    public float m_xOffsetForceScale;  // scalar for ball hit horizontal velocity adjustment based on offset from bat center

    private bool m_movementEnabled;  // can bat can be moved by player input?
    private Rigidbody2D m_rb;

	void Start() {
	    m_rb = GetComponent<Rigidbody2D>();
        m_movementEnabled = true;
	}
   
	void Update() {
        if (m_movementEnabled) {
            Move();
        }
	}

    public void DisableMovement() {
        // stop bat movement and disable movement on user input

        m_movementEnabled = false;
        m_rb.velocity = new Vector2(0f, 0f);
    }

    public void EnableMovement() {
        // enable movement on user input

        m_movementEnabled = true;
    }

    void Move() {
        // move bat left/right according to Horizontal axis input

        float xInput = Input.GetAxis("Horizontal");

        // keep bat within wall boundaries
        float batXPosition = transform.position.x;
        if ((batXPosition > m_batLeftBound || xInput > 0) && (batXPosition < m_batRightBound || xInput < 0)) {
            m_rb.velocity = new Vector2(xInput * m_batSpeed, 0);
        }
        else {
            // bat is at wall and attempting to move toward wall; stop it
            m_rb.velocity = new Vector2(0, 0);
        }
    }
 
    void OnCollisionEnter2D(Collision2D c) {
        // handle ball collision

        if (c.gameObject.tag != "Ball") {
            // bail on non-ball object, who cares
            return;
        }

        // ball inherits some of bat's horizontal speed; calculate necessary force
        Rigidbody2D ballRb = c.gameObject.GetComponent<Rigidbody2D>();
        Vector2 inheritedVelocity = m_ballXVelocityInheritance * m_rb.velocity;
        Vector2 velocityInheritanceForce = (ballRb.mass / Time.fixedDeltaTime) * inheritedVelocity;

        // ball launch angle is affected by distance from center of bat; calculate appropriate force
        Vector2 batPosition = transform.position;
        ContactPoint2D contact = c.contacts[0];
        float xOffset = Mathf.Round(1000f * (contact.point.x - batPosition.x)) / 1000f;  // absolute horizontal offset between ball and center of bat
        BoxCollider2D batCollider = GetComponent<BoxCollider2D>();
        float batWidth = batCollider.size.x * transform.localScale.x;
        float xOffsetRelative = Mathf.Clamp(xOffset * 2 / batWidth, -1f, 1f);  // "relative" horizontal offset between ball and center of bat (in range [-1, 1])
        Vector2 batOffsetForce = (m_xOffsetForceScale * xOffsetRelative) * Vector2.right;

        // add both hit adjustment forces to ball
        ballRb.AddForce(velocityInheritanceForce + batOffsetForce);
    }
}
