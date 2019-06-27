using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMoveForward : MonoBehaviour {


    [SerializeField] private float speed = 10;
    [SerializeField] private float despawnTimer = 2;

    void Start()
    {
        StartCoroutine(DespawnObject());
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }

    // Update is called once per frame
    void Update()
    {
        var moveVelocity = speed * Time.deltaTime * transform.forward;
        transform.position += moveVelocity;
    }

    IEnumerator DespawnObject()
    {
        yield return new WaitForSeconds(despawnTimer);
        //SimplePoolManager.instance.DisablePoolObject (transform);
        Destroy(gameObject);
    }
}