using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPosition : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Color color = Color.green;
        Gizmos.color = color;
        Gizmos.DrawCube(transform.position, new Vector3(7, 0.1f, 10));
    }
}
