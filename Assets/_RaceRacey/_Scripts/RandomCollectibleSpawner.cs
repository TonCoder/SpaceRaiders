using System.Collections.Generic;
using UnityEngine;

public class RandomCollectibleSpawner : MonoBehaviour {
    [SerializeField] private bool UseThisObjectToSpawnInto;
    [SerializeField] private string PoolObjectName;
    private List<Transform> ActiveCollectibles = new List<Transform> ();

    private void Start() {
        Initialize();
    }

    private void OnDisable () {
        // instantiating = false;
        // if (ActiveCollectibles.Count <= 0) return;
        // ActiveCollectibles.ForEach (x => SimplePoolManager.instance.DisablePoolObject (x.transform));
        // ActiveCollectibles.Clear ();
    }

    void Initialize () {
        float leftMost = transform.TransformPoint(Vector3.left / 2).z;
        float rightMost = transform.TransformPoint(Vector3.right / 2).z;
        Vector3 randomPos = new Vector3(transform.position.x, 0, Random.Range(leftMost, rightMost));
        SpawnObj (randomPos);
    }

    private void SpawnObj (Vector3 pos) {
        var Obj = SimplePoolManager.instance.GetNextAvailablePoolItem (PoolObjectName);
        // Obj.transform.parent = this.transform;
        Obj.transform.position = pos;
        Obj.transform.localRotation = Quaternion.identity;
        // Obj.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        Obj.SetActive (true);
        ActiveCollectibles.Add (Obj.transform);
    }

}