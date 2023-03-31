using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] tilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 30;
    public int numOfTiles = 5;
    public Transform playerTransform;
    private List<GameObject> activeTile = new List<GameObject>();

    void Start()
    {
        for(int i = 0; i < numOfTiles; i++)
        {
            if (i == 0)
            {
                spawnTile(i);
                continue;
            }
            spawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - 35 > zSpawn - (numOfTiles * tileLength))
        {
            spawnTile(Random.Range(0, tilePrefabs.Length));
            deleteTile();
        }
    }
    
    public void spawnTile(int tileIndex)
    {
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward*zSpawn, transform.rotation);
        activeTile.Add(go);
        zSpawn += tileLength;
    }

    private void deleteTile()
    {
        if(activeTile.Count > 0)
        {
            GameObject go = activeTile.ElementAt(0);
            activeTile.RemoveAt(0);
            Destroy(go);
        }
    }
}
