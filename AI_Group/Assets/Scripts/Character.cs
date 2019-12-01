using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Tilemaps map;
    public float speed = 5;

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
        Vector3 pos = transform.position;
        Node start = map.GetNode(Mathf.FloorToInt(pos.x + 0.5f), Mathf.FloorToInt(pos.y + 0.5f));

        // Get the shortest path between start and target using astar algorithm
        List<Node> path = aStarPathfinder.Search(start, target);
        pathDisplay = path;

        foreach (Node n in path)
        {
            Vector3 nextPos = new Vector3(n.x, n.y, 0);
            Vector3 dir = nextPos - transform.position;
            Vector3 moved = Vector3.zero;
            while (dir.magnitude > moved.magnitude)
            {
                pos += speed * dir.normalized * Time.deltaTime;
                moved += speed * dir.normalized * Time.deltaTime;
                transform.position = pos;
                yield return null;
            }

            yield return null;
        }

        // set state to IDLE in order to enable next movement
        state = State.IDLE;
    }
}
