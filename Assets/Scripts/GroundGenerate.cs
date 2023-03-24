using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerate : MonoBehaviour
{
    public GameObject[] groundPrefabs;
    private List<GameObject> activeGround = new List<GameObject>();
    private float spawnPos = 0;
    private float tileLength = 100;

    [SerializeField] private Transform player;
    private int startTiles = 6;

    void Start()
    {
        for (int i = 0; i < startTiles; i++)
        {
            SpawnGround(Random.Range(0, groundPrefabs.Length));
        }
    }

  void Update()
    {
        if(player.position.z - 60  > spawnPos - (startTiles * tileLength))
        {
            SpawnGround(Random.Range(0, groundPrefabs.Length));
            DeleteGround();
        }
    }

  private void SpawnGround(int tileIndex)
  {
      GameObject nextGround = Instantiate(groundPrefabs[tileIndex], transform.forward * spawnPos, transform.rotation);
      activeGround.Add(nextGround);
      spawnPos += tileLength;
  }

  private void DeleteGround()
  {
      Destroy(activeGround[0]);
      activeGround.RemoveAt(0);
  }
}
