using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveForward : MonoBehaviour {

	[SerializeField] private float speed = 10;

	public void SetSpeed (float value) {
		speed = value;
	}

	void Update () {
		var moveVelocity = speed * Time.deltaTime * transform.forward;
		transform.position += moveVelocity;
	}
}