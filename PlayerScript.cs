using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public float maxHealth;
    public float playerHealth;

    public bool canBeDamaged;

    public Text text; //This is for the HUD

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = maxHealth;
        text.text = "Health: " + playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth == 0)
            {

            //Do game over
        }
    }

    public void loseHealth() // Activate after a collision. Can also make this an OnCollisionEnter here. 

    {
        if (canBeDamaged)
        //  playerHealth = playerHealth - obstacle.Damage;

        text.text = "Health: " + playerHealth;
        StartCoroutine(mercyInvincibility());
    }

    IEnumerator mercyInvincibility()
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(.5f);
        canBeDamaged = true;
    }
}
