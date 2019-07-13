using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Rigidbody rBody;
    public Animator animtr;
    public SO_ShipStats ship;

    
    [Header("Lane position Settings")]
    [SerializeField] internal bool canMoveFreely = false;
    [SerializeField] private Transform[] _lanePositions;


    float newHorizontalPos = 0;
    float power;
    float shipVelocity;

    private int laneIndex = 0;
    private Vector3 nrotate;
    
    bool startedTorqueRev = false;
    
    public float ShipVelocity
    {
        get {
            ship.shipVelocity = rBody.velocity.magnitude; 
            return ship.shipVelocity;
        }
    }

    // Use this for initialization
    void Start()
    {
        animtr = GetComponentInChildren<Animator>();
        Debug.Log(_lanePositions.Length - 1);

    }

 
    void Update(){
        if(!canMoveFreely){
            PositionIndexConstraint();
        }

        SetMoveDirectionForAnimation();
    }

    void FixedUpdate()
    {
        if(GameManager.instance.IsGameStarted()){
            if(canMoveFreely)
                MoveNoConstraint();
            else
                MoveBetweenLanes();
            
            MoveForward();
        }
    }

    void MoveBetweenLanes()
    {
        // FIX THIS, INCASE I NEED TO CHANGE AXIS
        newHorizontalPos = Mathf.Lerp(transform.position.x,_lanePositions[laneIndex].position.x, ship.turnSpeed * Time.deltaTime);
        transform.position = new Vector3(newHorizontalPos, transform.position.y, transform.position.z);
    }

    void MoveNoConstraint(){
        newHorizontalPos = Input.GetAxis("Horizontal") * ship.turnSpeed * Time.deltaTime;
        Vector3 newrotate = new Vector3(0, rBody.rotation.y * newHorizontalPos, 0);

        Quaternion deltaRotation = Quaternion.Euler(newrotate);
        
        rBody.AddRelativeTorque(0f, newHorizontalPos, 0f, ForceMode.VelocityChange);

        // calculate the current sideways speed by using the dot product. Tells us how much the ship is going left or right
        float sidewaysSpeed = Vector3.Dot(rBody.velocity, transform.right);
        // adds drift ability by setting friction
        Vector3 sideFriction = -transform.right * (sidewaysSpeed / Time.fixedDeltaTime / ship.drift);
        // applies drift
        rBody.AddForce(sideFriction, ForceMode.Acceleration);
    }

    void PositionIndexConstraint()
    {
        if (Input.GetKeyDown("left"))
        {
            laneIndex--;
        }
        else if (Input.GetKeyDown("right"))
        {
            laneIndex++;
        }
        
        if (laneIndex > (_lanePositions.Length - 1))
        {
            laneIndex = (_lanePositions.Length - 1);
        }
        if (laneIndex < 0)
        {
            laneIndex = 0;
        }
    }

    void SetMoveDirectionForAnimation(){
        animtr.SetFloat("roll", Input.GetAxis("Horizontal"));
    }

    void MoveForward()
    {
        if (shipVelocity < ship.maxSpeed)
        {
            // Use CurrentGasPedalAmount as input (Vertical) value 1 and -1 (1 for forward, -1 for revers, 0 for idle)
            // rBody.AddForce(CurrentGasPedalAmount * transform.forward * enginePower * Time.fixedDeltaTime);
            power = ship.enginePower  * Time.fixedDeltaTime * ship.rampSpeed;
            rBody.AddForce(Vector3.forward *  power);

            shipVelocity = rBody.velocity.magnitude;

        }else{
            rBody.AddForce(Vector3.forward * power);
        }

    }

}