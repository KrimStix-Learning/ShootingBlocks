using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject fallingBlockPrefab;
    public Vector2 secondsBetweenSpawnsMinMax;
    float nextSpawnTime;

    public Vector2 spawnSizeMinMax;
    public float spawnAngleMax;

    Vector2 screenHalfSizeWorldUnits;
    // Start is called before the first frame update
    void Start()
    {
        screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);    
    }

    // Update is called once per frame
    void Update()
    {
        //spawn a block
        if (Time.time > nextSpawnTime)
        {
            float secondsBetweenSpawns = Mathf.Lerp(secondsBetweenSpawnsMinMax.y, secondsBetweenSpawnsMinMax.x, Difficulty.GetDifficultyPercent());
            nextSpawnTime = Time.time + secondsBetweenSpawns;

            //randomize the angle of the block
            float spawnAngle = Random.Range(-spawnAngleMax, spawnAngleMax);
            //randomize the size of the block
            float spawnSize = Random.Range(spawnSizeMinMax.x, spawnSizeMinMax.y);
            //randomize the position of the block
            Vector2 spawnPosition = new Vector2(Random.Range(-screenHalfSizeWorldUnits.x, screenHalfSizeWorldUnits.x), screenHalfSizeWorldUnits.y + spawnSize);
            //create the block
            GameObject newBlock = (GameObject)Instantiate(fallingBlockPrefab, spawnPosition, Quaternion.Euler(Vector3.forward * spawnAngle));
            //set the size of the block
            newBlock.transform.localScale = Vector2.one * spawnSize;
        }
        
    }
}
