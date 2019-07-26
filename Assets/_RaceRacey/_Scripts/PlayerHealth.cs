using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//***************************************************
// Code created by Forest - Discord SN: KaijuMittens
//***************************************************
public class PlayerHealth: MonoBehaviour
{
    public float maxHealth;
    public float playerHealth;
    public bool canBeDamaged;
    public float invisibilityTimer = 1f;

    public Slider healthSlider; //This is for the HUD

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = maxHealth;
        healthSlider.maxValue = playerHealth;
        healthSlider.value = playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth == 0)
        {
            // GameManager.instance.game
        }
    }

    public void loseHealth(float hitValue) // Activate after a collision. Can also make this an OnCollisionEnter here. 
    {
        if (canBeDamaged)
            playerHealth = playerHealth - hitValue;

        healthSlider.value  = playerHealth;
        StartCoroutine(mercyInvincibility());
    }

    IEnumerator mercyInvincibility()
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(invisibilityTimer);
        canBeDamaged = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Obstacle"){
            loseHealth(Random.Range(10,20));
            SimplePoolManager.instance.DisablePoolObject(other.transform);
        }
    }
}
