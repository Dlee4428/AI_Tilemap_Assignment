using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemaps : MonoBehaviour
{
    public GameObject cursor;
    List<Node> nodes = new List<Node>();

    void Start()
    {
        foreach (Transform t in transform)
        {
            SpriteRenderer sr = t.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                Node n = new Node();
                n.x = Mathf.FloorToInt(t.position.x);
                n.y = Mathf.FloorToInt(t.position.y);
                if (sr.sprite.name == "floor")
                {
                    n.tileType = Node.TileType.FLOOR;
                }
                else
                {
                    n.tileType = Node.TileType.WALL;
                }
                nodes.Add(n);
            }
        }
    }

    void Update()
    {
        Vector3 pos = Input.mousePosition;
        pos = Camera.main.ScreenToWorldPoint(pos);
        pos = new Vector3(Mathf.FloorToInt(pos.x + 0.5f), Mathf.FloorToInt(pos.y + 0.5f), 0);
        cursor.transform.position = pos;
    }

    public List<Node> GetAllNodes()
    {
        return nodes;
    }

    public Node GetNode(int x, int y)
    {
        return nodes.Find(delegate (Node n) { return (n.x == x) && (n.y == y); });
    }

    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();
        Node neighbor = GetNode(node.x + 1, node.y);
        if (neighbor != null && neighbor.tileType != Node.TileType.WALL)
        {
            neighbors.Add(neighbor);
        }

        neighbor = GetNode(node.x - 1, node.y);
        if (neighbor != null && neighbor.tileType != Node.TileType.WALL)
        {
            neighbors.Add(neighbor);
        }

        neighbor = GetNode(node.x, node.y + 1);
        if (neighbor != null && neighbor.tileType != Node.TileType.WALL)
        {
            neighbors.Add(neighbor);
        }

        neighbor = GetNode(node.x, node.y - 1);
        if (neighbor != null && neighbor.tileType != Node.TileType.WALL)
        {
            neighbors.Add(neighbor);
        }
        return neighbors;
    }

    float GetDistance(Node n1, Node n2)
    {
        float dist = 0;
        float xDiff = n1.x - n2.x;
        float yDiff = n1.y - n2.y;
        dist = Mathf.Sqrt(xDiff * xDiff + yDiff * yDiff);
        return dist;
    }

    public float GetNeighborDistance(Node n1, Node n2)
    {
        return GetDistance(n1, n2);
    }

    public float GetEstimatedDistance(Node n1, Node n2)
    {
        float d = GetDistance(n1, n2);
        return d;
    }
}
