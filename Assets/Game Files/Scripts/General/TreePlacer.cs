

using Sirenix.OdinInspector;
using UnityEngine;

public class TreePlacer : MonoBehaviour
{
    public Terrain terrain;          // Assign your Terrain here
    public BoxCollider area;         // Box area to scatter in
    public GameObject[] prefabs;     // Prefabs to place

    public int count = 20;           // How many objects to place
    public LayerMask terrainLayer;   // Set to Terrain layer (or Default if terrain is on that)

    public bool alignToNormal = false;
    public bool randomYRotation = true;


    [Button]
    public void PlacePrefabs()
    {
        for (int i = 0; i < count; i++)
            SpawnOne();
    }


    void SpawnOne()
    {
        Bounds b = area.bounds;

        // Pick a random XZ inside the box
        float x = Random.Range(b.min.x, b.max.x);
        float z = Random.Range(b.min.z, b.max.z);

        // Start raycast from above the box
        Vector3 rayStart = new Vector3(x, b.max.y + 50f, z);

        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, 1000f, terrainLayer))
        {
            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

            Quaternion rot = Quaternion.identity;

            if (alignToNormal)
            {
                rot = Quaternion.FromToRotation(Vector3.up, hit.normal);
                if (randomYRotation)
                    rot *= Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            }
            else if (randomYRotation)
            {
                rot = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            }

            Instantiate(prefab, hit.point, rot, transform);
        }
    }
}