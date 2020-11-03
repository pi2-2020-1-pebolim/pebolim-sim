using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Kicker : MonoBehaviour
{
    [SerializeField]
    GameObject rotationProviderObj;

    [SerializeField]
    bool isSecondPlayer;

    IRotationProvider rotationProvider;
    private bool localIsKicking;

    public float impulseAmount;

    HashSet<Collision> stayCollisions;

    private void Start() {
        rotationProvider = rotationProviderObj.GetComponent<IRotationProvider>();
        stayCollisions = new HashSet<Collision>();
    }

    void KickCollision(Collision collision) {

        var vector = (isSecondPlayer ? Vector3.left : Vector3.right);

        var extraImpulse = vector * impulseAmount;
        collision.rigidbody.AddForce(extraImpulse, ForceMode.Impulse);

        Debug.DrawLine(collision.transform.position, (collision.transform.position + extraImpulse), Color.yellow, 3); ;
    }

    void OnCollisionEnter(Collision collision) {

        Debug.DrawLine(collision.transform.position, (collision.transform.position + collision.impulse), Color.red, 3);
        Debug.DrawLine(collision.transform.position, transform.position, Color.cyan, 3);

        if (rotationProvider.IsKicking()) {
            KickCollision(collision);
        }

    }

    private void Update() {

        if (localIsKicking == false && rotationProvider.IsKicking()){
            
            foreach(var col in stayCollisions){
                KickCollision(col);
            }
        }

        localIsKicking = rotationProvider.IsKicking();
    }

    private void OnCollisionStay(Collision collision) {
        stayCollisions.Add(collision);
    }

    private void OnCollisionExit(Collision collision) {
        
        if (stayCollisions.Contains(collision)) {
            stayCollisions.Remove(collision);
        }

    }
}
