using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rigid;
    private CharacterStats plStats;
    //private GameObject chargeMeter;
    private float speed;
    public float chargeAmt;
    public bool isMoving;
    public bool isFlying;
    public bool isGrounded;
    private bool spaced;
    private bool overheated;
    private int timer;
    private float rise;
    private bool isRight;
    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
        isMoving = false;
        isFlying = false;
        spaced = false;
        rigid = GetComponent<Rigidbody2D>();
        plStats = GetComponent<CharacterStats>();
        speed = plStats.speed;
        chargeAmt = plStats.chargeAmt;
        overheated = false;
        isRight = true;
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
                transform.GetChild(1).transform.Rotate(0,90,0);
                overheated = true;
                rigid.gravityScale = 1f;

            }

        }
        if (chargeAmt >= 60 && overheated)
        {
            transform.GetChild(1).transform.Rotate(0, -90, 0);
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


        var mov = Input.GetAxis("Horizontal");
       // print(chargeAmt);
        if (isRight && mov < 0)
        {
            isRight = false;
            this.transform.Rotate(new Vector3(0, 180f, 0));
        }
        else if (!isRight && mov > 0)
        {
            isRight = true;
            this.transform.Rotate(new Vector3(0, 180f, 0));
        }
        if (!overheated && !isGrounded)
        {
            isMoving = false;
            isFlying = true;
        }
        else
        {
                isFlying = false;
            if (mov <= 0.15f && mov >= -0.15f)
                isMoving = false;
            else
                isMoving = true;
            
        }
        animator.SetBool("moving", isMoving);
        animator.SetBool("flying", isFlying);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        print("Grounde");
        isGrounded = rigid.velocity.y <= 0.1f && rigid.velocity.y >= -0.1f;
        
    }
}
