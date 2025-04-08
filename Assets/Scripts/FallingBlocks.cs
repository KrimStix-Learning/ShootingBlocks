using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FallingBlocks : MonoBehaviour
{
    public Vector2 speedMinMax;
    float speed;

    float visibleHeightThreshold;
    void Start()
    {
        speed = Mathf.Lerp(speedMinMax.x, speedMinMax.y, Difficulty.GetDifficultyPercent());

        visibleHeightThreshold = -Camera.main.orthographicSize - transform.localScale.y;
    }
    // Update is called once per frame
    void Update()
    {
        //move the block down 
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        //move the block down in world space, not basing from the angle
        //transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);

        if(transform.position.y < visibleHeightThreshold)
        {
            Destroy(gameObject);

        }

    }

    private void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (triggerCollider.tag == "Shooting Block")
        {
            //bad practice, use events instead
            //FindAnyObjectByType<GameOver>().OnGameOver();
            Destroy(gameObject);
        }
    }


}
