using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class SpaceshipControls : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Collider2D collider;
    public float thrust;
    public float turnThrust;
    private float thrustInput;
    private float turnInput;
    public float screenTop;
    public float screenBottom;
    public float screenLeft;
    public float screenRight;
    public float bulletForce;
    public float deathForce;
    private bool hyperspace;

    public GameObject bullet;

    public int score;
    public int lives;
    public TMP_Text scoreText;
    public TMP_Text livesText;

    public AudioSource audio;
    public GameObject explosion;

    public Color inColor;
    public Color normalColor;
    // Initiallization
    void Start()
    {
        score = 0;
        hyperspace = false;
        lives = 3;
        scoreText.text = "Score " + score;
        livesText.text = "Lives " + lives;

    }

    // Update is called once per frame
    void Update()
    {
        // Check for input from keyboard
        thrustInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        // Check for input from the fire key and make bullets
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
            newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * bulletForce);
            Destroy(newBullet, 5.0f);
        }

        if (Input.GetButtonDown("Hyperspace") && !hyperspace)
        {
            hyperspace = true;

            spriteRenderer.enabled = false;
            collider.enabled = false;
            Invoke("Hyperspace", 1f);
        }

        // Rotate the ship
        transform.Rotate(Vector3.forward * turnInput * Time.deltaTime * -turnThrust);

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

    void FixedUpdate()
    {
        rb.AddRelativeForce(Vector2.up * thrustInput);
        //rb.AddTorque (-turnInput);
    }

    void ScorePoints(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = "Score " + score;

    }

    void Respawn()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = Vector2.zero;

       
        spriteRenderer.enabled = true;
        spriteRenderer.color = inColor;
        Invoke("Invulnerable", 3f);


    }

    void Invulnerable()
    {
        collider.enabled = true;
        spriteRenderer.color = normalColor;

    }

    void Hyperspace()
    {
        Vector2 newPosition = new Vector2(Random.Range(-17f, 17f), Random.Range(-9f, 9f));
        transform.position = newPosition;


        spriteRenderer.enabled = true;
        collider.enabled = true;

        hyperspace = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.relativeVelocity.magnitude);

        if (col.relativeVelocity.magnitude > deathForce)
        {
            lives--;
            //Make explosion
            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(newExplosion, 3f);
            livesText.text = "Lives " + lives;
            //Respawn -New life


            spriteRenderer.enabled = false;
            collider.enabled = false;
            Invoke("Respawn", 3f);


            if (lives <= 0)
            {
                //game over
            }
        }
        else
        {
            audio.Play();
        }
    }


}
