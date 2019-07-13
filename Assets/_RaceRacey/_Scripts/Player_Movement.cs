using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Rigidbody rBody;
    public Animator animtr;

    [Space(10)]

    [Header("Vehicle Settings")]
    [SerializeField] private float turnSpeed;
    [SerializeField] private float drift, maxSpeed, enginePower, torque, shipVelocity;
    [SerializeField] float torqueRamp = 0.01f;
    [SerializeField] private bool canMoveFreely = false;

    [Header("Lane position Settings")]
    [SerializeField] private Transform[] _lanePositions;

    float torqueSpeed = 0f;
    float newHorizontalPos = 0;
    float power;

    private int laneIndex = 0;
    private Vector3 nrotate;
    private List<float> positionList = new List<float>();
    bool startedTorqueRev = false;
    
    public float ShipVelocity
    {
        get { 
            shipVelocity = rBody.velocity.magnitude; 
            return shipVelocity;
        }
    }

    // Use this for initialization
    void Start()
    {
        animtr = GetComponentInChildren<Animator>();
        Debug.Log(_lanePositions.Length - 1);

        // Set Torque value accordingly
        torqueSpeed =  100 / torque;

        for (int i = 0; i < _lanePositions.Length; i++)
        {
            positionList.Add(_lanePositions[i].position.z);
        }
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
        newHorizontalPos = Mathf.Lerp(transform.position.z, positionList[laneIndex], turnSpeed * Time.deltaTime);
        transform.position = new Vector3( transform.position.x, transform.position.y, newHorizontalPos);
    }

    void MoveNoConstraint(){
        newHorizontalPos = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        Vector3 newrotate = new Vector3(0, rBody.rotation.y * newHorizontalPos, 0);

        Quaternion deltaRotation = Quaternion.Euler(newrotate);
        
        rBody.AddRelativeTorque(0f, newHorizontalPos, 0f, ForceMode.VelocityChange);

        // calculate the current sideways speed by using the dot product. Tells us how much the ship is going left or right
        float sidewaysSpeed = Vector3.Dot(rBody.velocity, transform.right);
        // adds drift ability by setting friction
        Vector3 sideFriction = -transform.right * (sidewaysSpeed / Time.fixedDeltaTime / drift);
        // applies drift
        rBody.AddForce(sideFriction, ForceMode.Acceleration);
    }

    void PositionIndexConstraint()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            laneIndex--;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            laneIndex++;
        }
        if (laneIndex > positionList.Count - 1)
        {
            laneIndex = positionList.Count - 1;
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
        if(!startedTorqueRev)
            StartCoroutine(IncreaseTorque());

        if (shipVelocity < maxSpeed)
        {
            // Use CurrentGasPedalAmount as input (Vertical) value 1 and -1 (1 for forward, -1 for revers, 0 for idle)
            // rBody.AddForce(CurrentGasPedalAmount * transform.forward * enginePower * Time.fixedDeltaTime);
            power = enginePower * torqueRamp * Time.fixedDeltaTime;
            rBody.AddForce(transform.forward *  power);

            shipVelocity = rBody.velocity.magnitude;

        }else{
            rBody.AddForce(transform.forward * power);
        }

    }

    IEnumerator IncreaseTorque(){
        if(startedTorqueRev) yield return 0;

        yield return new WaitForSeconds(torqueSpeed);
        if (torqueRamp < 100)
        {
            torqueRamp += Time.deltaTime;
        }

    }

}