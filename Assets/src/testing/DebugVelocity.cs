using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugVelocity : MonoBehaviour
{
    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        Debug.DrawLine(transform.position, (transform.position + rigidbody.velocity), Color.magenta);
    }

    void OnCollisionEnter(Collision collision) {

        Debug.DrawLine(collision.GetContact(0).point, transform.position, Color.cyan, 1);
    }

    
    
}
