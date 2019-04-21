using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public float health = 5000;
    public float projectileSpeed = -10;
    public GameObject enemyProjectile;
    public float firingRate = 0.2f;
    public float shotsPerSecond = 0.5f;
    public int scoreValue = 1000;

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

    void Update()
    {
        float probability = Time.deltaTime * shotsPerSecond;
        if (Random.value < probability)
        {
            Fire();
        }

        if(health <= 0)
        {
            Die();
        }
    }

    void Fire()
    {
        GameObject bossmissile = Instantiate(enemyProjectile, transform.position, Quaternion.identity) as GameObject;
        bossmissile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        AudioSource.PlayClipAtPoint(firesound, transform.position);
    }

    void Die()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathtone, transform.position);
        scoreKeeper.Score(scoreValue);
        LevelManager manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        manager.LoadLevel("Win");
    }
}
