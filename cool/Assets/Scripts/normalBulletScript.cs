using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//(◣_◢)ノ-=≡≡卍
public class normalBulletScript : MonoBehaviour
{
    private float speed = 25;
    private float number = 1;
    private DeadTile DT;
    private DoorScript DS;
    [SerializeField] private Transform trutransform;
    private Rigidbody2D rb;
    [SerializeField] private GameObject normalEnd;
    [SerializeField] Transform d2;
    [SerializeField] Transform d3;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }
    private void Update()
    {
        if (!GetComponent<Renderer>().isVisible)//if the bullet is not on the screen....
        {
            number -= Time.deltaTime;
            if (number < 0)//and 1 second passes....
            {
                Destroy(gameObject);//IT DIES ☠️:):):):)
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D col)//called when collides
    {
        if (!col.gameObject.CompareTag("Bullet") && !col.gameObject.CompareTag("Gangster"))//collides with everything but another bullet or Jhonny
        {
            Instantiate(normalEnd, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.gameObject.GetComponent<CompositeCollider2D>().bounds.ClosestPoint(trutransform.position));
        if (col.gameObject.GetComponent<CompositeCollider2D>() != null)//hitting tilesa
        {
            DT = col.GetComponent<DeadTile>();
            DT.giveTiles(trutransform.position);
            DT.giveTiles(d2.position);
            DT.giveTiles(d3.position);
            Instantiate(normalEnd, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (!col.isTrigger && col.gameObject.GetComponent<Animator>() == null)//hitting door
        {
            DS = col.GetComponent<DoorScript>();
            DS.DoorDamage();
            Instantiate(normalEnd, trutransform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (col.gameObject.GetComponent<Animator>() != null)//hitting gangster
        {
            Instantiate(normalEnd, trutransform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
/*
⠄⣾⣿⡇⢸⣿⣿⣿⠄⠈⣿⣿⣿⣿⠈⣿⡇⢹⣿⣿⣿⡇⡇⢸⣿⣿⡇⣿⣿⣿
⢠⣿⣿⡇⢸⣿⣿⣿⡇⠄⢹⣿⣿⣿⡀⣿⣧⢸⣿⣿⣿⠁⡇⢸⣿⣿⠁⣿⣿⣿
⢸⣿⣿⡇⠸⣿⣿⣿⣿⡄⠈⢿⣿⣿⡇⢸⣿⡀⣿⣿⡿⠸⡇⣸⣿⣿⠄⣿⣿⣿
⢸⣿⡿⠷⠄⠿⠿⠿⠟⠓⠰⠘⠿⣿⣿⡈⣿⡇⢹⡟⠰⠦⠁⠈⠉⠋⠄⠻⢿⣿
⢨⡑⠶⡏⠛⠐⠋⠓⠲⠶⣭⣤⣴⣦⣭⣥⣮⣾⣬⣴⡮⠝⠒⠂⠂⠘⠉⠿⠖⣬
⠈⠉⠄⡀⠄⣀⣀⣀⣀⠈⢛⣿⣿⣿⣿⣿⣿⣿⣿⣟⠁⣀⣤⣤⣠⡀⠄⡀⠈⠁
⠄⠠⣾⡀⣾⣿⣧⣼⣿⡿⢠⣿⣿⣿⣿⣿⣿⣿⣿⣧⣼⣿⣧⣼⣿⣿⢀⣿⡇⠄
⡀⠄⠻⣷⡘⢿⣿⣿⡿⢣⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣜⢿⣿⣿⡿⢃⣾⠟⢁⠈
⢃⢻⣶⣬⣿⣶⣬⣥⣶⣿⣿⣿⣿⣿⣿⢿⣿⣿⣿⣿⣿⣷⣶⣶⣾⣿⣷⣾⣾⢣
⡄⠈⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡏⠘
⣿⡐⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⢠⢃
⣿⣷⡀⠈⠻⣿⣿⣿⣿⣿⣿⣿⣿⠿⠿⠿⠿⢿⣿⣿⣿⣿⣿⣿⣿⡿⠋⢀⠆⣼
⣿⣿⣷⡀⠄⠈⠛⢿⣿⣿⣿⣿⣷⣶⣶⣶⣶⣶⣿⣿⣿⣿⣿⠿⠋⠠⠂⢀⣾⣿
⣿⣿⣿⣧⠄⠄⢵⢠⣈⠛⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⢋⡁⢰⠏⠄⠄⣼⣿⣿
⢻⣿⣿⣿⡄⢢⠨⠄⣯⠄⠄⣌⣉⠛⠻⠟⠛⢋⣉⣤⠄⢸⡇⣨⣤⠄⢸⣿⣿⣿
*/