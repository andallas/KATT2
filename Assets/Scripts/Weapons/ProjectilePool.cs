using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectilePool : MonoBehaviour
{
    public int poolSize;
    public int poolMax = 0;
    public GameObject goPrefab;
    public ProjectilePool Instance { get { return _instance; } }

    private List<GameObject> pool;
    private ProjectilePool _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        poolMax = poolSize > poolMax ? poolSize : poolMax;
    }

    void Start()
    {
        pool = new List<GameObject>();

        // Create initial pool of objects
        AddToPool(poolSize);
    }

    public GameObject Create(GameObject prefab)
    {
        return Create(prefab, Vector3.zero, Quaternion.identity);
    }

    public GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject go = GetFromPool(prefab);

        Transform transform = go.transform;
        transform.localPosition = position;
        transform.localRotation = rotation;
        go.SetActive(true);

        return go;
    }

    public void Recycle(GameObject go)
    {
        if(AddToPool(go) == null)
        {
            Destroy(go);
            return;
        }
        go.transform.parent = null;
        go.SetActive(false);
    }

    public void Recycle(GameObject go, float seconds)
    {
        StartCoroutine(RecycleObject(go, seconds));
    }

    private void AddToPool(int count)
    {
        for (int i = 0; i < count; i++ )
        {
            // If poolMax is set to zero or a negative number, then there is no pool limit
            if (poolMax > 0 && pool.Count + 1 > poolMax)
            {
                Debug.LogError("ERROR: Maximum pool size for ProjectilePool reached - " + poolMax + ".");
                break;
            }

            GameObject go = (GameObject)Instantiate(goPrefab);
            go.SetActive(false);
            pool.Add(go);
        }
    }

    private GameObject AddToPool(GameObject prefab)
    {
        // If poolMax is set to zero or a negative number, then there is no pool limit
        if (poolMax > 0 && pool.Count + 1 > poolMax)
        {
            Debug.LogError("ERROR: Maximum pool size for ProjectilePool reached - " + poolMax + ".");
            return null;
        }

        GameObject go = (GameObject)Instantiate(prefab);
        go.SetActive(false);
        pool.Add(go);
        return go;
    }

    IEnumerator RecycleObject(GameObject go, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Recycle(go);
    }

    private GameObject GetFromPool(GameObject prefab)
    {
        GameObject go = null;
        if(pool.Count > 0)
        {
            go = pool[0];
            pool.RemoveAt(0);
        }
        else
        {
            go = AddToPool(prefab);
        }

        return go;
    }
}