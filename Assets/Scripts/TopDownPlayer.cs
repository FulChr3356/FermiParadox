using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayer : MonoBehaviour
{
    private float speed;
    private Rigidbody2D rigid;
    private CharacterStats plStats;
    private bool activated;
    // Start is called before the first frame update
    void Start()
    {

        rigid = GetComponent<Rigidbody2D>();
        plStats = GetComponent<CharacterStats>();
        speed = plStats.speed;
        rigid.gravityScale = 0;

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.gravityScale = 1;
            activated = true;
            
        }
    }
    void FixedUpdate()
    {
        if(!activated)
        rigid.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * speed, 0.8f),rigid.velocity.y);

        //  print(speed);
        // Debug.Log("Hello");

    }
}
