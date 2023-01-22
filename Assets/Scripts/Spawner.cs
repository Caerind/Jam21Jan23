using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject manPrefab;
    public int spawnCountMan = 7;
    private int spawnedMan = 0;

    public GameObject womanPrefab;
    public int spawnCountWoman = 7;
    private int spawnedWoman = 0;

    public float spawnDelay = 1.5f;
    private float timer = 0.0f;

    private GameObject[] men = null;
    private GameObject[] women = null;

    private void Start()
    {
        men = new GameObject[spawnCountMan];
        for (int i = 0; i < spawnCountMan; ++i)
        {
            men[i] = GameObject.Instantiate(manPrefab, transform.position, Quaternion.identity);
            men[i].SetActive(false);
            men[i].transform.SetParent(transform);
        }

        women = new GameObject[spawnCountWoman];
        for (int i = 0; i < spawnCountWoman; ++i)
        {
            women[i] = GameObject.Instantiate(womanPrefab, transform.position, Quaternion.identity);
            women[i].SetActive(false);
            women[i].transform.SetParent(transform);
        }
    }

    private void Update()
    {
        bool canSpawnMan = spawnedMan < spawnCountMan;
        bool canSpawnWoman = spawnedWoman < spawnCountWoman;

        if ((manPrefab != null || womanPrefab != null) && (canSpawnMan || canSpawnWoman))
        {
            timer += Time.deltaTime;
            if (timer > spawnDelay)
            {
                timer = 0.0f;

                if (canSpawnMan && canSpawnWoman)
                {
                    if (Random.Range(0.0f, 100.0f) < 50.0f)
                    {
                        SpawnMan();
                    }
                    else
                    {
                        SpawnWoman();
                    }
                }
                else if (canSpawnMan)
                {
                    SpawnMan();
                }
                else if (canSpawnWoman)
                {
                    SpawnWoman();
                }
            }
        }
    }

    private void SpawnMan()
    {
        men[spawnedMan].SetActive(true);
        GameManager.Instance.peons.Add(men[spawnedMan]);
        spawnedMan++;
    }

    private void SpawnWoman()
    {
        women[spawnedWoman].SetActive(true);
        GameManager.Instance.peons.Add(women[spawnedWoman]);
        spawnedWoman++;
    }
}
