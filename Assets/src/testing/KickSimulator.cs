using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class KickSimulator : MonoBehaviour, IRotationProvider
{

    [SerializeField]
    float constanteAngles;

    float lastRotationStep;
    private float lastAngle;

    [SerializeField]
    Vector2 kickLimits;

    bool clockwise = true;
    float target;

    void Start() {
        target = kickLimits.x;
    }

    void Update() {

        UpdateDirection();
        
        lastRotationStep = CalculateRotation();
        transform.RotateAround(transform.position, Vector3.forward, lastRotationStep);
    }

    private void UpdateDirection() {

        // returns a value between 0 and 360
        var currentAngle = transform.rotation.eulerAngles.z;

        if (Mathf.Approximately(Mathf.Round(currentAngle - lastRotationStep), Mathf.Round(lastAngle)) == false) {
            currentAngle =  (this.clockwise ? -(360 - currentAngle) : 360 + currentAngle);
        }

        if (GoneTroughTarget(target, currentAngle, currentAngle - lastRotationStep)) {
            
            target = (Mathf.Approximately(target, kickLimits.x) ? kickLimits.y : kickLimits.x);
            clockwise = !clockwise;
        }

        lastAngle = transform.rotation.eulerAngles.z;
    }

    bool GoneTroughTarget(float target, float currentPosition, float startingPosition) {
        return Mathf.Min(startingPosition, currentPosition) <= target && target <= Mathf.Max(startingPosition, currentPosition);
    }

    float CalculateRotation() {
        return Time.deltaTime * (clockwise ? -1 : 1) * constanteAngles;
    }

    public float GetLastRotationStep() {
        return lastRotationStep;
    }

    public Boolean IsKicking() {
        return this.clockwise == false;
    }
}