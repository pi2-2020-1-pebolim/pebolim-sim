using PUDM.DataObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonGenerate : MovementManager
{
    
    [SerializeField]
    private Vector2Int positionRange;
    
    [SerializeField]
    private float updateTime;

    private long currentTimestamp = 0;

    public Vector2Int PositionRange { get => positionRange; set => positionRange = value; }

    void MovementGenerator()
    {
        currentTimestamp++;

        for (int i = 0; i < laneCount; i++)
        {

            var desiredState = new DesiredState {
                laneID = i,
                position = Random.Range(positionRange.x, positionRange.y),
                kick = false
            };

            SetState(desiredState, currentTimestamp);
        }
    }

    public override void Initialize() {
        base.Initialize();

        InvokeRepeating("MovementGenerator", 0, updateTime);
    }
}
