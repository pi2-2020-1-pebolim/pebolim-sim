﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

public class KickSimulator : MonoBehaviour, IRotationProvider
{

    [SerializeField]
    float constanteAngles;

    float lastRotationStep;
    private float lastAngle;
    private long lastKickTimestamp = 0;

    [SerializeField]
    Vector2 kickLimits;

    [SerializeField]
    axisOptions axis;


    [SerializeField]
    float eulerAngX;
    [SerializeField]
    float eulerAngY;
    [SerializeField]
    float eulerAngZ;

    public enum axisOptions { X, Y, Z };

    float target;

    KickStates currentState = KickStates.Idle;
    enum KickStates{ Idle, Kicking, Retracting };

    void Start() {
        target = kickLimits.x;
    }

    public void Kick(long timestamp) {

        if (timestamp == lastKickTimestamp) return;

        if (currentState != KickStates.Kicking) {
            currentState = KickStates.Kicking;
            target = kickLimits.y;
            lastKickTimestamp = timestamp;
        }
    }

    void Update() {

        eulerAngX = transform.localEulerAngles.x;
        eulerAngY = transform.localEulerAngles.y;
        eulerAngZ = transform.localEulerAngles.z;

        UpdateState();

        if (Input.GetKeyUp(KeyCode.Space)) {
            // facking the timestamp value
            Kick(lastKickTimestamp + 1);
        }

        // save the data for the next frame
        lastAngle = GetCurrentAngle();        
        lastRotationStep = CalculateRotation(GetCurrentAngle(), target);
        transform.RotateAround(transform.position, Vector3.forward, lastRotationStep);
    }

    private void UpdateState() {

        if (Mathf.Approximately(GetCurrentAngle(), target)) {

            switch (currentState) {
                case KickStates.Kicking:
                        target = kickLimits.x;
                        currentState = KickStates.Retracting;
                    break;
                case KickStates.Retracting:
                        currentState = KickStates.Idle;
                    break;
                default:
                    break;
            }

        }

    }

    float GetCurrentAngle() {

        var avaliableAxis= new Dictionary<axisOptions, float>(){
            {axisOptions.X, transform.rotation.eulerAngles.x},
            {axisOptions.Y, transform.rotation.eulerAngles.y},
            {axisOptions.Z, transform.rotation.eulerAngles.z}
        };

        return avaliableAxis[this.axis];
    }

    float CalculateRotation(float current, float target) {

        if (currentState == KickStates.Idle) {
            return 0;
        }

        var deltaDistance = Mathf.Abs(target - current);

        var anglesToWalk = Time.deltaTime * constanteAngles;

        if (deltaDistance < Mathf.Abs(anglesToWalk)) {
            anglesToWalk = deltaDistance * (constanteAngles < 0 ? -1: 1);
        }

        return (currentState == KickStates.Kicking ? 1 : -1) * anglesToWalk;
    }

    public float GetLastRotationStep() {
        return lastRotationStep;
    }

    public bool IsKicking() {
        return currentState == KickStates.Kicking;
    }
}