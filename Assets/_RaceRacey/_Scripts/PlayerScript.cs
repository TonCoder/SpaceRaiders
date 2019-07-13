using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float maxHealth;
    public float playerHealth;
    public bool canBeDamaged;

    public TextMeshProUGUI text; //This is for the HUD

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
            // GameManager.instance.game
        }
    }

    public void loseHealth(float hitValue) // Activate after a collision. Can also make this an OnCollisionEnter here. 
    {
        if (canBeDamaged)
            playerHealth = playerHealth - hitValue;

        text.text = "Health: " + playerHealth;
        StartCoroutine(mercyInvincibility());
    }

    IEnumerator mercyInvincibility()
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(.5f);
        canBeDamaged = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Obstacle"){
            loseHealth(Random.Range(10,50));
            SimplePoolManager.instance.DisablePoolObject(other.transform);
        }
    }
}
