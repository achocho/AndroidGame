
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
 
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 20f;

    [SerializeField] private Transform levelPart_Start;
    float timer = 0;
    public Transform[] Prefabs;
    private List<Transform> levelPartList=new List<Transform>();
    [SerializeField] private PlayerController player;

    private Vector3 lastEndPosition;

    private void Awake()
    {
        lastEndPosition = levelPart_Start.Find("EndPosition").position;

        int startingSpawnLevelParts = 5;
        for (int i = 0; i < startingSpawnLevelParts; i++)
        {
            SpawnLevelPart();
        }
    }

    private void Update()
    {
        timer+=1*Time.deltaTime;
        if (Vector3.Distance(player.GetPosition(), lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            // Spawn another level part
            SpawnLevelPart();
            if (timer>10) {
                DeletePlatform();
                timer = 0;
            } }
    }

    private void SpawnLevelPart()
    {
        Transform chosenLevelPart = Prefabs[Random.Range(0, Prefabs.Length)];
        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
            lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
         }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        levelPartList.Add(levelPartTransform);
        return levelPartTransform;
    }
    public void DeletePlatform()
    {
        levelPartList.RemoveAt(0);
        Destroy(levelPartList[0].gameObject);
    }
}