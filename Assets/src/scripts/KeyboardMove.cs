using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMove : MonoBehaviour
{

    Rigidbody rigidbody;

    [SerializeField]
    Vector3 forceMultiplier;
    
    // Start is called before the first frame update
    void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        var movementNormal = Vector3.zero;

        movementNormal.x += (Input.GetKey(KeyCode.D) ? 1 : 0) + (Input.GetKey(KeyCode.A) ? -1 : 0);
        movementNormal.y = (Input.GetKey(KeyCode.Space) ? 1 : 0);
        movementNormal.z = (Input.GetKey(KeyCode.W) ? 1 : 0) + (Input.GetKey(KeyCode.S) ? -1 : 0);

        movementNormal.Scale(forceMultiplier);
        movementNormal *= Time.deltaTime;

        rigidbody.AddForce(movementNormal);
    }
}
