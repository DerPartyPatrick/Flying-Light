using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShieldedEnemyController : MonoBehaviour
{
    public GameObject shield;

    private void Start()
    {
        shield = Instantiate(shield, transform.position, transform.rotation);
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), shield.GetComponent<BoxCollider2D>()); 
    }

    private void Update()
    {
        Follow(); 
    }


    private void Follow()
    {
        shield.transform.position = transform.position;
        shield.transform.rotation = transform.rotation; 
    }

    


}
