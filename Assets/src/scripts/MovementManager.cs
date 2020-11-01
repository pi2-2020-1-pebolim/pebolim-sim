using PUDM;
using PUDM.DataObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementManager : MonoBehaviour
{
    protected Dictionary<int, Tuple<DesiredState, long>> currentStates;

    protected int laneCount;

    [SerializeField]
    private int player_number;

    public virtual void Initialize() {
        laneCount = GameManager.GetInstance(player_number).GetLaneDefinitions().Count;

        currentStates = new Dictionary<int, Tuple<DesiredState, long>>();
        for (var i = 0; i < laneCount; i++) {
            SetState(GetDefaultState(i), 0);
        }
    }


    void Start() {

        Initialize();
    }

    public void SetState(DesiredState state, long timestamp) {
        currentStates[state.laneID] = new Tuple<DesiredState, long>(state, timestamp);
    }

    public Tuple<DesiredState, long> GetState(int laneID) {
        return currentStates[laneID];
    }

    public DesiredState GetDefaultState(int laneID) {

        return new DesiredState {
            laneID = laneID,
            position = 0,
            kick = false
        };
    }
}

