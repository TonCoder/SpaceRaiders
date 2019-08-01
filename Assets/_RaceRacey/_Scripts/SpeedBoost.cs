using System.Collections;
using UnityEngine;

//***************************************************
// Code created by - Discord SN: Yuriohs
//***************************************************
public class SpeedBoost : MonoBehaviour {

    public GameObject boost;

    //public GameObject player; //placeholdder, since PlayerScript seems to determine what the player is, can't reference player gameobject(I think)
    public Rigidbody rBody;

    [SerializeField] private Vector3 baseSpeed;
    [SerializeField] private float speedAddition;
    [SerializeField] private float boostTime;

    private float maxSpeed; // will possibly be used as a speed cap
    private float playerSpeed;
    bool isboosting = false;

    // public void gainSpeed()
    // {
    //     if (isboosting)
    //         playerSpeed = baseSpeed + speedAddition;
    //     else
    //         playerSpeed = baseSpeed;
    // }

    // private void OnTriggerEnter(Collider other)
    // {
    //     speedAddition = 5; // example speed boost valaue
    //     isboosting = true;
    //     if (other.tag == "Booster")
    //     {
    //         playerSpeed = baseSpeed + speedAddition;
    //     }
    // }

    float prevSpeed;
    bool IsBackToSpeed = true;

    public void TestBoost(){
        prevSpeed = rBody.velocity.magnitude;
        if(!isboosting) {
            StartCoroutine(StopBoost());
            StartCoroutine(Boost());
        }
    }

    IEnumerator Boost(){
        isboosting = true;
        while(true){
            rBody.AddForce(transform.forward * speedAddition, ForceMode.Impulse);
            yield return 0;
        }
    }

    IEnumerator StopBoost(){
        yield return new WaitForSeconds(boostTime);
        StopCoroutine(Boost());
        IsBackToSpeed = false;

        while(!IsBackToSpeed){
            Debug.Log("slowing");

            rBody.AddRelativeForce(Vector3.forward * -1);
            yield return new WaitForSeconds(0.01f);
            BackToSpeed();
        }

        Debug.Log("STop Boost");
        isboosting = false;

    }

    void BackToSpeed(){
        float newSpeed = rBody.velocity.magnitude;
        if (newSpeed <= prevSpeed)
        {
            Debug.Log(prevSpeed +"  currentSPd: " +  newSpeed);
            IsBackToSpeed = true;
            StopCoroutine(StopBoost());
        }
    }
}