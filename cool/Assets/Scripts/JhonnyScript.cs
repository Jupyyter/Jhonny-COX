using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//  (((＼（✘෴✘）／)))
public class JhonnyScript : MonoBehaviour
{   //local variables for BODY
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private Rigidbody2D rb;
    private Animator anim;
    private float dirx = 0f;
    private BoxCollider2D coll;
    [SerializeField] private LayerMask jumpGround;
    private SpriteRenderer sprite;
    private float MoveSpeed = 10f;
    private float JumpForce = 25f;
    [SerializeField] private bool facingright = true;
    private string currentState;
    private AnimationState anima;
    private const string MOVE_BODY_ANIMATION = "MoveBodyAnimation";
    private const string IDLE_BODY_ANIMATION = "IdleBodyAnimation";
    private const string NEW_JHONNY_JUMP_RUN_ANIMATION = "NewJhonnyJumpRun";
    private const string NEW_JHONNY_JUMP_STAY_ANIMATION = "NewJhonnyJumpStay";
    private const string NEW_JHONNY_FALL_RUN_ANIMATION = "NewJhonnyFallRun";
    private const string NEW_JHONNY_FALL_STAY_ANIMATION = "NewJhonnyFallStay";

    //local variables of HANDS

    [SerializeField] Transform FirePoint1;
    [SerializeField] Transform FirePoint2;
    [SerializeField] private GameObject BulletPrefab;
    public AnimationState COOOLLL;
    private float LastFrameDirx = 0;
    private bool mid = false;
    private bool canshoot = true;
    private float number = 0f;
    private const string RUNNING_HANDS_ANIMATION = "RunningHandsAnimation";
    private const string IDLE_HANDS_ANIMATION = "IdleHandsAnimation";
    private const string JUMP_HANDS_ANIMATION = "IdleHandsAnimation";
    private const string SHOOTING_HANDS_ANIMATION = "ShootingHandsAnimation";
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void Start()
    {
        //anima=GetComponent<AnimationState>();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        dirx = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirx * MoveSpeed, rb.velocity.y);//move using the power of Vector2
        if (Input.GetButton("Jump") && OnGround())//jump
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }
        HandleBody();//animate the body and flip children
        HandleHands();//animate and shoot
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void HandleBody()
    {
        if (dirx > 0f)
        {//if moving right
            if (!facingright)
            {
                flip();
                facingright = true;
            }
        }
        else if (dirx < 0f)
        {//if moving left
            if (facingright)
            {
                flip();
                facingright = false;
            }
        }
        if (!OnGround())
        {
            if (rb.velocity.y > 0 && dirx != 0)
            {
                ChangeAnimationState(NEW_JHONNY_JUMP_RUN_ANIMATION);
            }
            else if (rb.velocity.y < 0 && dirx != 0)
            {
                ChangeAnimationState(NEW_JHONNY_FALL_RUN_ANIMATION);
            }
            else if (rb.velocity.y > 0)
            {
                ChangeAnimationState(NEW_JHONNY_JUMP_STAY_ANIMATION);
            }
            else if (rb.velocity.y < 0)
            {
                ChangeAnimationState(NEW_JHONNY_FALL_STAY_ANIMATION);
            }
        }
        else if (dirx != 0 && OnGround())
        {
            ChangeAnimationState(MOVE_BODY_ANIMATION);
        }
        else if (OnGround())//if idle Ω
        {
            ChangeAnimationState(IDLE_BODY_ANIMATION);
        }
    }
    private void HandleHands()
    {
        dirx = Input.GetAxisRaw("Horizontal");
        number += Time.deltaTime;
        if (number > 0.05f)
        {//limiting shooting to once 0.05 seconds from god mode
            canshoot = true;
            number = 0;
        }
        if (Input.GetKey("z"))//shoot shoot shoot shoot
        {
            if (canshoot)
            {
                if (!mid)
                {
                    shoot(FirePoint1);
                    mid = true;
                }
                else
                {
                    shoot(FirePoint2);
                    mid = false;
                }
                canshoot = false;
                ChangeAnimationState(SHOOTING_HANDS_ANIMATION);
            }
        }
        else if (dirx == 0 || !OnGround())
        {
            ChangeAnimationState(IDLE_HANDS_ANIMATION);
        }
        else if (OnGround())
        {
            if (LastFrameDirx == 0f)
            {
                ChangeAnimationState(RUNNING_HANDS_ANIMATION);
            }
            else
            {
                float normalizedTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
                float normalizedTimeInCurrentLoop = normalizedTime - Mathf.Floor(normalizedTime);
                ChangeAnimationStateB(RUNNING_HANDS_ANIMATION, normalizedTimeInCurrentLoop);
            }
        }
        LastFrameDirx = dirx;
    }

