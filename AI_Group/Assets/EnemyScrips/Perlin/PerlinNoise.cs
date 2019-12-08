using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    [Header("TileList")]
    [SerializeField]
    Tilemaps map;
    public Node node;

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
        Vector2 movement = GenerateDirection();
        transform.position += new Vector3(movement.x, movement.y, 0);




        timer = 1.5f; 
        return;
    }
    private Vector2 GenerateDirection()
    {
        Vector2 movement = Vector2.zero;
        int direction = (int)(Mathf.PerlinNoise(counter * 0.2f, 0.5f) * 4.9 + 1);
        counter = Random.Range(1, 100);

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

        node = map.GetNode((int)transform.position.x + (int)movement.x, (int)transform.position.y + (int)movement.y);
        while (node.tileType == Node.TileType.WALL) //if the target node is a wall, recalculate until the targert node is no longer a node
        {
            Debug.Log(node.tileType);
          
          
            counter = Random.Range(1, 100);
           
            direction = (int)(Mathf.PerlinNoise(counter * 0.2f, 0.5f) * 4.9 + 1);


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

            //random seed for the counter variable to keep the enemies from having the same movement on every restart   
            node = map.GetNode((int)transform.position.x + (int)movement.x, (int)transform.position.y + (int)movement.y);
        }

        return movement;
       
      
    }
}
//ChangeLog
//30/11/2019

/*Added Enum and states IDLE, CHASING, EVADING
 * Added GenerateIdleMovement Function
 * Added GenerateDirection
 * ~Brandon Gale
 * 
 * */
