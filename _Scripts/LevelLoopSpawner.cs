using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoopSpawner : MonoBehaviour {

	public Transform endPoint;
	// Use this for initialization
	void Start () {
		endPoint = transform.parent.GetChild (1);
		if (endPoint.name != "EndPoint") {
			Debug.Log ("Please set EndPointBelow this Object, NOT AS CHILD");
			Debug.Break ();
		}
	}

	// Update is called once per frame

	void OnTriggerEnter (Collider obj) {
		if (obj.tag == "Player") {
			// Spawn next Random Path
			Transform nextPath = SimplePoolManager.instance.GetNextAvailablePoolItem ("Paths").transform;
			nextPath.transform.position = new Vector3 (endPoint.position.x, 0, endPoint.parent.position.z);
			nextPath.gameObject.SetActive (true);

			// Get all active Objects
			var activeList = SimplePoolManager.instance.GetAllActiveCategoryItem ("Paths");

			activeList.ForEach (x => {
				if (x.transform != transform.parent.transform && x.transform != nextPath.transform) {
					SimplePoolManager.instance.DisablePoolObject (x.transform);
				}
			});
		}
	}
}