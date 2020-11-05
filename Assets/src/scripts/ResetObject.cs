using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using WebSocketSharp;

public class ResetObject : MonoBehaviour
{

    [SerializeField]
    KeyCode resetKey = KeyCode.R;

    Vector3 originalPosition;
    Quaternion originalRotation;
    Rigidbody rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        originalPosition = this.transform.position;
        originalRotation = this.transform.rotation;

        try {
            rigidbody = GetComponent<Rigidbody>();
        } catch (NullReferenceException ex) {
            rigidbody = null;
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(resetKey)){
            InitReset();
        } else if (Input.GetKeyUp(resetKey)) {
            EndReset();
        }
    }

    public void InitReset() {
        
        if (this.rigidbody != null) {
            rigidbody.isKinematic = true;
            rigidbody.velocity = Vector3.zero;
        }

        transform.position = originalPosition;
    }

    public void EndReset() {

        if (this.rigidbody != null) {
            rigidbody.isKinematic = false;
        }

        transform.rotation = originalRotation;
    }
}
