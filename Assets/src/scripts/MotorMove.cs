using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorMove : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject decisionServer = GameObject.Find("DecisionServer");
        JsonGenerate decisionServerScript = decisionServer.GetComponent<JsonGenerate>();
        
        if (gameObject.name == "Rod1")
        {
            transform.Translate(0, decisionServerScript.json["Motor1"]["movimento"], 0);
            transform.Rotate(0, 0, 0);
        } else if (gameObject.name == "Rod2")
        {
            transform.Translate(0, decisionServerScript.json["Motor2"]["movimento"], 0);
            transform.Rotate(0, 0, 0);
        } else if (gameObject.name == "Rod4")
        {
            transform.Translate(0, decisionServerScript.json["Motor3"]["movimento"], 0);
            transform.Rotate(0, 0, 0);
        } else if (gameObject.name == "Rod6")
        {
            transform.Translate(0, decisionServerScript.json["Motor4"]["movimento"], 0);
            transform.Rotate(0, 0, 0);
        }
    }
}
