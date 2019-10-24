using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rigid;
    private CharacterStats plStats;
    //private GameObject chargeMeter;
    private float speed;
    private float chargeAmt;
    private bool isFlying;
    private bool spaced;
    private bool overheated;
    private int timer;
    private float rise;
    // Start is called before the first frame update
    void Start()
    {
        spaced = false;
        rigid = GetComponent<Rigidbody2D>();
        plStats = GetComponent<CharacterStats>();
        speed = plStats.speed;
        chargeAmt = plStats.chargeAmt;
        overheated = false;
        timer = 0;
       // isFlying = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        spaced = Input.GetKey(KeyCode.Space) || (Input.GetKey(KeyCode.Space) && Input.anyKey);
        if (!spaced || overheated)
        {
            rise -= 0.8f;
            if (rise <= 0f)
                rise = 0f;
            rigid.velocity = new Vector2(Mathf.Lerp(0f, Input.GetAxis("Horizontal") * speed, 0.8f), rigid.velocity.y);
            timer++;
        }

            
        if(spaced && !overheated &&chargeAmt >= 0)
        {
            if (rise <= 5)
                rise += 0.4f;
            timer = 0;
            if(rigid.velocity.y <= 0)
            rigid.velocity = new Vector2(Mathf.Lerp(0f, Input.GetAxis("Horizontal") * speed, 0.8f), Mathf.Lerp(rigid.velocity.y, rise, 0.8f));
            else
                rigid.velocity = new Vector2(Mathf.Lerp(0f, Input.GetAxis("Horizontal") * speed, 0.8f), Mathf.Lerp(0f, rise, 0.8f));

            chargeAmt--;
            GetComponentInChildren<adjustscale>().x -= (1/ plStats.chargeAmt);

            if (spaced && chargeAmt <= 0)
            {
                overheated = true;
                this.GetComponentInChildren<SpriteRenderer>().sortingOrder -= 2;
                rigid.gravityScale = 1f;

            }

        }
        if (chargeAmt >= 60 && overheated)
        {
            this.GetComponentInChildren<SpriteRenderer>().sortingOrder += 2;

            overheated = false;
        }
        if (overheated && rigid.velocity.magnitude <= 2 && timer >= 180)
        {
            chargeAmt += 2;
            this.GetComponentInChildren<adjustscale>().x += 2 / plStats.chargeAmt;
           
        }
        else if (chargeAmt <= plStats.chargeAmt && timer >= 90)
        {
            this.GetComponentInChildren<adjustscale>().x += 1/plStats.chargeAmt;
            chargeAmt++;
        }



        print(chargeAmt);

    }
}
