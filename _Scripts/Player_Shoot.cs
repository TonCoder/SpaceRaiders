using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot : MonoBehaviour {
	[SerializeField] private bool autoFire = false;
	[SerializeField] private float fireSpeed = 0.5f;
	[SerializeField] private float bulletSpeed = 0.5f;
	[SerializeField] private string fireButtonName = "Fire1";
	[SerializeField] private Transform fireLocation;
	[SerializeField] private string shootingPointTagName = "ShootingPoint";
	[SerializeField] private GameObject ItemToInstantiate;
	[SerializeField] private ParticleSystem bulletFireFX;

	private bool IsFiring = false;
	// Use this for initialization
	void Start () {
		fireLocation = GameObject.FindGameObjectWithTag (shootingPointTagName).transform;
		if (autoFire) {
			StartCoroutine (Fire ());
		}
	}

	void Update () {
		if (Input.GetButton (fireButtonName) && !IsFiring && !autoFire) {
			IsFiring = true;
			StartCoroutine (FireByButton ());
		}
	}

	IEnumerator Fire () {
		fireLocation = fireLocation? fireLocation : GameObject.FindGameObjectWithTag (shootingPointTagName).transform;
		var bullet = SimplePoolManager.instance.GetNextAvailablePoolItem ("Bullets");
		// var bullet = Instantiate(ItemToInstantiate, fireLocation.position, Quaternion.identity);
		bullet.transform.position = fireLocation.position;
		bullet.transform.rotation = fireLocation.rotation;

		var bulletScript = bullet.GetComponent<AutoMoveForward> ();
		bulletScript.SetSpeed (bulletSpeed);
		bullet.SetActive (true);

		bulletFireFX.Play (true);
		yield return new WaitForSeconds (fireSpeed);
		StartCoroutine (Fire ());
	}

	IEnumerator FireByButton () {
        fireLocation = fireLocation ? fireLocation : GameObject.FindGameObjectWithTag(shootingPointTagName).transform;
        var bullet = SimplePoolManager.instance.GetNextAvailablePoolItem("Bullets");
        // var bullet = Instantiate(ItemToInstantiate, fireLocation.position, Quaternion.identity);
        bullet.transform.position = fireLocation.position;
        bullet.transform.rotation = fireLocation.rotation;

        var bulletScript = bullet.GetComponent<AutoMoveForward>();
        bulletScript.SetSpeed(bulletSpeed);
        bullet.SetActive(true);

        bulletFireFX.Play(true);
		yield return new WaitForSeconds (fireSpeed);
		IsFiring = false;
	}
}