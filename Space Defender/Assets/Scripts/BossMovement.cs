using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour {

    public float speed = 20;
    float xmax;
    float xmin;
    bool movingRight = true;
    public float width;
    public float height;

    // Use this for initialization
    void Start () {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        xmax = rightmost.x;
        xmin = leftmost.x;
    }
	
	// Update is called once per frame
	void Update () {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);
        if (leftEdgeOfFormation < xmin)
        {
            movingRight = true;
        }

        else if (rightEdgeOfFormation > xmax)
        {
            movingRight = false;
        }
    }
}
