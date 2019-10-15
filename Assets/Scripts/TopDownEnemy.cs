using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownEnemy : MonoBehaviour
{
    public enum States {  Default, Follow };

    public List<Transform> waypoints;
    public States state;
    public int currWaypoint = 0;

    public Transform player;

    private float speed;
    private CharacterStats enStats;
    private Rigidbody2D rigid;

    public float threshold = .05f;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        enStats = GetComponent<CharacterStats>();

        speed = enStats.speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveTo = new Vector2();
        switch(state)
        {
            case States.Default:
                moveTo = waypoints[currWaypoint].position;
                break;

            case States.Follow:
                moveTo = player.position;
                break;

        }

        Vector2 dir = (moveTo - transform.position).normalized;

        rigid.velocity = dir * speed;

        if((waypoints[currWaypoint].position - transform.position).magnitude < threshold)
        {
            currWaypoint++;
            if(currWaypoint >= waypoints.Count)
            {
                currWaypoint = 0;
            }
        }

    }

     void OnTriggerEnter2D(Collider2D coll)
    {
        //print("test");
        if(coll.gameObject.name.Equals("bluecircle"))
        {
            state = States.Follow;
        }

    }
     void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.name.Equals("bluecircle"))
        {
            state = States.Default;
        }
    }
}
