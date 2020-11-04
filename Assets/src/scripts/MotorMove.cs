using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngineInternal;

public class MotorMove : MonoBehaviour
{

    // private float limitTranslateRod1 = 10.5f;
    // private float limitTranslateRod2 = 17.0f;
    // private float limitTranslateRod4 = 3.5f;
    // private float limitTranslateRod6 = 13.5f; 

    [SerializeField]
    private float limitTranslate;

    [SerializeField]
    private int laneId;

    [SerializeField]
    private int player_number;

    private Vector3 initialPosition;
    private KickSimulator kickSimulator;

    [SerializeField]
    float targetPosition;
    [SerializeField]
    float currentPosition;
    [SerializeField]
    float deltaPosition;

    long lastTimestamp = 0;
    const float speed = 2;

    enum MotorState {Moving, Idle};
    MotorState currentState = MotorState.Idle;


    private float calculateLimit(float positionZ, float movement)
    {
        if (positionZ + movement > limitTranslate)
        {
            movement = limitTranslate - positionZ;
        }

        if (positionZ + movement < -limitTranslate)
        {
            movement = -limitTranslate - positionZ;
        }

        return movement;
    }

    private void Start() {
        this.initialPosition = transform.position;
        this.kickSimulator = GetComponent<KickSimulator>();
    }


    void Update()
    {
        var data = GameManager.GetInstance(player_number).GetMovementManager().GetState(laneId);
        var state = data.Item1;
        var timestamp = data.Item2;
    
        currentPosition = transform.position.z;

        if (lastTimestamp != timestamp) {
            currentState = MotorState.Moving;
    
            if (this.player_number == 0) {
                targetPosition = initialPosition.z + state.position;
            } else {
                targetPosition = initialPosition.z - state.position;
            }
            targetPosition = calculateLimit(currentPosition, targetPosition);
        }

        if (Mathf.Approximately(currentPosition, targetPosition)) {
            currentState = MotorState.Idle;
        }

        deltaPosition = targetPosition - currentPosition;

        if(currentState == MotorState.Moving) {
            transform.Translate(0, deltaPosition * Time.deltaTime * speed, 0);
        }

        if (state.kick) {
            kickSimulator.Kick(timestamp);
        }
    }
}
