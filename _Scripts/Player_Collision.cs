using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collision : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	private void OnTriggerEnter (Collider obj) {

		switch (obj.tag) {
			case "Obstacles":
				// Hurt, slow or stop player
				break;
			case "Collectibles":
				// Hurt, check effect to add to player
				// or add point if its point
				break;
			case "Bullets":
				// Hurt,player
				break;
			default:
				// Do nothing ?
				break;
		}
	}
}