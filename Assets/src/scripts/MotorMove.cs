using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class MotorMove : MonoBehaviour
{    
    
    private float limitTranslate = 17.0f;
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
        if (gameObject.name == "Rod1")
        {
            float movement = calculateLimit(gameObject.transform.position.z, decisionServerScript.json["Motor1"]["movimento"]);
            float rotacao = decisionServerScript.json["Motor1"]["rotacao"];
            transform.Translate(0, movement * Time.deltaTime, 0);
            // transform.Rotate(0, 0, 0);
            transform.RotateAround(gameObject.transform.position, Vector3.forward, rotacao * Time.deltaTime);
        } else if (gameObject.name == "Rod2")
        {
            float movement = calculateLimit(gameObject.transform.position.z, decisionServerScript.json["Motor2"]["movimento"]);
            float rotacao = decisionServerScript.json["Motor2"]["rotacao"];
            transform.Translate(0, movement * Time.deltaTime, 0);
            transform.RotateAround(gameObject.transform.position, Vector3.forward, rotacao * Time.deltaTime);
        } else if (gameObject.name == "Rod4")
        {
            float movement = calculateLimit(gameObject.transform.position.z, decisionServerScript.json["Motor3"]["movimento"]);
            float rotacao = decisionServerScript.json["Motor3"]["rotacao"];
            transform.Translate(0, movement * Time.deltaTime, 0);
            transform.RotateAround(gameObject.transform.position, Vector3.forward, rotacao * Time.deltaTime);
        } else if (gameObject.name == "Rod6")
        {
            float movement = calculateLimit(gameObject.transform.position.z, decisionServerScript.json["Motor4"]["movimento"]);
            float rotacao = decisionServerScript.json["Motor4"]["rotacao"];
            transform.Translate(0, movement * Time.deltaTime, 0);
            transform.RotateAround(gameObject.transform.position, Vector3.forward, rotacao * Time.deltaTime);
        }
    }
}
