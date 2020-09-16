using PUDM;
using PUDM.DataObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // game mamanger is a singleton
    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    private PUDMClient pudmClient;
    private Task connectTask;

    [SerializeField]
    string hostUri;

    [SerializeField]
    Vector2 fieldSize;
    [SerializeField]
    GameObject referencePoint;
    [SerializeField]
    List<GameObject> laneGameObjects;

    List<PUDM.DataObjects.Lane> lanesState;
    
    async void Start()
    {
        if (Instance is null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }

        this.pudmClient = new PUDMClient(this.hostUri);
        
        var initialID = 0;
        lanesState = new List<Lane>();
        foreach (var lane in laneGameObjects) {
            lanesState.Add(new Lane(initialID++, getYfromReferencePoint(lane), lane));
        }

        connectTask = this.pudmClient.Connect();
    }
    
    private async void OnDestroy()
    {
        Debug.Log("Destroying GameManager and pudmClient");
        this.pudmClient.Disconnect();
    }
    
    async void Update() {
        
        updateLaneInformation();

        if (this.pudmClient.Connected == false)
        {
            await this.connectTask;
        }
        else
        {
            await this.pudmClient.PublishQueue();
        }
    }

    void updateLaneInformation() { }

    private float getYfromReferencePoint(GameObject target) {
        var dist = referencePoint.transform.position - target.transform.position;

        return Mathf.Abs(dist.z);
    }

    public void SendUpdate(byte[] image) {
        pudmClient.SendUpdate(image, this.lanesState);
    }

}
