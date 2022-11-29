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

    private List<GameObject> spawnedObjects = new List<GameObject>();
    private List<GameObject> spawnedPlatforms = new List<GameObject>();
   

    // How many platforms get loaded at the start
    private const int NUM_START_PLATFORMS = 20;
    // How many platforms to spawn once the player reaches the next max spawning distance
    private const int NUM_TO_SPAWN = NUM_START_PLATFORMS;
    private PlatformerPlayer player = null;
    private PlatformerPlayer_Movement playerMovement = null;
    // The value (x for horz, y for vert spawning) that the player was at when platforms were last spawned
    // Used to determine how far the player has to go before next set of platforms spawn
    private float lastSpawningPoint = 0;
    
    private void Awake()
    {
        Assert.IsNotNull(platformPrefab);
        Assert.IsNotNull(specialPlatformPrefabs);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlatformerPlayer>();
        playerMovement = player.GetComponentInChildren<PlatformerPlayer_Movement>();
        Assert.IsNotNull(player);
        Assert.IsNotNull(playerMovement);
        Assert.IsNotNull(platformPrefab);
        Assert.IsNotNull(enemyPrefabs);
        Assert.IsNotNull(itemPrefabs);

        // Spawn x amount of platforms
        SpawnPlatforms(NUM_START_PLATFORMS, lastSpawningPoint, verticalSpawning);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the current player position - the last position platforms were spawned at is >= 
        // the amount of distance the player has to reach before spawning a new batch of platforms
        if(verticalSpawning && playerMovement.transform.position.y - lastSpawningPoint > maxSpawningDist)
        {
            SpawnPlatforms(NUM_TO_SPAWN, lastSpawningPoint, verticalSpawning);
        }
        else if(!verticalSpawning && playerMovement.transform.position.x - lastSpawningPoint> maxSpawningDist)
        {
            SpawnPlatforms(NUM_TO_SPAWN, lastSpawningPoint, verticalSpawning);
        }
    }


    private void SpawnPlatforms(int numToSpawn, float curPos, bool isVertical)
    {
        for (int i = 0; i < numToSpawn; ++i)
        {
            if (Random.Range(0, 100) < specialPlatformChance)
            {
                var newPlatform = Instantiate(specialPlatformPrefabs[Random.Range(0, specialPlatformPrefabs.Count - 1)]);
                Assert.IsNotNull(newPlatform);
                spawnedObjects.Add(newPlatform);
                spawnedPlatforms.Add(newPlatform);
            }
            else
            {
                var newPlatform = Instantiate(platformPrefab);
                spawnedObjects.Add(newPlatform);
                spawnedPlatforms.Add(newPlatform);
            }
        }
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
