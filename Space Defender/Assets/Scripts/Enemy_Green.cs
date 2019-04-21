using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Green : MonoBehaviour {

    public float health = 250;
    public float projectileSpeed = -10;
    public GameObject enemyProjectile;
    public float firingRate = 0.2f;
    public float shotsPerSecond = 0.5f;
    public int scoreValue = 175;

    public AudioClip firesound;
    public AudioClip deathtone;

    private ScoreKeeper scoreKeeper;

    private void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void Update()
    {
        float probability = Time.deltaTime * shotsPerSecond;
        if (Random.value < probability)
        {
            Fire();
        }
    }

    void Fire()
    {
        GameObject missile = Instantiate(enemyProjectile, transform.position, Quaternion.identity) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        AudioSource.PlayClipAtPoint(firesound, transform.position);
    }

    void Die()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathtone, transform.position);
        scoreKeeper.Score(scoreValue);
    }
}
