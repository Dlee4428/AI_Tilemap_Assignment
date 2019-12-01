using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int x;
    public int y;
    public enum TileType { FLOOR, WALL };
    public TileType tileType;
}
