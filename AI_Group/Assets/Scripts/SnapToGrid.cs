using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SnapToGrid : MonoBehaviour
{

    public float gridSideLength = 1.0f;

    void Update()
    {
        if (Application.isPlaying)
        {
            return;
        }

        transform.localPosition = GetSnappedPosition(transform.localPosition);
    }

    public Vector3 GetGridPosition()
    {
        return GetSnappedPosition(transform.position);
    }

    public Vector3 GetSnappedPosition(Vector3 position)
    {
        if (gridSideLength == 0)
        {
            return position;
        }

        Vector3 gridPosition = new Vector3(
            gridSideLength * Mathf.Round(position.x / gridSideLength),
            gridSideLength * Mathf.Round(position.y / gridSideLength),
            gridSideLength * Mathf.Round(position.z / gridSideLength)
        );
        return gridPosition;
    }
}
