using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public bool mouseAiming;
    public GameObject torch;
    public GameObject flyingTorchPrefab; 
    public bool pickUpReady;

    private GameObject flyingTorch;
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private Vector2 lightDirection;
    private TorchController torchController;
    private bool fired;
    private bool paused;

    private GameControler gameControler;
   
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lightDirection = new Vector2(0f, 1f);
        fired = false;
        pickUpReady = false;
        gameControler = FindObjectOfType<GameControler>();
       
    }

    private void Update()
    {
        mouseAiming = !InputManager.usingController;
        if (InputManager.controller == "XBOX" || InputManager.controller == "PS4") mouseAiming = false;
        Movement();

        Turning();
      
        Firing();
       
        pause();
    }




    private void Movement()
    {
        movementDirection = new Vector2(InputManager.PrimaryDirection_Horizontal(), InputManager.PrimaryDirection_Vertical());
        movementDirection = Vector2.ClampMagnitude(movementDirection, 1f); 
        rb.velocity = movementDirection * speed; 
    }

    private void Turning()
    {
        Vector2 newDirection; 

        if(mouseAiming)
        {
            Vector2 mp = InputManager.MousePosition();
            Vector2 playerPosition = transform.position;
            newDirection = mp - playerPosition; 
        }
        else
        {
            newDirection = new Vector2(InputManager.SecondaryDirection_Horizontal(), InputManager.SecondaryDirection_Vertical()); 

            if(newDirection == new Vector2(0f, 0f))
            {
                return; 
            }

        }

        float angle = Vector2.SignedAngle(lightDirection, newDirection);

        

        torch.transform.RotateAround(transform.position, Vector3.forward, angle);
        newDirection.Normalize();
        lightDirection = newDirection;
    }


    private void pause()
    {
        if(!paused)
        {
            if(InputManager.Pause())
            {
                gameControler.setPauseCanvas();
                paused = true;
            }
        }
        else
        {
            if(InputManager.Pause())
            {
                gameControler.closePauseCanvas();
                paused = false;
            }
        }
    }

    private void Firing()
    {
        if(!fired)
        {
            if (InputManager.Fire())
            {
                torch.SetActive(false);
                flyingTorch = Instantiate(flyingTorchPrefab, torch.transform.position, torch.transform.rotation);
                torchController = flyingTorch.GetComponent<TorchController>();
                torchController.direction = lightDirection;
                fired = true; 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Flying Light" && pickUpReady)
        {
            Destroy(collision.gameObject);
            fired = false;
            torch.SetActive(true);
            pickUpReady = false; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Flying Light")
        {
            pickUpReady = true;
        }
    }
}
