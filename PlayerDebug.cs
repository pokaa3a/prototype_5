using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDebug : MonoBehaviour
{
    // [Body size]
    float _bodySizeX;
    float _bodySizeY;
    public Vector2 bodySize { get => new Vector2(_bodySizeX, _bodySizeY); }

    // [Scale]
    public Vector2 scale { get => (Vector2)transform.localScale; }

    // [Position]
    public Vector2 position
    {
        get => transform.position;
        set => transform.position = value;
    }    

    void Awake()
    {
        // BodySize
        var colliderSize = gameObject.GetComponent<CapsuleCollider2D>().size;
        _bodySizeX = colliderSize.x;
        _bodySizeY = colliderSize.y;

        var rect = new Rect(0f, 0f, 100f, 100f);
         Debug.DrawLine(new Vector3(rect.x, rect.y), new Vector3(rect.x + rect.width, rect.y ),Color.green);
         Debug.DrawLine(new Vector3(rect.x, rect.y), new Vector3(rect.x , rect.y + rect.height), Color.red);
         Debug.DrawLine(new Vector3(rect.x + rect.width, rect.y + rect.height), new Vector3(rect.x + rect.width, rect.y), Color.green);
         Debug.DrawLine(new Vector3(rect.x + rect.width, rect.y + rect.height), new Vector3(rect.x, rect.y + rect.height), Color.red);
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Vector2 offset = new Vector2(0f, bodySize.y * scale.y / 2f);
        
        // Draw a semitransparent blue cube at the transforms position
        // Gizmos.color = new Color(1, 0, 0, 0.5f);
        //     Gizmos.DrawCube(position,
        //         new Vector2(bodySize.x * scale.x, bodySize.y * scale.y));
    }
}
