using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kicker : MonoBehaviour
{
    [SerializeField]
    GameObject ball;

    [SerializeField]
    ConstantRotation rotationProvider;

    public float impulseAmount;

    void OnCollisionEnter(Collision collision) {

        if (collision.gameObject == ball) {

            var incomingVector = collision.rigidbody.velocity;

            Debug.DrawLine(collision.transform.position, (collision.transform.position + collision.impulse), Color.red, 3);
            Debug.DrawLine(collision.transform.position, (collision.transform.position + (collision.impulse * -1)), Color.yellow, 3);
            Debug.DrawLine(collision.transform.position, transform.position, Color.cyan, 3);

            // if ballDirection is negative, the collided object is coming from the front (on x)
            var ballDirection = Mathf.Sign(this.transform.position.x - collision.transform.position.x);

            // if rotation is positive, we are rotating from left to right (on x)
            var rotationDirecton = Mathf.Sign(rotationProvider.GetLastRotationStep());


            if (ballDirection != rotationDirecton) { 

                var extraImpulse = collision.impulse * impulseAmount;
                collision.rigidbody.AddForce(extraImpulse);

            } else {

                collision.rigidbody.AddForce(new Vector3(0,-1000,0));
            }

        }
    }
}
