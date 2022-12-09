using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GangsterScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private Transform raycastPosition;
    [SerializeField] private Transform raycast2Down;
    [SerializeField] private LayerMask gridLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject bulletNormal;
    [SerializeField] private Transform firePoint;
    private const string GANGSTER_IDLE = "GangsterIdle";
    private const string GANGSTER_MOVE = "GangsterMove";
    private const string GANGSTER_SHOOTING = "GangsterShooting";
    private string currentState;
    private float speedNOW = 5;
    private int hp=5;
    private bool facingRight = true;
    private bool canShoot = true;
    private bool shooting = false;
    private RaycastHit2D hit;
    private RaycastHit2D enemy;
    private RaycastHit2D somethingDown;
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //MAIN SHOW
    private void Update()
    {
        hit = Physics2D.Raycast(raycastPosition.position, transform.TransformDirection(Vector2.right), 1f, gridLayer);
        enemy = Physics2D.Raycast(raycastPosition.position, transform.TransformDirection(Vector2.right), 15f, enemyLayer);
        somethingDown = Physics2D.Raycast(raycast2Down.position, transform.TransformDirection(Vector2.down), 3f, gridLayer);
        Vector2 endpos = raycastPosition.position + Vector3.right;
        rb.velocity = new Vector2(speedNOW, rb.velocity.y);
        if (hit)//if hit wall
        {
            speedNOW *= -1;
        }
        else if (enemy && enemy.collider.tag == "Jhonny")//if enemy in sight
        {
            speedNOW = 0;
            if (canShoot == true)
            {
                ChangeAnimationState(GANGSTER_IDLE);
                canShoot = false;
                Invoke(nameof(SwitchToShooting), 1.5f);
            }
        }
        else if (!somethingDown)
        {
            speedNOW *= -1;
        }
        else if (speedNOW == 0)//if detect nothing reset
        {
            speedNOW = 5 * random01();
            canShoot = true;
            shooting = false;
        }
        if (speedNOW > 0f)
        {//if moving right
            if (!facingRight)
            {
                flip();
                facingRight = true;
            }
            ChangeAnimationState(GANGSTER_MOVE);
        }
        else if (speedNOW < 0f)
        {//if moving left
            if (facingRight)
            {
                flip();
                facingRight = false;
            }
            ChangeAnimationState(GANGSTER_MOVE);
        }
        else if (shooting)
        {
            shoot(firePoint);
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //FUNCTIONS
    private void Start()
    {
        speedNOW *= random01();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private int random01()
    {
        float number = Random.value;
        if (number > 0.50f)
        {
            return 1;
        }
        else return -1;
    }
    private void flip()
    {//flipping the motherfucker
        transform.Rotate(0f, 180f, 0f);
    }
    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;//if the same animation plays return
        anim.Play(newState);
        currentState = newState;
    }
    private void SwitchToShooting()
    {
        if (enemy && enemy.collider.tag == "Jhonny")
        {
            ChangeAnimationState(GANGSTER_SHOOTING);
            shooting = true;
            StartCoroutine(shoot(firePoint));
        }
    }
    IEnumerator shoot(Transform FirePoint)//spawns bullets at fire points with their coordinates and random degrees 1 hand at time
    {
        if (shooting)
        {
            Quaternion rotationn = Quaternion.Euler(0, 0, Random.Range(-4, 4));
            rotationn *= FirePoint.rotation;
            Instantiate(bulletNormal, FirePoint.position, rotationn);
            yield return new WaitForSeconds(0.001f);
            StartCoroutine(shoot(FirePoint));
        }
    }
    public void takeDMG(){
        hp--;
        if(hp==0){
            Destroy(gameObject);
        }
    }
}