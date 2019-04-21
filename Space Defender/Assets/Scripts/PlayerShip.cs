using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour {

    public float shipSpeed;
    public float padding = 1;
    public GameObject laser_1;
    public float projectileSpeed;
    public float firingRate = 0.2f;
    public float health = 250;
    public AudioClip fireSound;

    float xmin;
    float xmax;

	// Use this for initialization
	void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
	}
	
    void Fire()
    {
        Vector3 offset = new Vector3(0, .5f, 0);
        GameObject laser = Instantiate(laser_1, transform.position + offset, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-shipSpeed, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(shipSpeed, 0f, 0f);
        }
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);

        transform.position = new Vector3(newX, -3.6f, 0f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
	}
    void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile missile = collider.gameObject.GetComponent<Projectile>();
            if (missile)
            {
            health -= missile.GetDamage();
            missile.Hit();
            Death();
                
            }
    }

    void Death()
    {
        if (health <= 0)
        {
            LevelManager manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            manager.LoadLevel("Lose");
            Destroy(gameObject);
        }
    }
}
