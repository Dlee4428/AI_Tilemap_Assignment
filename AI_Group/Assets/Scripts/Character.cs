using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public Tilemaps map;

    Astar aStarPathfinder = new Astar();
    public enum State { MOVE, IDLE };
    State state;

    List<Node> pathDisplay = new List<Node>();

    // Use this for initialization
    void Start()
    {
        state = State.IDLE;
        aStarPathfinder.Init(map);
    }

    // Update is called once per frame
    void Update()
    {
        // handle mouse input
        if (state == State.IDLE && Input.GetMouseButtonDown(0))
        {
            state = State.MOVE;
            Vector3 pos = Input.mousePosition;
            pos = Camera.main.ScreenToWorldPoint(pos);

            Node targetNode = map.GetNode(Mathf.FloorToInt(pos.x + 0.5f), Mathf.FloorToInt(pos.y + 0.5f));

            if (targetNode.tileType == Node.TileType.FLOOR)
            {
                StartCoroutine(SearchPathAndMove(targetNode));
            }
            else
            {
                state = State.IDLE;
                Debug.Log("BLOCKED BY WALL");
            }
        }

        for (int i = 0; i < pathDisplay.Count - 1; i++)
        {
            Node n1 = pathDisplay[i];
            Node n2 = pathDisplay[i + 1];
            Debug.DrawLine(new Vector3(n1.x, n1.y, 0), new Vector3(n2.x, n2.y, 0), Color.red);
        }
    }

    IEnumerator SearchPathAndMove(Node target)
        {
            // To do: Find out a start node. The start node is the node where the character stands
            // Node start = ?
            Vector3 pos = transform.position;
            Node startNode = map.GetNode(Mathf.FloorToInt(pos.x + 0.5f), Mathf.FloorToInt(pos.y + 0.5f));

            // To do: Get the shortest path between start and target using astar algorithm
            // List<Node> path = ?
            List<Node> path = aStarPathfinder.Search(startNode, target);
            pathDisplay = path;
            // To do: move the character using the position info in the shortest path
            // you need to use "yield return new WaitForSeconds(0.5f);" to make a delay between each movement
            // Refer to https://docs.unity3d.com/Manual/Coroutines.html
            foreach(Node paths in path)
            {
                float speed = 10.0f;
                // At the origin
                Vector3 visitedPos = Vector3.zero;
                // A* pathfinding searched position of x, y
                Vector3 unvisitedPos = new Vector3(paths.x, paths.y, transform.position.z);
                // Direction Vector track by a* final pos - current pos
                Vector3 dirVector = unvisitedPos - pos; 

                // While loop iteration till diretion vector is less than visited position; 
                while(Vector3.Magnitude(dirVector) > Vector3.Magnitude(visitedPos))
                {
                    pos += dirVector.normalized * speed * Time.deltaTime;
                    visitedPos += dirVector.normalized * speed * Time.deltaTime;
                    transform.position = pos;
                    //yield return new WaitForSeconds(0.5f);
                    yield return null;
                }
            }
        // set state to IDLE in order to enable next movement
        state = State.IDLE;
    }
}
