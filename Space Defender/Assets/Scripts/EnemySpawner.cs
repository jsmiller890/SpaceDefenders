using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy_black_1;
    public GameObject enemy_green_1;
    public float width = 10f;
    public float height = 5f;
    public float speed = 25;
    public float spawnDelay = 0.5f;
    public int killcount = 0;

    private bool movingRight = false;
    private float xmax;
    private float xmin;


    // Use this for initialization
    void Start()
    {
        SpawnBlackUntilFull();
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        xmax = rightmost.x;
        xmin = leftmost.x;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    // Update is called once per frame
    void Update()
    {
        if (killcount >= 2)
        {
            //LevelManager manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            //manager.LoadLevel("Level_02");
            SpawnGreenUntilFull();
        }

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
        //WORKING HERE!
        if (AllMembersDead())
        {
            SpawnBlackUntilFull();
        }
    }
    bool AllMembersDead()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            {
                if (childPositionGameObject.childCount > 0)
                {
                    return false;
                }
            }
        }
        killcount++;
        return true;
    }

    Transform NextFreePosition()
    {
        foreach(Transform childPositionGameObject in transform)
        {
           if(childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }
        return null;
    }

    void SpawnBlackUntilFull()
    {
        if(killcount < 2)
        {
            Transform freePosition = NextFreePosition();
            if (freePosition)
            {
                GameObject enemy = Instantiate(enemy_black_1, freePosition.position, Quaternion.identity) as GameObject;
                enemy.transform.parent = freePosition;
            }
            if (NextFreePosition())
            {
                Invoke("SpawnBlackUntilFull", spawnDelay);
            }
        }
    }

    void SpawnGreenUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if (freePosition)
        {
            GameObject enemy2 = Instantiate(enemy_green_1, freePosition.position, Quaternion.identity) as GameObject;
            enemy2.transform.parent = freePosition;
        }
        if (NextFreePosition())
        {
            Invoke("SpawnGreenUntilFull", spawnDelay);
        }
    }

    void Respawn()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemy_black_1, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }
}