using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {

    public float ballSpeed;
    public GameObject bat;

    private Rigidbody2D rb;
    private bool onBat;
    private float ballRadius;
    private float verticalBatOffset;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        ballRadius = GetComponent<CircleCollider2D>().radius;
        verticalBatOffset = ballRadius + bat.GetComponent<BoxCollider2D>().size.y / 2.0f + 0.1f;

        ResetToBat();
    }

    void Update() {
        if (Input.GetKeyDown("space") && onBat) {
            Launch();
        }
        if (onBat) {
            TrackBat();
        }
    }

    void StartMovement() {
        rb.velocity = ballSpeed * (new Vector2(-1.0f, 2.0f)).normalized;
    }

    void ResetToBat() {
        onBat = true;
        rb.isKinematic = true;
    }

    void Launch() {
        rb.isKinematic = false;
        onBat = false;
        StartMovement();
    }

    void TrackBat() {
        transform.position = new Vector2(bat.transform.position.x, bat.transform.position.y + verticalBatOffset);
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other);
    }

}
