using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeSpawner : MonoBehaviour
{
    [SerializeField] GameObject CubePreb;

    float accumTime = 0f;
    List<GameObject> LiveCubes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnWithTimeIntervalIfNecessary();
        updateCubesWithTimeInterval();
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
        // set cube and position if supplied
        // newCube.transform.localPosition = 
        // newCub.transform.localRotation = 

        // 
        LiveCubes.Add(newCube); 
    }

    void updateCubesWithTimeInterval() {
        const float cubeSpeed = 1f; // units per second
        Vector3 updateVector = new Vector3(0f, 0f, -Time.deltaTime * cubeSpeed);

        foreach (GameObject cube in LiveCubes) {
            cube.transform.position += updateVector;
        }
    }
}
