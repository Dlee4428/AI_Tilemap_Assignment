using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    int counter = 8;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
      
      
    }
    public void GenerateIdleMovement()
    {
        //time between enemy moves
        timer -= Time.deltaTime;
        if (timer > 0)
        {
            return;
        }
        Vector2 movement = Vector2.zero;
        int direction = (int)(Mathf.PerlinNoise(counter * 0.2f, 0.5f) * 4.9 + 1);


        //enemies move in 4 directions, and can sometimes not move at all
        switch (direction)
        {
            case 1:
                movement.x = 0;
                movement.y = 1;
                break;
            case 2:
                movement.x = 0;
                movement.y = -1;
                break;
            case 3:
                movement.x = 1;
                movement.y = 0;
                break;
            case 4:
                movement.x = -1;
                movement.y = 0;
                break;

            default:
                movement.x = 0;
                movement.y = 0;
                break;
        }
        transform.position += new Vector3(movement.x, movement.y, 0);
        timer = 0.5f;
        counter = Random.Range(1, 100);
        //random seed for the counter variable to keep the enemies from having the same movement on every restart    
        return;
    }
}
