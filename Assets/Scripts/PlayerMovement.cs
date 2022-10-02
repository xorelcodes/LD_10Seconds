using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameManager gameManager;
    public AudioSource soundInputRecorded;
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.CurrentState == GameState.PLANNING)
        {
            Vector3 up = new Vector3(transform.position.x, transform.position.y + 1, 0f);
            Vector3 down = new Vector3(transform.position.x, transform.position.y - 1, 0f);
            Vector3 left = new Vector3(transform.position.x - 1, transform.position.y, 0f);
            Vector3 right = new Vector3(transform.position.x - 1, transform.position.y, 0f);

            RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 1f);

            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 1f);


            RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 1f);


            RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 1f);

            Debug.DrawRay(transform.position, Vector2.left, Color.red);
            if (Input.GetKeyDown("w"))
            {
                soundInputRecorded.Play();
                gameManager.AddMovement("Up!");
            }

            if (Input.GetKeyDown("s"))
            {
                soundInputRecorded.Play();
                gameManager.AddMovement("Down!");
            }

            if (Input.GetKeyDown("d"))
            {
                soundInputRecorded.Play();
                gameManager.AddMovement("Right!");
            }

            if (Input.GetKeyDown("a"))
            {
                soundInputRecorded.Play();
                gameManager.AddMovement("Left!");
            }

        }

        //  Debug.Log("Number of movements: " + gameManager.SavedMovement.Count.ToString());
    }
}
