using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {

    public float ballSpeed;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
	    rb = GetComponent<Rigidbody2D>();
        rb.velocity = ballSpeed * (new Vector2(1.0f, -2.0f)).normalized;
        Debug.Log(rb.velocity);
	}
}
