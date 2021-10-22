using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class RotationObject : MonoBehaviour
{
    //[SerializeField] Vector3.Euler rotationVector = Quaternion.Euler(10f, 10f, 10f, 10f);
    [SerializeField] float period = 2f;
    [SerializeField] Quaternion offset;

    [Range(0, 1)] [SerializeField] float rotationFactor; // 0 for not moved, 1 for fully moved.

    Quaternion startingRot;

    void Start()
    {
        startingRot = transform.rotation;
    }

/*    void Update()
    {
        // protecc against period is zero
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; // Grows continually from 0, like cycles

        const float tau = Mathf.PI * 2f; // About 6.28
        float rawSinWave = Mathf.Sin(cycles * tau); // goes from -1 to +1

        rotationFactor = rawSinWave / 2f + 0.5f;
        offset = rotationVector * rotationFactor;
        transform.position = startingRot + offset;
    }*/
}
