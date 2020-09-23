using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class MotorMove : MonoBehaviour
{    
    
    private float limitTranslateRod1 = 10.5f;
    private float limitTranslateRod2 = 17.0f;
    private float limitTranslateRod4 = 3.5f;
    private float limitTranslateRod6 = 13.5f; 
    private JsonGenerate decisionServerScript;

    private float calculateLimit(float positionZ, float movement, float limitTranslate)
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
        if (gameObject.name == "Rod1")
        {
            float movement = calculateLimit(gameObject.transform.position.z, decisionServerScript.json["laneID1"]["position"], limitTranslateRod1);
            float rotacao = decisionServerScript.json["laneID1"]["rotation"];
            transform.Translate(0, movement * Time.deltaTime, 0);
            // transform.Rotate(0, 0, 0);
            transform.RotateAround(gameObject.transform.position, Vector3.forward, rotacao * Time.deltaTime);
        } else if (gameObject.name == "Rod2")
        {
            float movement = calculateLimit(gameObject.transform.position.z, decisionServerScript.json["laneID2"]["position"], limitTranslateRod2);
            float rotacao = decisionServerScript.json["laneID2"]["rotation"];
            transform.Translate(0, movement * Time.deltaTime, 0);
            transform.RotateAround(gameObject.transform.position, Vector3.forward, rotacao * Time.deltaTime);
        } else if (gameObject.name == "Rod4")
        {
            float movement = calculateLimit(gameObject.transform.position.z, decisionServerScript.json["laneID3"]["position"], limitTranslateRod4);
            float rotacao = decisionServerScript.json["laneID3"]["rotation"];
            transform.Translate(0, movement * Time.deltaTime, 0);
            transform.RotateAround(gameObject.transform.position, Vector3.forward, rotacao);
        } else if (gameObject.name == "Rod6")
        {
            float movement = calculateLimit(gameObject.transform.position.z, decisionServerScript.json["laneID4"]["position"], limitTranslateRod6);
            float rotacao = decisionServerScript.json["laneID4"]["rotation"];
            transform.Translate(0, movement * Time.deltaTime, 0);
            transform.RotateAround(gameObject.transform.position, Vector3.forward, rotacao * Time.deltaTime);
        }
    }
}
