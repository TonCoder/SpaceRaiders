using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_Movement : MonoBehaviour
{
    public Rigidbody rBody;
    public Animator animtr;
    public SO_ShipStats ship;
    private float prevSpeed;
    float torqueRamp = 0;
    float newHorizontalPos = 0;
    float power;
    float moveBetweenLanes;

    [Header("Boost Settings")]
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private float shipVelocity;
    [SerializeField, Range(1f, 99f)] private float boostSpeed;
    [SerializeField] private float boostTime;
    [SerializeField] bool isBoosting = false;

    [Header("Lane position Settings")]
    [SerializeField] internal bool canMoveFreely = false;
    [SerializeField] private Transform[] _lanePositions;
    private int laneIndex = 0;


    public float ShipVelocity
    {
        get {
            shipVelocity = rBody.velocity.magnitude;
            return  shipVelocity;
        }
    }

    // Use this for initialization
    void Start()
    {
        animtr = GetComponentInChildren<Animator>();
    }
 
    void Update(){
        if (GameManager.instance.IsGameStarted())
        {
            SetMoveDirectionForAnimation();
        }
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
        moveBetweenLanes += Input.GetAxis("Horizontal") * ship.turnSpeed * Time.deltaTime;
        moveBetweenLanes = Mathf.Clamp(moveBetweenLanes, _lanePositions[0].position.x, _lanePositions[1].position.x);

        rBody.position = new Vector3(moveBetweenLanes, rBody.position.y, rBody.position.z);
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


    void SetMoveDirectionForAnimation(){
        animtr.SetFloat("roll", Input.GetAxis("Horizontal"));
    }

    void MoveForward()
    {
        if (ShipVelocity < ship.maxSpeed){
            // Use CurrentGasPedalAmount as input (Vertical) value 1 and -1 (1 for forward, -1 for revers, 0 for idle)
            // rBody.AddForce(CurrentGasPedalAmount * transform.forward * enginePower * Time.fixedDeltaTime);
            power = ship.enginePower * Time.fixedDeltaTime * 1 * ship.torque;
            rBody.AddForce(Vector3.forward *  Mathf.Abs(power));
        }
        else {
            rBody.AddForce(Vector3.forward * power);
        }
        // speedText.text = "Speed: " + (int)ShipVelocity;
    }

    public void TestBoost()
    {
        if (!isBoosting)
        {
            StartCoroutine(Boost());
        }
    }

    IEnumerator Boost()
    {
        isBoosting = true;
        float prevMass = rBody.mass;
        rBody.mass = rBody.mass - boostSpeed;
        yield return new WaitForSeconds(boostTime);
        isBoosting = false;
        rBody.mass = prevMass;
    }


}