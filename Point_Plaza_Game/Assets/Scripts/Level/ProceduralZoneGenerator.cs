using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralZoneGenerator : MonoBehaviour
{
    private List<GameObject> leftZonePrefabs;
    private List<GameObject> midZonePrefabs;
    private List<GameObject> rightZonePrefabs;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 somePos = new Vector3(Random.Range(0, 100), Random.Range(0, 200), 0);
        var newPref = leftZonePrefabs[Random.Range(0, leftZonePrefabs.Count - 1)];
        //if(somePos.x + newPref.transform.)
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 
    /// Total Zones = <paramref name="numLayers"/>*<paramref name="zoneDepth"/>
    /// </summary>
    /// <param name="zoneDepth">How many Zones are inside of a layer.</param>
    /// <param name="numLayers">How many layers will be generated.</param>
    private void GenerateZones(int zoneDepth, int numLayers, ZoneType zoneType)
    {
        // Generate a layer
        GameObject temp_zone;
        switch(zoneType)
        {
            case ZoneType.LEFT_SIDE:
                temp_zone = Instantiate(leftZonePrefabs[Random.Range(0, leftZonePrefabs.Count)], new Vector3(), Quaternion.identity);
                break;
            case ZoneType.RIGHT_SIDE:
                temp_zone = Instantiate(rightZonePrefabs[Random.Range(0, rightZonePrefabs.Count)], new Vector3(), Quaternion.identity);
                break;
            case ZoneType.MIDDLE:
                temp_zone = Instantiate(midZonePrefabs[Random.Range(0, rightZonePrefabs.Count)], new Vector3(), Quaternion.identity);
                break;
        }
        
    }
}
