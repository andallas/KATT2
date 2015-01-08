using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveGenerator : MonoBehaviour
{
    public Transform spawnParent;
    public List<Wave> waves;

    private bool keepSpawning = true;
    private int spawnCount = 0;
    private Vector2 screenSize;
    private bool waveReady = false;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    void Update()
    {
        screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    IEnumerator SpawnLoop()
    {
        while (keepSpawning)
        {
            foreach (Wave wave in waves)
            {
                foreach (WaveAction waveAction in wave.waveActions)
                {
                    if(!waveReady)
                    {
                        yield return new WaitForSeconds(wave.delay);
                        waveReady = true;
                        if (wave.message != "")
                        {
                            Debug.Log(wave.message);
                        }
                    }
                    
                    if (waveAction.delay > 0)
                    {
                        yield return new WaitForSeconds(waveAction.delay);
                    }

                    if (waveAction.message != "")
                    {
                        Debug.Log(waveAction.message);
                    }

                    if (waveAction.enemyPrefab != null && waveAction.numToSpawn > 0)
                    {
                        for (spawnCount = 0; spawnCount < waveAction.numToSpawn; spawnCount++)
                        {
                            GameObject enemy = (GameObject)Instantiate(waveAction.enemyPrefab);
                            enemy.transform.SetParent(spawnParent);

                            Vector2 rand = new Vector2(Random.Range(0f, screenSize.x), Random.Range(-screenSize.y, screenSize.y));
                            Vector3 pos = new Vector3(transform.position.x + rand.x, transform.position.y + rand.y, transform.position.z);
                            enemy.transform.position = pos;
                        }
                    }
                    waveReady = false;
                }
                yield return null;
            }
            keepSpawning = false;
            yield return null;
        }
    }
}