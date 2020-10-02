using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{

    [SerializeField]
    float constanteAngles;

    float lastRotationStep;

    // Update is called once per frame
    void Update()
    {
        lastRotationStep = constanteAngles * Time.deltaTime;
        transform.RotateAround(transform.position, Vector3.forward, lastRotationStep);    
    }

    public float GetLastRotationStep() {
        return lastRotationStep;
    }
}
