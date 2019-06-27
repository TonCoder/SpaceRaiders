using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCollectibleSpawner : MonoBehaviour {
    [SerializeField] private Transform ObjectToSpawnCollectible;
    private List<Transform> ActiveCollectibles = new List<Transform> ();
    private bool instantiating = false;

    private void OnDisable () {
        instantiating = false;
        if (ActiveCollectibles.Count <= 0) return;
        ActiveCollectibles.ForEach (x => SimplePoolManager.instance.DisablePoolObject (x.transform));
        ActiveCollectibles.Clear ();
    }

    // Use this for initialization
    void Update () {
        if (!instantiating)
            Initialize ();
    }

    void Initialize () {
        instantiating = true;
        if (ObjectToSpawnCollectible.childCount > 0) {
            foreach (Transform child in ObjectToSpawnCollectible) {
                SpawnCollectible (child.GetComponent<Renderer> ().bounds.center);
            }
        } else {
            SpawnCollectible (ObjectToSpawnCollectible.GetComponent<Renderer> ().bounds.center);
        }
    }

    private void SpawnCollectible (Vector3 center) {
        var Obj = SimplePoolManager.instance.GetNextAvailablePoolItem ("Collectibles");
        Obj.transform.position = center;
        Obj.SetActive (true);
        ActiveCollectibles.Add (Obj.transform);
    }

}