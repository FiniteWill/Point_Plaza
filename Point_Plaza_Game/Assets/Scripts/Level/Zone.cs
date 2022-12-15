using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZoneType { LEFT_SIDE, RIGHT_SIDE, MIDDLE };

public class Zone : MonoBehaviour
{
    private ZoneType zoneType;
    private List<GameObject> zoneObjects;
    private int zoneID = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
