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

    private Vector3 initialPosition;
    private KickSimulator kickSimulator;

    const float speed = 2;

    [SerializeField]
    float targetPosition;
    [SerializeField]
    float currentPosition;
    [SerializeField]
    float deltaPosition;

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
        var data = GameManager.Instance.GetMovementManager().GetState(laneId);
        var state = data.Item1;
        var timestamp = data.Item2;

        currentPosition = transform.position.z;

        targetPosition = initialPosition.z + state.position;
        targetPosition = calculateLimit(currentPosition, targetPosition);

        deltaPosition = targetPosition - currentPosition;

        transform.Translate(0, deltaPosition * Time.deltaTime * speed, 0);

        if (state.kick) {
            kickSimulator.Kick(timestamp);
        }
    }
}
