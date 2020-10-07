using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kicker : MonoBehaviour
{
    [SerializeField]
    GameObject rotationProviderObj;

    IRotationProvider rotationProvider;

    public float impulseAmount;

    private void Start() {
        rotationProvider = rotationProviderObj.GetComponent<IRotationProvider>();
    }

    void OnCollisionEnter(Collision collision) {

        Debug.DrawLine(collision.transform.position, (collision.transform.position + collision.impulse), Color.red, 3);
        Debug.DrawLine(collision.transform.position, transform.position, Color.cyan, 3);

        if (rotationProvider.IsKicking()) { 

            var extraImpulse = collision.impulse * impulseAmount;
            Debug.DrawLine(collision.transform.position, (collision.transform.position + extraImpulse), Color.yellow, 3);
            collision.rigidbody.AddForce(extraImpulse);

        } else {

            var force = new Vector3(0, -1000, 0);
            collision.rigidbody.AddForce(force);

            Debug.DrawLine(collision.transform.position, (collision.transform.position + force), Color.green, 3);
        }

    }
    
}
