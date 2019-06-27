using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimplePoolManager : MonoBehaviour {
    public static SimplePoolManager instance;

    [SerializeField] private PoolCollection[] collection;

    [Serializable]
    public struct PoolCollection {
        public string _Name;
        public bool _ActiveFirstItem;
        public int InstantiateQty;
        [Tooltip ("Parent transform is not required")]
        public Transform ParentTransform;
        public GameObject[] items;
    }

    private Dictionary<string, List<GameObject>> _instantiatedList = new Dictionary<string, List<GameObject>> ();
    private List<GameObject> _collectionItemsAvailable;
    int index = 0;

    //****************************
    // Initiations
    //****************************
    void Awake () {
        Singleton ();
        InstantateGameObjects ();
    }

    void InstantateGameObjects () {
        var newlist = new List<GameObject> ();
        foreach (PoolCollection poolCollection in collection) {
            foreach (GameObject poolItem in poolCollection.items) {
                for (int i = 0; i < poolCollection.InstantiateQty; i++) {
                    GameObject instantiatedGo = poolCollection.ParentTransform != null ?
                        Instantiate (poolItem, transform.position, Quaternion.identity, poolCollection.ParentTransform) :
                        Instantiate (poolItem, transform.position, Quaternion.identity);

                    if (poolCollection._ActiveFirstItem && i <= 0) {
                        instantiatedGo.SetActive (true);
                    } else {
                        instantiatedGo.SetActive (false);
                    }

                    newlist.Add (instantiatedGo);
                }
            }

            AddToPoolList (poolCollection._Name, newlist);
            newlist.Clear ();
        }
    }

    //****************************
    // Get Actions
    //****************************
    public GameObject GetNextAvailablePoolItem (string nameOfPool) {
        if (!DoesPoolExist (nameOfPool)) {
            return null;
        }

        GameObject go = null;
        foreach (var list in _instantiatedList) {
            if (list.Key == nameOfPool) {
                go = list.Value.FirstOrDefault (x => !x.activeSelf);
                if (go == null) {
                    AddToCurrentPoolList (nameOfPool);
                    return GetNextAvailablePoolItem (nameOfPool);
                }
                go.SetActive (true);
                break;
            }
        }
        return go;
    }

    public GameObject GetRandomPoolItem (string nameOfPool) {
        if (!DoesPoolExist (nameOfPool)) {
            return null;
        }

        _collectionItemsAvailable = new List<GameObject> (_instantiatedList[nameOfPool].Where (itm => !itm.activeSelf));

        if (_collectionItemsAvailable.Count <= 0 && index < 2) {
            AddToCurrentPoolList (nameOfPool);
            _collectionItemsAvailable.Clear ();
            _collectionItemsAvailable = new List<GameObject> (_instantiatedList[nameOfPool].Where (itm => !itm.activeSelf));
        }

        GameObject go = _collectionItemsAvailable[UnityEngine.Random.Range (0, _collectionItemsAvailable.Count)];
        go.SetActive (true);
        _collectionItemsAvailable.Clear ();
        return go;
    }

    public List<GameObject> GetAllActiveCategoryItem(string nameOfPool)
    {
        if (!DoesPoolExist(nameOfPool))
        {
            return null;
        }

        return _instantiatedList[nameOfPool]; 
    }

    public List<GameObject> GetAllItemInACategory(string nameOfPool)
    {
        if (!DoesPoolExist(nameOfPool))
        {
            return null;
        }

        var activeItems = new List<GameObject>();
        var currentPool = _instantiatedList[nameOfPool];

        foreach (var item in currentPool)
        {
            if (item.activeSelf)
            {
                activeItems.Add(item);
            }
        }

        return activeItems;
    }

    bool DoesPoolExist (string nameOfPool) {
        if (!_instantiatedList.ContainsKey (nameOfPool)) {
            Debug.Log ("The specified Pool does not exist: " + nameOfPool);
            Debug.Break ();
            return false;
        }

        return true;
    }
    //****************************
    // Add to pool Actions
    //****************************
    void AddToPoolList (string name, List<GameObject> list) {
        _instantiatedList.Add (name, new List<GameObject> (list));
    }

    void AddToCurrentPoolList (string nameOfPool) {
        Debug.Log ("Added extra items to : " + nameOfPool + ", considder increasing the Qty to create in the pool");
        index++;

        var currentPoolDetails = collection.FirstOrDefault (x => x._Name == nameOfPool);
        int currentPoolQty = currentPoolDetails.InstantiateQty;

        for (int i = 0; i < currentPoolQty; i++) {
            foreach (var item in currentPoolDetails.items) {
                GameObject instantiatedGo = currentPoolDetails.ParentTransform != null ?
                    Instantiate (item, transform.position, Quaternion.identity, currentPoolDetails.ParentTransform) :
                    Instantiate (item, transform.position, Quaternion.identity);
                instantiatedGo.SetActive (false);
                _instantiatedList[nameOfPool].Add (instantiatedGo);
            }
        }

    }

    //****************************
    // Disable Actions
    //****************************
    public void DisablePoolObject (string nameOfPool, Transform poolObjectTransform) {
        foreach (var list in _instantiatedList) {
            if (list.Key == nameOfPool) {
                try {
                    list.Value.FirstOrDefault (x => x.transform == poolObjectTransform).SetActive (false);
                } catch (Exception) {
                    Debug.Log ("Incorrect Transform being sent to DisablePoolObject: sent Transform" + poolObjectTransform.name + ". Need transform specifiec to " + list.Key);
                    Debug.Break ();
                }
            }
        }
    }

    public void DisablePoolObject (Transform poolObjectTransform) {
        foreach (var list in _instantiatedList) {
            var itemFound = list.Value.FirstOrDefault (x => x.transform == poolObjectTransform);
            if (itemFound) {
                try {
                    itemFound.SetActive (false);
                } catch (Exception) {
                    Debug.Log ("Incorrect Transform being sent to DisablePoolObject: sent Transform" + poolObjectTransform.name + ". Need transform specifiec to " + list.Key);
                    Debug.Break ();
                }
            }
        }
    }

    public void DisableAllPoolObject () {
        foreach (var list in _instantiatedList) {
            try {
                list.Value.FindAll (x => x.activeSelf).ForEach (x => x.SetActive (false));
            } catch (Exception ex) {
                Debug.Log ("Error: " + ex.Message);
                Debug.Break ();
            }
        }
    }

    public void DespawnAllPoolObjectByCategory (string name) {
        if (!DoesPoolExist (name)) {
            return;
        }

        List<GameObject> collectionList = _instantiatedList.FirstOrDefault (x => x.Key == name).Value;
        collectionList.ForEach (x => x.gameObject.SetActive (false));
    }

    void Singleton () {
        if (instance == null || instance != this) {
            instance = this;
        } else if (instance == this) {
            Destroy (gameObject);
        }
    }
}