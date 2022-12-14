using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float width;
    private float horzDist;
    public GameObject cam;
    public float parallaxFactor;

    // Start is called before the first frame update
    void Start()
    {
        horzDist = transform.position.x;
        horzDist = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixdUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxFactor));
        float dist = (cam.transform.position.x * parallaxFactor);

        transform.position = new Vector3(horzDist + dist, transform.position.y, transform.position.z);

        if (temp > horzDist + width) horzDist += width;
        else if (temp < horzDist - width) horzDist -= width;
    }
}
