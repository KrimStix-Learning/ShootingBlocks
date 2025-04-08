using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 7;
    public GameObject shootingPrefab;
    public event System.Action OnPlayerDeath;
    float nextSpawnTime;
    //hardcoding the width of the screen
    //float screenHalfWidthInWorldUnits = 9f;

    //better approach to get the width of the screen
    float screenHalfWidthInWorldUnits;
    float halfPlayerWidth;
    // Start is called before the first frame update
    void Start()
    {
        //get the width of the player
        halfPlayerWidth = transform.localScale.x / 2f;
        //get the width of the screen
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;

        //Collision System
        //screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize - halfPlayerWidth;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float velocity = inputX * speed;
        float moveAmount = velocity * Time.deltaTime;

        transform.Translate(Vector2.right * velocity * Time.deltaTime);

        float wall = screenHalfWidthInWorldUnits + halfPlayerWidth;
        if (transform.position.x < -wall)
        {
            transform.position = new Vector2(wall , transform.position.y);
        }
        if (transform.position.x > wall)
        {
            transform.position = new Vector2(-wall, transform.position.y);
        }

        //Collision system
        //if (transform.position.x < -screenHalfWidthInWorldUnits)
        //{
        //    transform.position = new Vector2(-screenHalfWidthInWorldUnits, transform.position.y);
        //}
        //if (transform.position.x > screenHalfWidthInWorldUnits)
        //{
        //    transform.position = new Vector2(screenHalfWidthInWorldUnits, transform.position.y);
        //}

        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + 1;
            Instantiate(shootingPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if(triggerCollider.tag == "Falling Block")
        {
            //bad practice, use events instead
            //FindAnyObjectByType<GameOver>().OnGameOver();

            if(OnPlayerDeath != null)
            {
                OnPlayerDeath();
            }
            Destroy(gameObject);
        } 
    }
}
