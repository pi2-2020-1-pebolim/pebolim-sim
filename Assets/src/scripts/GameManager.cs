using PUDM;
using PUDM.DataObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // game mamanger is a singleton
    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    private PUDMClient pudmClient;

    [SerializeField]
    string hostUri;

    [SerializeField]
    Vector2 fieldSize;

    [SerializeField]
    GameObject captureCamera;

    [SerializeField]
    GameObject referencePoint;
    [SerializeField]
    List<GameObject> laneGameObjects;
    [SerializeField]
    List<float> movementLimits;

    List<PUDM.DataObjects.LaneDefinition> lanesDefinition;
    List<PUDM.DataObjects.LaneUpdate> lanesState;
    
    void Start()
    {
        if (Instance is null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }

        lanesDefinition = GetLanesDefinition();
        var field = new FieldDefinition(
            this.fieldSize.x,
            this.fieldSize.y,
            lanesDefinition
        );

        var cameraSettings = captureCamera.GetComponent<CameraGrabber>().GetCameraSettings();

        this.pudmClient = new PUDMClient(this.hostUri, field, cameraSettings);


        this.lanesState = CreateLaneUpdateList(lanesDefinition);
    }

    List<LaneDefinition> GetLanesDefinition() {

        var currentID = 0;
        var lanes = new List<LaneDefinition>();

        foreach (var lane in laneGameObjects) {
            lanes.Add(new LaneDefinition(currentID++, getXfromReferencePoint(lane), movementLimits[currentID-1], lane));
        }

        return lanes;
    }

    List<LaneUpdate> CreateLaneUpdateList(List<LaneDefinition> definitions) {
        var lanes = new List<LaneUpdate>();

        foreach (var definition in definitions) {
            var lane = new LaneUpdate(definition.laneID, definition.xPosition, definition.GetGameObject());
            lanes.Add(lane);
        }

        return lanes;
    }

    void updateLaneInformation() {
    
        foreach(var lane in this.lanesState) {
            lane.Update();
        }
    }

    void Update() {
        
        updateLaneInformation();
    }

    private async void OnDestroy() {
        Debug.Log("Destroying GameManager and pudmClient");
        this.pudmClient.End();
    }

    private float getXfromReferencePoint(GameObject target) {
        
        var dist = referencePoint.transform.position - target.transform.position;

        return Mathf.Abs(dist.x);
    }

    public void SendUpdate(byte[] image) {
        pudmClient.SendUpdate(image, lanesState);
    }

}
