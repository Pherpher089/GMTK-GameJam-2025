using System.Collections.Generic;
using UnityEngine;

public class TrailPointManager : MonoBehaviour
{
    public GameObject trailColliderPrefab; // Small sphere or capsule with Trigger and appropriate layer/tag
    public float pointSpacing = 0.5f;
    public float lifetime = 15f; // How long each point persists

    private Vector3 lastPoint;
    private List<GameObject> spawnedPoints = new List<GameObject>();

    void Start()
    {
        lastPoint = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, lastPoint) >= pointSpacing)
        {
            GameObject point = Instantiate(trailColliderPrefab, transform.position - (transform.forward * .5f), Quaternion.identity);
            Destroy(point, lifetime);
            spawnedPoints.Add(point);
            lastPoint = transform.position;
        }
    }
}
