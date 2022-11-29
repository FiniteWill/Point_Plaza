using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ProceduralPlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab = null;
    [SerializeField] private List<GameObject> specialPlatformPrefabs = null;
    [SerializeField] private List<GameObject> enemyPrefabs = null;
    [SerializeField] private List<GameObject> itemPrefabs = null;
    [SerializeField] private bool verticalSpawning = true;
    [Tooltip("The max horizontal distance a new platform can be spawned from the player/last platform.")]
    [SerializeField] private float horizontalMaxDist = 5.0f;
    [Tooltip("The max vertical distance a new platform can be spawned from the player/last platform.")]
    [SerializeField] private float verticalMaxDist = 5.0f;
    [Tooltip("The percent chance that an enemy will spawn on the next platform.")]
    [SerializeField] private float enemySpawnChance = 2f;
    [Tooltip("The percent chance that an item will spawn on the next platform")]
    [SerializeField] private float itemSpawnChance = 0.5f;
    [Tooltip("The percent chance that the next platform will be a special type.")]
    [SerializeField] private float specialPlatformChance = 5f;
    [Tooltip("The speed at which the lava rises for lava climb.")]
    [SerializeField] private float lavaRisingSpeed = 2f;
    [Tooltip("How close the player can get to the last platform before another one spawns.")]
    [SerializeField] private float maxSpawningDist = 20f;
    private float curHorzPlatformDist = 2f;
    private float curVertPlatformDist = 2f;
    private List<GameObject> spawnedObjects;
   
    

    // How many platforms get loaded at the start
    private const int NUM_START_PLATFORMS = 20;
    private void Awake()
    {
        Assert.IsNotNull(platformPrefab);
        Assert.IsNotNull(specialPlatformPrefabs);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Spawn x amount of platforms
        for(int i=0; i<NUM_START_PLATFORMS; ++i)
        {
            if(Random.Range(0,100) < specialPlatformChance)
            {
                var newPlatform = Instantiate(specialPlatformPrefabs[Random.Range(0, specialPlatformPrefabs.Count - 1)]);
            }
            else
            {
                var newPlatform = Instantiate(platformPrefab);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject GetSpecialPlatform(List<GameObject> platforms)
    {
        return platforms[Random.Range(0, platforms.Count - 1)];
    }

    private GameObject GetSpecialPlatform(List<GameObject> platforms, int index)
    {
        Mathf.Clamp(index, 0, platforms.Count - 1);
        return platforms[index];
    }
}
