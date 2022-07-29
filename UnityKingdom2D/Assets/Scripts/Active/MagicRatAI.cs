using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MagicRatAI : MonoBehaviour
{
    public Transform target;
    public float shootingDistance;

    private float timeBtwShots;
    public float startTimeBtwshots;

    public GameObject projectile;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    bool reachedEndOfPath = false;

    public Transform ratGFX;

    Seeker seeker;
    Rigidbody2D rb;

    Path path;
    int currentWaypoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        timeBtwShots = startTimeBtwshots;
       
        InvokeRepeating("UpdatePath", 0f, .5f);
        
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
       

        if (path == null)
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if(rb.velocity.x >= 0.01f)
        {
            ratGFX.localScale = new Vector3(1f, 1f, 1f);
        }else if(rb.velocity.x <= -0.01f)
        {
            ratGFX.localScale = new Vector3(-1f, 1f, 1f);
        }

        if(timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwshots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
            
        }
    }
}
