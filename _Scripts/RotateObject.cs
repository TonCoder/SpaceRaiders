using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {

	public float rotateSpeed = 1;

	public Vector3 axistoRotate;
		
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(axistoRotate.x * rotateSpeed, axistoRotate.y * rotateSpeed, axistoRotate.z * rotateSpeed));	
	}
}
