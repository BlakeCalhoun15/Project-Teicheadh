using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    public GameObject player;
    private GameObject enemy;

    private float movementSpeed;
    public float moveDistanceCheck = 1f;
    public List<Transform> waypoints;
    public int nextPointIndex = 0;
    private int idChangeValue = 1;
    public bool inRangeForPathing = false;
    public bool inRangeForCombat = false;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = GetComponentInParent<Enemy>().movementSpeed;
        enemy = GetComponentInParent<Enemy>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inRangeForPathing)
        {
            MoveToNextPoint();
        }
        else 
        {
            inRangeForCombat = GetComponentInParent<Enemy>().inRangeForCombat;
            if (!inRangeForCombat)
            {
                FollowPlayer();
            }
        }
    }

    void MoveToNextPoint()
    {
        // get the next point transform
        Transform goalPoint = waypoints[nextPointIndex];
        
        // flip enemy transform
        if (goalPoint.transform.position.x > enemy.transform.position.x)
        {
            enemy.transform.localScale = new Vector3(1,1,1);
        }
        else enemy.transform.localScale = new Vector3(-1,1,1);
       
        // move enemy towards goal point
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, goalPoint.position, movementSpeed * Time.deltaTime);
        
        // check distance between enemy and goal point to trigger next point
        if (Vector2.Distance(enemy.transform.position, goalPoint.position) < moveDistanceCheck)
        {
            // check if we are at the end of the line (make change -1)
            if (nextPointIndex == waypoints.Count - 1)
            {
                idChangeValue = -1;
            }
            
            // check if we are at the start of the line (make chnge +1)
            if (nextPointIndex == 0)
            {
                idChangeValue = 1;
            }
            
            // apply the change on the nextPointIndex
            nextPointIndex += idChangeValue;
        }
    }

    void FollowPlayer()
    {
        // flip enemy transform
        if (player.transform.position.x > enemy.transform.position.x)
        {
            enemy.transform.localScale = new Vector3(1,1,1);
        }
        else enemy.transform.localScale = new Vector3(-1,1,1);

        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, player.transform.position, movementSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="collision">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inRangeForPathing = true;
            player = collision.gameObject;
        }
    }

    /// <summary>
    /// Sent when another object exits a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="collision">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inRangeForPathing = false;
        }
    }
}
