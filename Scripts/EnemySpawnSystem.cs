using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{

    public GameObject[] enemys;
    public int maxShieldEnemys;
    private int currentShieldEnemys;

    public Vector2 topLeft, botRight;

    public int maxEnemys;
    

  



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void spawnEnemys()
    {
        currentShieldEnemys = 0;
        Vector3 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector2 spawnPos;
       

        for(int i = 0; i < maxEnemys; i++ )
        {
            do
            {
                spawnPos = new Vector2(Random.Range(botRight.x, topLeft.x), Random.Range(topLeft.y, botRight.y));
            } while (viewPortIntersect(spawnPos.x, spawnPos.y, lowerLeft,upperRight));

            Instantiate(enemys[chooseEnemyType()], spawnPos, Quaternion.identity);


        }
        

    }


    bool viewPortIntersect(float x, float y, Vector3 lowerLeft, Vector3 upperRight)
    {
        if(x > lowerLeft.x && x < upperRight.x)
        {
            if(y > lowerLeft.x && y < upperRight.y) return true;

        }
     
        return false;
    }


    int chooseEnemyType()
    {
        if(currentShieldEnemys <= maxShieldEnemys)
        {
            currentShieldEnemys++;
            return 1;
            
        }

        return 0;
    }
}
