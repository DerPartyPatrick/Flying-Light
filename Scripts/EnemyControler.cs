using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyControler : MonoBehaviour
{

    public float moveSpeed;


    private GameObject player;
    private Rigidbody2D rb;


    public GameObject[] eyes;
    public float openEyesRadius;
    public Animator anim;

    public bool dead;
    private SpriteRenderer spr;
    bool eyesOpned = false;
    public AudioSource saudioSource;
    private GameControler gameControler;
    public bool isShieldEnemy;
    public Transform shieldRendere;
    bool shieldRight = true;
    bool shieldUp = false; 
    Vector2 dir;
    float angle; 

    private CameraShaker camShaker;
    private bool firstHit = true;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        gameControler = GameObject.FindObjectOfType<GameControler>();
        camShaker = FindObjectOfType<CameraShaker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isShieldEnemy)
        { 
            setShield();
        }

        chasePlayer();
    }



    void chasePlayer()
    {
        if(!dead)
        {
            dir = (player.transform.position - transform.position).normalized;
            rb.velocity = dir * moveSpeed;
            
            if (Vector2.Distance(transform.position, player.transform.position) <= openEyesRadius)
            {
                openEyes();
            }
            else
            {
                closeEyes();
            }
        }
        else
        { 
            if (eyesOpned)
            {
                rb.velocity = Vector2.zero;
                spr.color += new Color(0, 0, 0, 0);
                anim.SetTrigger("dead");
                
            }
            else
            {
                StartCoroutine(die()); 
            }
           
        }

    }

    private IEnumerator die()
    {
       
        yield return new WaitForSeconds(1f);
       
        Destroy(gameObject);
    }




    void openEyes()
    {
        foreach(GameObject eye in eyes)
        {
            eye.SetActive(true);
        }
        eyesOpned = true;
    }

    void closeEyes()
    {
        foreach (GameObject eye in eyes)
        {
            eye.SetActive(false);
        }
        eyesOpned = false;
    }


    public void destroy()
    {
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            Destroy(collision.gameObject);
            gameControler.setHighScore();
            gameControler.setDeadCanvas();

        }
        
        if(collision.gameObject.tag.Equals("Flying Light"))
        {

            camShaker.shake();

            //if(isShieldEnemy)
            //{
            //    Vector2 normal = collision.GetContact(0).normal;
            //    Debug.Log(shieldRight + "/" + normal);


            //    if((shieldRight && normal.x < 0) || ( !shieldRight && normal.x > 0))
            //    {
                    
            //    }
            //    else
            //    {
            //        despawnEnemy();
            //    }
            //}
            //else
            //{
            //    despawnEnemy();
            //}

            if(isShieldEnemy)
            {
                Vector2 normal = collision.GetContact(0).normal; 

                if(normal == new Vector2(1f, 0f))
                {
                    if(shieldRight)
                    {
                        despawnEnemy(); 
                    }
                }
                else if(normal == new Vector2(0f, 1f))
                {
                    if(shieldUp)
                    {
                        despawnEnemy(); 
                    }
                }
                else if(normal == new Vector2(-1f, 0f))
                {
                    if(!shieldRight)
                    {
                        despawnEnemy(); 
                    }
                }
                else if(normal == new Vector2(0f, -1f))
                {
                    if(!shieldUp)
                    {
                        despawnEnemy(); 
                    }
                }
            }
            else
            {
                despawnEnemy(); 
            }
            
        }
        
    }

    void setShield()
    {
        //if(dir.x  > 0 )
        //{
        //    shieldRendere.localScale = new Vector2(1,1);
        //    shieldRight = true;
            
        //}
        //else
        //{
        //    shieldRendere.localScale = new Vector2(-1, 1);
        //    shieldRight = false;
        //}

        if(dir.x >= 0 && dir.y >= 0)
        {
            shieldRendere.localScale = new Vector2(1, -1);
            shieldRight = true;
            shieldUp = true; 
        }
        else if(dir.x < 0 && dir.y >= 0)
        {
            shieldRendere.localScale = new Vector2(-1, -1);
            shieldRight = false;
            shieldUp = true; 
        }
        else if(dir.x < 0 && dir.y < 0)
        {
            shieldRendere.localScale = new Vector2(-1, 1);
            shieldRight = false;
            shieldUp = false; 
        }
        else if(dir.x >= 0 && dir.y < 0)
        {
            shieldRendere.localScale = new Vector2(1, 1);
            shieldRight = true;
            shieldUp = false; 
        }


    }

    void despawnEnemy()
    {
        if(firstHit)
        {
            gameControler.reduceEnemyCount();
            firstHit = false;
        }
        rb.velocity = Vector2.zero;
        dead = true;
        saudioSource.Play();
       

    }

}
