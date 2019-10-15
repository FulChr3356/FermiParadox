using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rigid;
    private CharacterStats plStats;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {

        rigid = GetComponent<Rigidbody2D>();
        plStats = GetComponent<CharacterStats>();
        speed = plStats.speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(Mathf.Lerp(0f, Input.GetAxis("Horizontal") * speed, 0.8f), rigid.velocity.y);

        if(Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rigid.velocity.y) <= 0.1f)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y + 5);
        }

    }
}
