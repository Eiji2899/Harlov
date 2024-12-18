using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public float maxThrust;
    public float maxTorque;
    public Rigidbody2D rb;
    public float screenTop;
    public float screenBottom;
    public float screenLeft;
    public float screenRight;
    public int asteroidSize;   //3=large, 2=medium, 1=small
    public GameObject asteroidMedium;
    public GameObject asteroidSmall;
    public int points;
    public GameObject player;
    public GameObject explosion;

    public GameMenager gm;


    void Start()
    {
     // Add a random amount of torque and thrust to the asteroid
     Vector2 thrust = new Vector2(Random.Range(-maxThrust,maxThrust),Random.Range(-maxThrust,maxThrust));
     float torque = Random.Range (-maxTorque, maxTorque);

     rb.AddForce (thrust);
     rb.AddTorque (torque);

     player=GameObject.FindWithTag("Player");

        //find the game menager
        gm = GameObject.FindObjectOfType<GameMenager>();

    }

    // Update is called once per frame
    void Update()
    {
        // Screen Wraping

        Vector2 newPos = transform.position;
        if (transform.position.y > screenTop)
        {
            newPos.y = screenBottom;
        }

        if (transform.position.y < screenBottom)
        {
            newPos.y = screenTop;
        }

        if (transform.position.x > screenRight)
        {
            newPos.x = screenLeft;
        }

        if (transform.position.x < screenLeft)
        {
            newPos.x = screenRight;
        }

        transform.position = newPos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bullet"))
        {
            Destroy(other.gameObject);
            if (asteroidSize == 3)
            {
                Instantiate(asteroidMedium,transform.position, transform.rotation);
                Instantiate(asteroidMedium, transform.position, transform.rotation);

                gm.UpdateNumberOfAsteroids(1);

                
            }
            else if (asteroidSize == 2)
            {
                Instantiate(asteroidSmall, transform.position, transform.rotation);
                Instantiate(asteroidSmall, transform.position, transform.rotation);

                gm.UpdateNumberOfAsteroids(1);
            }
            else if (asteroidSize == 1)
            {
                gm.UpdateNumberOfAsteroids(-1);
            }
            player.SendMessage("ScorePoints",points);
            GameObject newExplosion=Instantiate(explosion,transform.position, transform.rotation);
            Destroy (newExplosion,3f);

            Destroy(gameObject);


        }
        
    }

}
