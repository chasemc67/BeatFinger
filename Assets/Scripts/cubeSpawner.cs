using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeSpawner : MonoBehaviour
{
    [SerializeField] GameObject CubePreb;

    float accumTime = 0f;
    public float spawnWidth = 0.5f;
    List<GameObject> LiveCubes = new List<GameObject>();

    public GameScoreManager gameScoreManager;

    // Start is called before the first frame update
    void Start()
    {
        gameScoreManager = GameObject.Find("GameManager").GetComponent<GameScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnWithTimeIntervalIfNecessary();
    }

    // this is a great place for using a delegate, break up this code and make it more extensible
    void spawnWithTimeIntervalIfNecessary() {
        const float spawnRate = 1f;  

        accumTime += Time.deltaTime;
        if (accumTime >= spawnRate) {
            accumTime -= spawnRate;
            SpawnCube();
        }
    }

    void SpawnCube() {
        GameObject newCube = Instantiate(CubePreb, this.transform, false);
        cubeInteraction cubeScript = newCube.GetComponent<cubeInteraction>();
        cubeScript.isLeftCube = Random.value > 0.5;
        cubeScript.cubeHit = gameScoreManager.HandleIncrementScore;
        cubeScript.cubeMiss = gameScoreManager.HandleIncrementMiss;

        Transform cubPos = newCube.GetComponent<Transform>();
        cubPos.localPosition += new Vector3((float)(Random.value * spawnWidth - (spawnWidth / 2)), 0f, 0f);
    }
}
