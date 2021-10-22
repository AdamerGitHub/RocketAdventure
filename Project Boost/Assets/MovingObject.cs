using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MovingObject : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;
    [SerializeField] Vector3 offset;

    [Range(0,1)] [SerializeField] float movementFactor; // 0 for not moved, 1 for fully moved.

    Vector3 startingPos;

    private bool isAcitve = false;

    void Start()
    {
        startingPos = transform.position;
    }

    void Update()
    {
        //isAcitve = false;

        // protecc against period is zero
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; // Grows continually from 0, like cycles

        const float tau = Mathf.PI * 2f; // About 6.28
        float rawSinWave = Mathf.Sin(cycles * tau); // goes from -1 to +1

        movementFactor = rawSinWave / 2f + 0.5f;
        offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }

}
