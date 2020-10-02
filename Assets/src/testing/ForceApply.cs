using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class ForceApply : MonoBehaviour
{

    [SerializeField]
    Vector3 force;
    Rigidbody rigidbody;

    [SerializeField]
    bool stopOnCollision;

    private bool applyForce = true;
    public bool ApplyForce { get => applyForce; set => applyForce = value; }

    // Start is called before the first frame update
    void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ApplyForce)
            rigidbody.AddForce(force * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) {
        
        if (collision.gameObject.tag == "Finish") {
            ApplyForce = false;
        }
    }
}
