using UnityEngine;
using System.Collections;

public class BatMovement : MonoBehaviour {
    public float m_batSpeed;
    public float m_batLeftBound, m_batRightBound;
    public float m_ballXVelocityInheritance, m_xOffsetForceScale;

    private Rigidbody2D m_rb;

	void Start() {
	    m_rb = GetComponent<Rigidbody2D>();
	}
   
	void Update () {
	    float xInput = Input.GetAxis("Horizontal");
        float batXPosition = transform.position.x;
        if ((batXPosition > m_batLeftBound || xInput > 0) && (batXPosition < m_batRightBound || xInput < 0)) {
            m_rb.velocity = new Vector2(xInput * m_batSpeed, 0);
        }
        else {
            m_rb.velocity = new Vector2(0, 0);
        }
	}
 
    void OnCollisionEnter2D(Collision2D c) {
        if (c.gameObject.tag != "Ball") {
            return;
        }

        Rigidbody2D ballRb = c.gameObject.GetComponent<Rigidbody2D>();

        Vector2 inheritedVelocity = m_ballXVelocityInheritance * m_rb.velocity;
        Vector2 velocityInheritanceForce = (ballRb.mass / Time.fixedDeltaTime) * inheritedVelocity;

        Vector2 batPosition = transform.position;
        BoxCollider2D batCollider = GetComponent<BoxCollider2D>();
        ContactPoint2D contact = c.contacts[0];
        float xOffset = Mathf.Round(1000f * (contact.point.x - batPosition.x)) / 1000f;
        float batWidth = batCollider.size.x * transform.localScale.x;
        float xOffsetRelative = Mathf.Clamp(xOffset * 2 / batWidth, -1f, 1f);
        Vector2 batOffsetForce = (m_xOffsetForceScale * xOffsetRelative) * Vector2.right;

//        ballRb.velocity += inheritedVelocity;
        ballRb.AddForce(velocityInheritanceForce + batOffsetForce);

       

//        foreach (ContactPoint2D contact in c.contacts) {
//            Debug.Log("hi");
//            if (contact.collider == batCollider) {
//                Debug.Log(contact.point.x - batPosition.x);
//            }
//        }
    }
}
