﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KeyboardMove : MonoBehaviour
{

    Rigidbody rigidbody;

    [SerializeField]
    Vector3 forceMultiplier;

    [SerializeField]
    private Vector3 velocity;

    [SerializeField]
    private Vector3 velocity_threshold;

    [SerializeField]
    Vector3 centerFieldPull;
    [SerializeField]
    Vector3 centerFieldPullForce;
    [SerializeField]
    ForceMode pullForceType;

    [SerializeField]
    private bool eternalMovement;

    HashSet<Collision> kickerCollisions;

    // Start is called before the first frame update
    void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();
        kickerCollisions = new HashSet<Collision>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rigidbody.velocity;
        var movementNormal = Vector3.zero;

        movementNormal.x += (Input.GetKey(KeyCode.D) ? 1 : 0) + (Input.GetKey(KeyCode.A) ? -1 : 0);
        movementNormal.y = (Input.GetKey(KeyCode.Space) ? 1 : 0);
        movementNormal.z = (Input.GetKey(KeyCode.W) ? 1 : 0) + (Input.GetKey(KeyCode.S) ? -1 : 0);

        movementNormal.Scale(forceMultiplier);
        movementNormal *= Time.deltaTime;

        rigidbody.AddForce(movementNormal);

        var target = new Vector3(0, 0, 0);
        centerFieldPull = target - this.transform.position;
        centerFieldPull.Scale(centerFieldPullForce * Time.deltaTime);
        
        rigidbody.AddForce(centerFieldPull, pullForceType);

        if (Input.GetKeyUp(KeyCode.G)) {
            eternalMovement = !eternalMovement;
        }

        if (eternalMovement) {
            if (
                Mathf.Abs(velocity.x) < velocity_threshold.x
                && Mathf.Abs(velocity.z) < velocity_threshold.z
                && kickerCollisions.Count == 0
            ) {

                velocity.Scale(forceMultiplier/4);
                velocity *= Time.deltaTime;

                rigidbody.AddForce(velocity);
            }
            
        }
    }

    private void OnCollisionStay(Collision collision) {
        
        if (collision.gameObject.GetComponent<Kicker>() == null) return;

        kickerCollisions.Add(collision);
    }

    private void OnCollisionExit(Collision collision) {

        if (kickerCollisions.Contains(collision)) {
            kickerCollisions.Remove(collision);
        }

    }

    private void OnTriggerEnter(Collider other) {

        var resetHandler = this.GetComponent<ResetObject>();
        resetHandler.InitReset();
        resetHandler.EndReset();

        rigidbody.AddForce(-centerFieldPull*2, ForceMode.VelocityChange);
    }
}
