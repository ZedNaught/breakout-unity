using UnityEngine;
using System.Collections;

public class BatMovement : MonoBehaviour {
    public float batSpeed;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
	    rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    float xInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(batSpeed * xInput, 0);
	}
}
