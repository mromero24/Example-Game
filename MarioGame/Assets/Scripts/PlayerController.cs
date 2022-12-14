using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private bool canMove;
    private Rigidbody2D theRB2D;

    public bool grounded;
    public LayerMask whatIsGrd;
    public Transform grdChecker;
    public float grdCheckerRad;

    public float airTime;
    public float airTimeCounter;
    
    private Animator theAnimator;

    // Start is called before the first frame update
    void Start()
    {
        theRB2D = GetComponent<Rigidbody2D>();
        theAnimator = GetComponent<Animator>();

        airTimeCounter = airTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            canMove = true;
        }
        Debug.Log(theRB2D.velocity);

        Brake();

        Teleport2();

        Teleport3();



    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(grdChecker.position, grdCheckerRad, whatIsGrd);

        MovePlayer();
        Jump();
        Dash();
    }

    void MovePlayer()
    {
        if (canMove)
        {
            theRB2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, theRB2D.velocity.y);

            theAnimator.SetFloat("Speed", Mathf.Abs(theRB2D.velocity.x));

            if (theRB2D.velocity.x > 0)
                transform.localScale = new Vector2(1f, 1f);
            else if (theRB2D.velocity.x < 0)
                transform.localScale = new Vector2(-1f, 1f);
        }
    }

    void Brake()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            theRB2D.velocity = new Vector2(0,0);
        }

    }

    void Teleport2()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
           theRB2D.position = new Vector2(0,theRB2D.position.y);
        }
    }

    void Dash()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            theRB2D.velocity = new Vector2(20,theRB2D.velocity.y);
        }
    }

    void Teleport3()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            theRB2D.position = new Vector2(-theRB2D.position.x, -theRB2D.position.y);
        }
    }
    void Jump()
    {
        if (grounded == true)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                theRB2D.velocity = new Vector2(theRB2D.velocity.x, jumpForce);
            }

        }

        if(Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            if(airTimeCounter > 0)
            {
                theRB2D.velocity = new Vector2(theRB2D.velocity.x, jumpForce);
                airTimeCounter -= Time.deltaTime; 
            }
        }

        if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            airTimeCounter = 0;
        }

        if(grounded)
        {
            airTimeCounter = airTime;
        }

        theAnimator.SetBool("Grounded", grounded);


    }
}

