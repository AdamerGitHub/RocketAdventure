using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacleScript : MonoBehaviour
{
    public Transform damageBlock;
    public Transform obstacle;

    public float speed = .5f; // Max 1
    public float saveSize = 2;

    private Vector3 distanationPos; // DistanationPosition
    private int direction = 0; // 1 = to distanation, 0 = to rest

    // Start is called before the first frame update
    void Start()
    {
        distanationPos = damageBlock.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == 0)
        {
            damageBlock.position += Vector3.Normalize(obstacle.position - damageBlock.position) * speed;

            if ((obstacle.position - damageBlock.position).magnitude <= speed + saveSize)
            {
                direction = 1;
            }
        } else if (direction == 1)
        {
            damageBlock.position += Vector3.Normalize(distanationPos - damageBlock.position) * speed;

            if ((distanationPos - damageBlock.position).magnitude <= speed)
            {
                direction = 0;
            }
        }
    }

    // Return signed (where values 1 or -1) vector
    /* private Vector3 Vector3Sign(Vector3 vector)
    {
        return new Vector3(Mathf.Sign(vector.x), Mathf.Sign(vector.y), Mathf.Sign(vector.z));
    } */
}
