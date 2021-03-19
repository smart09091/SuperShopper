using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackOnCollision : MonoBehaviour
{
    [SerializeField]
    private float knockbackStrength;
    void Start(){
        GameEvents.Instance.onBeforeResettingPhysicsObjects += ResetConvexCollider;
    }
    private void OnCollisionEnter(Collision other){
        //Debug.Log(other.collider.name + " collided with" + gameObject.name);

        if(other.gameObject.tag == "Player"){
            PathMover player = other.gameObject.GetComponent<PathMover>();
            if(player != null){
                player.HandlePlayerDuringCollision();
            }

            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if(rb != null){
                Vector3 direction = other.transform.position - transform.position;

                rb.AddForce(direction.normalized * knockbackStrength, ForceMode.Impulse);

                gameObject.GetComponent<MeshCollider>().convex = true;
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                gameObject.GetComponent<Rigidbody>().AddForce(-direction.normalized * knockbackStrength, ForceMode.Impulse);
            }
        }
    }

    void ResetConvexCollider(){
        gameObject.GetComponent<MeshCollider>().convex = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
}
