using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody playerRb;
    private AudioManager audioManager;
    public float bounceForce = 6;



    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        audioManager.Play("bounce");
        playerRb.velocity = new Vector3(playerRb.velocity.x, bounceForce, playerRb.velocity.z);
        string materialName = collision.transform.GetComponent<MeshRenderer>().material.name;

        //Debug.Log(materialName);

        if (materialName.Equals("Safe (Instance)"))
        {
            // the ball hits the new safe area
        }
        else if (materialName.Equals("Unsafe (Instance)"))
        {
            // when the ball hits the unsafe area
           // Debug.Log("Game Over!");
            GameManager.gameOver = true;
            audioManager.Play("game_over");
        }
        else if (materialName.Equals("Last Ring (Instance)") && !GameManager.levelCompleted)
        {
            // Player won the game
            //Debug.Log("Congratulations");
            GameManager.levelCompleted = true;
            audioManager.Play("win_level");
        }
    }
}
