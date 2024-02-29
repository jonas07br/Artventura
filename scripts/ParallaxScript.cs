using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    
    public float parallaxValue = 1;
    private Camera cam;
    Vector3 startPosition;

    float startZ;

    Vector3 travel => (Vector3)cam.transform.position - startPosition;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        startPosition = transform.position;
        startZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(startPosition.x + travel.x * parallaxValue, cam.transform.position.y, transform.position.z);
    }
}
