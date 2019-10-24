using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rigid;
    private CharacterStats plStats;
    private float speed;
    private float chargeAmt;
    private bool isFlying;
    private bool spaced;
    // Start is called before the first frame update
    void Start()
    {
        spaced = false;
        rigid = GetComponent<Rigidbody2D>();
        plStats = GetComponent<CharacterStats>();
        speed = plStats.speed;
        chargeAmt = plStats.chargeAmt;
        isFlying = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spaced = Input.GetKeyDown(KeyCode.Space);
        
        rigid.velocity = new Vector2(Mathf.Lerp(0f, Input.GetAxis("Horizontal") * speed, 0.8f), rigid.velocity.y);

        if(spaced && !isFlying)
        {
            if(Mathf.Abs(rigid.velocity.y) <= 0.1f)
                rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y + 5);
            else if(chargeAmt > 0)
            {
                rigid.gravityScale = 0.5f;
                isFlying = true;

            }
        }
        else if(isFlying)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, Mathf.Lerp(0f, rigid.velocity.y + Input.GetAxis("Vertical") * 2, 0.8f));
            chargeAmt--;

            if (spaced || chargeAmt <= 0)
            {
                isFlying = false;
                rigid.gravityScale = 1f;

            }

        }
        if (chargeAmt <= 1200 && !isFlying)
            chargeAmt++;

        print(chargeAmt);

    }
}
