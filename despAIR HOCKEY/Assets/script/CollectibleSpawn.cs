using UnityEngine;
using Random = UnityEngine.Random;

public class CollectibleSpawn : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject collectible;

    [SerializeField] private float spawnInterval;
    [SerializeField] private float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;

        if (spawnInterval < timer)
        {
            SpawnCollectible();
            timer = 0;
        }
    }

    void SpawnCollectible()
    {
        int spawnPoint = Random.Range(0, spawnPoints.Length);

        Instantiate(collectible, spawnPoints[spawnPoint].position, Quaternion.identity,transform);
    }

    public void Reset()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Collectible"))
                Destroy(child.gameObject);
        }

        timer = 0;
    }
}
