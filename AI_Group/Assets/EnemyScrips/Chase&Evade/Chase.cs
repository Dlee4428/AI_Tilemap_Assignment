using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    [Header("Player Object")]
   [SerializeField]
    GameObject player;

    [Header("TileMap")]
    [SerializeField]
    Tilemaps map;
    public Node node;
    public Vector2 distance;
    
    float timer = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ChaseMe()
    {
        Vector2 movement = Vector2.zero;
        distance = distance.normalized;
        timer -= Time.deltaTime;


        if (timer <= 0)
        {
            if (transform.position.x > player.transform.position.x)
            {
                movement.x = -1;
                node = map.GetNode((int)transform.position.x + (int)movement.x, (int)transform.position.y + (int)movement.y);
                if (node.tileType == Node.TileType.WALL)
                {
                    movement.x = 0;
                   
                }


            }
            if (transform.position.x < player.transform.position.x)
            {
                movement.x = 1;
                node = map.GetNode((int)transform.position.x + (int)movement.x, (int)transform.position.y + (int)movement.y);
                if (node.tileType == Node.TileType.WALL)
                {
                    movement.x = 0;
                   
                }

            }

            if (transform.position.y > player.transform.position.y)
            {
                movement.y = -1;
                node = map.GetNode((int)transform.position.x + (int)movement.x, (int)transform.position.y + (int)movement.y);
                if (node.tileType == Node.TileType.WALL)
                {
                    movement.y = 0;
                }

            }
            if (transform.position.y < player.transform.position.y)
            {
                movement.y = 1;
                node = map.GetNode((int)transform.position.x + (int)movement.x, (int)transform.position.y + (int)movement.y);
                if (node.tileType == Node.TileType.WALL)
                {
                    movement.y = 0;
                }

            }

            //node = map.GetNode((int)transform.position.x + (int)movement.x, (int)transform.position.y + (int)movement.y);
            
                transform.position += new Vector3(movement.x, movement.y, 0);
            

            timer = 1.5f;
        }
    }
}
