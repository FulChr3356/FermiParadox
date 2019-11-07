using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rigid;
    private CharacterStats plStats;
    private float speed;
    public float savedSpeed;
    private bool overheated;
    private float chargeAmt;
    private bool isFlying;
    private bool spaced;
    public int timer;
    private float rise; 
    private bool isDashing;
    public float dashTimer =0;
    public float maxDash = 0;
    public Vector2 savedVelocity;
    public DashState dashState;
    public DashArrow arrowState;
    public float dashX = 3;
    public float dashY = 0; 

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
        isFlying = false;
        isDashing = false;
        
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


        if (spaced && !overheated && chargeAmt >= 0)
        {
            if (rise <= 5)
                rise += 0.4f;
            timer = 0;
            if (rigid.velocity.y <= 0)
                rigid.velocity = new Vector2(Mathf.Lerp(0f, Input.GetAxis("Horizontal") * speed, 0.8f), Mathf.Lerp(rigid.velocity.y, rise, 0.8f));
            else
                rigid.velocity = new Vector2(Mathf.Lerp(0f, Input.GetAxis("Horizontal") * speed, 0.8f), Mathf.Lerp(0f, rise, 0.8f));

            chargeAmt--;
            GetComponentInChildren<adjustscale>().x -= (1 / plStats.chargeAmt);

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
            this.GetComponentInChildren<adjustscale>().x += 1 / plStats.chargeAmt;
            chargeAmt++;
        }



        print(chargeAmt);






        switch (dashState)
        {
            case DashState.Ready:
                if (chargeAmt <= 40 || overheated)
                    break;
                var isDashKeyDown = Input.GetKeyDown(KeyCode.LeftShift);
                if (isDashKeyDown)
                {
                    savedSpeed = speed;
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        arrowState = DashArrow.FowardPressed;
                        dashX = 25;
                        dashY = 0;
                    }



                     else if (Input.GetKey(KeyCode.UpArrow))
                    {
                        arrowState = DashArrow.UpPressed;
                        dashY = 25;
                        dashX = 0;
                    }
                   else  if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        arrowState = DashArrow.BackPressed;
                        dashX = -25;
                        dashY = 0;
                    }

                   else  if (Input.GetKey(KeyCode.DownArrow))
                    {
                        arrowState = DashArrow.DownPressed;
                        dashX = 0;
                        dashY = -25;
                    }
                    chargeAmt -= 40;
                    dashState = DashState.Dashing;
                }
                break;
            case DashState.Dashing:

                rigid.velocity = new Vector2(dashX, dashY);

                
                dashTimer += .5f;
                if (dashTimer >= 5f)
                {
                    
                    speed = savedSpeed;
                    rigid.velocity = new Vector2(0, 0);
                    dashState = DashState.Cooldown;
                }
                break;
            case DashState.Cooldown:
                dashTimer -= .5f;
                if (dashTimer <= 0)
                {
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;
        }
    }
}
                



   
public enum DashState
{
    Ready,
    Dashing,
    Cooldown
}

public enum DashArrow
{
    UpPressed,DownPressed,BackPressed, FowardPressed
}