    private void shoot(Transform FirePoint)//spawns bullets at fire points with their coordinates and random degrees 1 hand at time
    {
        Quaternion rotationn = Quaternion.Euler(0, 0, Random.Range(-3,3));
        rotationn *= FirePoint.rotation;
        Instantiate(BulletPrefab, FirePoint.position, rotationn);
    }
    private bool OnGround()
    {//if on ground
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpGround);
    }
    private void flip()
    {//flipping the motherfucker AND HIS CHILDREN :):):):):):):):):):):):):):):):):):):)
        transform.Rotate(0f, 180f, 0f);
    }
    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;//if the same animation plays
        anim.Play(newState);
        currentState = newState;
    }
    private void ChangeAnimationStateB(string newState, float timee)
    {
        if (currentState == newState) return;//if the same animation plays
        anim.Play(newState, -1, timee);
        currentState = newState;
    }
}
/*
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⣤⣶⣶⣶⣶⣶⣦⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⢿⣿⣿⡿⣿⣿⣿⣿⣿⣿⣿⣿⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣰⣿⣿⣿⣿⡇⣿⣷⣿⣿⣿⣿⣿⣿⣯⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡰⣿⣿⣿⣇⣿⣀⠸⡟⢹⣿⣿⣿⣿⣿⣿⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⢡⣿⣿⣿⡇⠝⠋⠀⠀⠀⢿⢿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠸⢸⠸⣿⣿⣇⠀⠀⠀⠀⠀⠀⠊⣽⣿⣿⣿⠁⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢿⣿⣿⣷⣄⠀⠀⠀⢠⣴⣿⣿⣿⠋⣠⡏⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠐⠾⣿⣟⡻⠉⠀⠀⠀⠈⢿⠋⣿⡿⠚⠋⠁⡁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣶⣾⣿⣿⡄⠀⣳⡶⡦⡀⣿⣿⣷⣶⣤⡾⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⣿⣿⡆⠀⡇⡿⠉⣺⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⣿⣯⠽⢲⠇⠣⠐⠚⢻⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⡐⣾⡏⣷⠀⠀⣼⣷⡧⣿⣿⣦⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣻⣿⣿⣿⣿⣿⣮⠳⣿⣇⢈⣿⠟⣬⣿⣿⣿⣿⣿⡦⢄⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⢄⣾⣿⣿⣿⣿⣿⣿⣿⣷⣜⢿⣼⢏⣾⣿⣿⣿⢻⣿⣝⣿⣦⡑⢄⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⣠⣶⣷⣿⣿⠃⠘⣿⣿⣿⣿⣿⣿⣿⡷⣥⣿⣿⣿⣿⣿⠀⠹⣿⣿⣿⣷⡀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⣇⣤⣾⣿⣿⡿⠻⡏⠀⠀⠸⣿⣿⣿⣿⣿⣿⣮⣾⣿⣿⣿⣿⡇⠀⠀⠙⣿⣿⡿⡇⠀⠀⠀⠀⠀
⠀⠀⢀⡴⣫⣿⣿⣿⠋⠀⠀⡇⠀⠀⢰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⢘⣿⣿⣟⢦⡸⠀⠀⠀
⠀⡰⠋⣴⣿⣟⣿⠃⠀⠀⠀⠈⠀⠀⣸⣿⣿⣿⣿⣿⣿⣇⣽⣿⣿⣿⣿⣇⠀⠀⠀⠁⠸⣿⢻⣦⠉⢆⠀⠀
⢠⠇⡔⣿⠏⠏⠙⠆⠀⠀⠀⠀⢀⣜⣛⡻⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⡀⠀⠀⠀⠀⡇⡇⠹⣷⡈⡄⠀
⠀⡸⣴⡏⠀⠀⠀⠀⠀⠀⠀⢀⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣻⣿⣿⣿⣿⣿⣿⡄⠀⠀⠀⡇⡇⠀⢻⡿⡇⠀
⠀⣿⣿⣆⠀⠀⠀⠀⠀⠀⢀⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡀⠀⣰⠿⠤⠒⡛⢹⣿⠄
⠀⣿⣷⡆⠁⠀⠀⠀⠀⢠⣿⣿⠟⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠻⢷⡀⠀⠀⠀⠀⠀⣸⣿⠀
⠀⠈⠿⢿⣄⠀⠀⠀⢞⠌⡹⠁⠀⠀⢻⡇⠹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠁⢳⠀⠀⠁⠀⠀⠀⠀⢠⣿⡏⠀
⠀⠀⠀⠈⠂⠀⠀⠀⠈⣿⠁⠀⠀⠀⡇⠁⠀⠘⢿⣿⣿⠿⠟⠋⠛⠛⠛⠀⢸⠀⠀⡀⠂⠀⠀⠐⠛⠉⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠐⠕⣠⡄⣰⡇⠀⠀⠀⢸⣧⠀⠀⠀⠀⠀⠀⠀⢀⣸⠠⡪⠊⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢫⣽⡋⠭⠶⠮⢽⣿⣆⠀⠀⠀⠀⢠⣿⣓⣽⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⢹⣶⣦⣾⣿⣿⣿⡏⠀⠀⠀⠀⠀
*/