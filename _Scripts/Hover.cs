using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public float hitDistance;
    public float hoverStrength;
    public List<Transform> hoverSpots;
    public LayerMask layerToIgnore;

    public Rigidbody rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();

        for (int i = 0; i < hoverSpots.Count; i++)
        {
            Debug.DrawRay(hoverSpots[i].position, hoverSpots[i].TransformDirection(Vector3.down) * hitDistance, Color.yellow);
        }
    }

    void FixedUpdate()
    {
        FloorContact();
    }

    void FloorContact(){
        RaycastHit hit;

        for (int i = 0; i < hoverSpots.Count; i++)
        {
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(hoverSpots[i].position, Vector3.down, out hit, hitDistance, ~layerToIgnore))
            {
                rbody.AddForceAtPosition(Vector3.up * hoverStrength * (1.0f - (hit.distance / hitDistance)), hoverSpots[i].position);

                // Draw ray line
                Debug.DrawRay(hoverSpots[i].position, hoverSpots[i].TransformDirection(Vector3.down) * hitDistance, Color.red);

            }else{

                // Draw ray line
                Debug.DrawRay(hoverSpots[i].position, hoverSpots[i].TransformDirection(Vector3.down) * hitDistance, Color.yellow);

                if(transform.position.y > hoverSpots[i].position.y){
                    rbody.AddForceAtPosition(hoverSpots[i].up * hoverStrength, hoverSpots[i].position);
                }else{
                    rbody.AddForceAtPosition(hoverSpots[i].up * -hoverStrength, hoverSpots[i].position);
                }
            }

        }   
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;

        for (int i = 0; i < hoverSpots.Count; i++)
        {
            Gizmos.DrawRay(hoverSpots[i].transform.position, Vector3.down);
        }
    }
}
