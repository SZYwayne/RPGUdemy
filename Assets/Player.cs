using UnityEngine;

public class Player : Entity
{
    [Header("Move Info")]
    [SerializeField] private float yInput;
    [SerializeField] private float xSpeed;
    private float xInput;

    [Header("Attack Info")]
    [SerializeField] private float comboTime = 1.5f;
    private float comboTimeCounter;
    private bool isAttacking;
    private int comboCounter;

    [Header("Dash Info")]
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashColdown;
    [SerializeField] private float dashSpeed;
    private float dashTime;
    private float dashCooldownTimer;



    protected override void Start()
    {
        base.Start();
    }


    protected override void Update()
    {
        base.Update();

        Movement();
        CheckInput();

        dashTime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        comboTimeCounter -= Time.deltaTime;

        FlipController();
        AnimatorControllers();
    }



    public void AttackOver()
    {
        isAttacking = false;

        comboCounter++;
        if (comboCounter > 2)
        {
            comboCounter = 0;

        }
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }
    }

    private void Attack()
    {
        if (!isGrounded)
        {
            return;
        }

        isAttacking = true;

        if (comboTimeCounter < 0)
        {
            comboCounter = 0;
            comboTimeCounter = comboTime;
        }

    }

    private void DashAbility()
    {
        if (dashCooldownTimer < 0 && !isAttacking)
        {
            dashTime = dashDuration;
            dashCooldownTimer = dashColdown;
        }

    }

    private void Movement()
    {
        if (isAttacking)
        {
            rb.velocity = new Vector2(0, 0);
        }
        else if (dashTime > 0)
        {
            rb.velocity = new Vector2(facingDir * dashSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(xInput * xSpeed, rb.velocity.y);
        }

    }

    private void AnimatorControllers()
    {
        bool isMoving = rb.velocity.x != 0;
        anim.SetFloat("yVelocity", rb.velocity.y);

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing", dashTime > 0);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("comboCounter", comboCounter);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.velocity += new Vector2(0, yInput);
        }

    }
    
}
