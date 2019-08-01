using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public float hoverHeight;
    public float hoverStrength;

    [Tooltip("The amount of force used to push ship back to the ground")]
    public float HoverGravityForce;
    public float OrientationToGroundSpeed;

    public List<Transform> hoverSpots;
    public LayerMask layerToIgnore;
    public bool IsGrounded;
    public Rigidbody rbody;
    RaycastHit _groundRaycastHit;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();

        for (int i = 0; i < hoverSpots.Count; i++)
        {
            Debug.DrawRay(hoverSpots[i].position, hoverSpots[i].TransformDirection(Vector3.down) * hoverHeight, Color.yellow);
        }
    }

    void FixedUpdate()
    {
        HoverShip();
        OrientationToGround();
    }


    /// <summary>
    /// Management of the hover and gravity of the vehicle
    /// </summary>
    void HoverShip(){
        IsGrounded = false;

        for (int i = 0; i < hoverSpots.Count; i++)
        {
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(hoverSpots[i].position, Vector3.down, out _groundRaycastHit, hoverHeight, ~layerToIgnore))
            {
                if(_groundRaycastHit.distance <= hoverHeight){
                    IsGrounded = true;

                    // we determine the distance between current vehicle height and wanted height
                    float distanceVehicleToHoverPosition = hoverHeight - _groundRaycastHit.distance;

                    float force = distanceVehicleToHoverPosition * hoverHeight;

                    // we add the hoverforce to the rigidbody
                    //  rbody.AddForceAtPosition(Vector3.up * force * Time.fixedDeltaTime, ForceMode.Acceleration);
                    rbody.AddForceAtPosition(Vector3.up * hoverStrength * (1.0f - (_groundRaycastHit.distance / hoverHeight)), hoverSpots[i].position);
                }
            }
            else 
            {
                // If distance between vehicle and ground is higher than target height, we apply a force from up to
                // bottom (gravity) to push the vehicle down.
                rbody.AddForce(-Vector3.up * HoverGravityForce, ForceMode.Force);
            }

        }   
    }

    /// <summary>
    /// Manages orientation of the vehicle depending ground surface normale 
    /// </summary>
    protected virtual void OrientationToGround()
    {
        var rotationTarget = Quaternion.FromToRotation(transform.up, _groundRaycastHit.normal) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, Time.fixedDeltaTime * OrientationToGroundSpeed);
    }

    private void OnDrawGizmos() {
        
        if(IsGrounded){
            for (int i = 0; i < hoverSpots.Count; i++)
            {
                Gizmos.color = Color.red;
                Debug.DrawRay(hoverSpots[i].position, hoverSpots[i].TransformDirection(Vector3.down) * hoverHeight, Color.red);
                Gizmos.DrawRay(hoverSpots[i].transform.position, Vector3.down);
            }
        }else{
            for (int i = 0; i < hoverSpots.Count; i++)
            {
                Gizmos.color = Color.yellow;
                Debug.DrawRay(hoverSpots[i].position, hoverSpots[i].TransformDirection(Vector3.down) * hoverHeight, Color.yellow);
                Gizmos.DrawRay(hoverSpots[i].transform.position, Vector3.down);
            }
        }
        
    }
}
