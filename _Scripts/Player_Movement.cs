using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    public Rigidbody rBody;
    public MoveDir moveDirection;

    [SerializeField] private float speed, dashSpeed;

    [SerializeField] private Transform[] _movePositions;

    float newZPos = 0;
    public Animator animtr;
    private int positionIndx = 0;
    List<float> positionList = new List<float>();

    public enum MoveDir
    {
        left,
        right,
        forward,
        backwards
    }

    // Use this for initialization
    void Start()
    {
        animtr = GetComponentInChildren<Animator>();

        for (int i = 0; i < _movePositions.Length; i++)
        {
            positionList.Add(_movePositions[i].position.z);
        }
    }

 
    void Update(){
        PositionIndexConstraint();
        GetMoveDirectionForAnimation();
    }

    void FixedUpdate()
    {
        if(GameManager.instance.IsGameStarted()){
            MoveBetweenLanes();
            MoveForward();
        }
    }

    void MoveBetweenLanes()
    {
        newZPos = Mathf.Lerp(transform.position.z, positionList[positionIndx], dashSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPos);
    }

    void PositionIndexConstraint()
    {
        if (Input.GetKeyDown("left"))
        {
            positionIndx--;
        }
        else if (Input.GetKeyDown("right"))
        {
            positionIndx++;
        }
        if (positionIndx > positionList.Count - 1)
        {
            positionIndx = positionList.Count - 1;
        }
        if (positionIndx < 0)
        {
            positionIndx = 0;
        }
    }


    void GetMoveDirectionForAnimation(){
        animtr.SetFloat("roll", Input.GetAxis("Horizontal"));
    }

    void MoveForward()
    {
        rBody.AddForce(transform.forward * speed);
        // if (moveDirection == MoveDir.left)
        // {
        //     rBody.AddForce(Vector3.left * speed);
        // }
        // else if (moveDirection == MoveDir.right)
        // {
        //     rBody.AddForce(Vector3.right * speed);

        // }
        // else if (moveDirection == MoveDir.forward)
        // {
        //     rBody.AddForce(Vector3.forward * speed);

        // }
        // else if (moveDirection == MoveDir.backwards)
        // {
        //     rBody.AddForce(Vector3.back * speed);
        // }
    }

}