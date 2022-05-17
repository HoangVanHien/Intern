using System.Collections.Generic;
using UnityEngine;

public class GameObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;//input

    public Dictionary<string, Queue<GameObject>> poolDictionary;//object will be saved heare

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectsPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject gameObject = Instantiate(pool.prefab, transform);
                gameObject.SetActive(false);
                gameObject.name += i;
                objectsPool.Enqueue(gameObject);
            }
            poolDictionary.Add(pool.tag, objectsPool);
        }
    }

    public bool IsPoolContainTag(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool doesnt contain tag " + tag);
            return false;
        }
        return true;
    }

    public GameObject GetFromPool(string tag)
    {
        if (!IsPoolContainTag(tag)) return null;
        if (poolDictionary[tag].Count <= 0)
        {
            Debug.LogWarning(tag + " queue empty");
            return null;
        }
        GameObject spawnObject = poolDictionary[tag].Dequeue();
        spawnObject.SetActive(true);

        return spawnObject;

    }

    public void ReturnToPool(string tag, GameObject returnObject)
    {
        if (!IsPoolContainTag(tag)) return;
        returnObject.SetActive(false);
        poolDictionary[tag].Enqueue(returnObject);
        Debug.Log("Queue size: " + poolDictionary[tag].Count);
    }
}
