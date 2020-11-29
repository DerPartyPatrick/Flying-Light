using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : MonoBehaviour
{
    public float speed;
    public Vector2 direction;

   
    private Rigidbody2D rb; 

    public float maxTravelTime;
    private float curTravelTime;

    private GameControler gameControler;
    

    public  AudioSource audioSource;
    public AudioClip wallHitAudio, enemyHitAudio;

    private void Start()
    {

        gameControler = FindObjectOfType<GameControler>();
        rb = GetComponent<Rigidbody2D>();
        
        Accelerate(); 
    }

    private void Update()
    {
        if(curTravelTime < 0)
        {
            //rb.velocity = Vector2.zero;
        }
        else
        {
            curTravelTime -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.GetContact(0).normal;
        direction = Vector2.Reflect(direction, normal);
        Accelerate();
       
        if(collision.gameObject.tag.Equals("wall") || collision.gameObject.tag.Equals("CameraBoundarys"))
        {
            audioSource.Play();
        }

        if(collision.gameObject.tag.Equals("Enemy"))
        {    
            audioSource.Play();
        }

       
    }

  
    private void Accelerate()
    {
        curTravelTime = maxTravelTime;
        rb.velocity = direction * speed; 
    }

    
}
