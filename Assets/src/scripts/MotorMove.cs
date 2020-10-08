using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private string laneId;

    private JsonGenerate decisionServerScript;

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

    // Start is called before the first frame update
    void Start()
    {
        decisionServerScript = GameObject.Find("DecisionServer").GetComponent<JsonGenerate>();
    }

    // Update is called once per frame
    void Update()
    {
        float movement = calculateLimit(gameObject.transform.position.z, decisionServerScript.json[laneId]["position"]);
        //float rotacao = decisionServerScript.json[laneId]["rotation"];
        transform.Translate(0, movement * Time.deltaTime, 0);
        //transform.RotateAround(gameObject.transform.position, Vector3.forward, rotacao * Time.deltaTime);
    }
}
