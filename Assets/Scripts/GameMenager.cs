using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameMenager : MonoBehaviour{

    public int numberOfAsteroids; //this is the current numb of ast in scene
    public int levelNumber = 1;
    public GameObject asteroid;
    public void UpdateNumberOfAsteroids(int change)
    {
        numberOfAsteroids += change;

        if(numberOfAsteroids <= 0) {

            Invoke("StartNewLevel", 3f);
        }
    }

    void StartNewLevel()
    {
        levelNumber++;

        for (int i = 0; i < levelNumber*2; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-15.7f,15.7f),12f);
            Instantiate(asteroid, spawnPosition, Quaternion.identity);
            numberOfAsteroids++;
        }
    }
}
