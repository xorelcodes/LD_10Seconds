using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ExecuteMovement : MonoBehaviour
{
    public GameObject player;
    public AudioSource moveSound;
    private GameManager gameManager;
    private GameState currentState;
    private List<string> savedMovement;
    private Transform playerTransform;
    private bool isMoving = false;
    private Vector3 currentPos;
    private Vector3 goToPosition;
    private bool levelwin = false;

    public Tilemap goalMap;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        currentState = gameManager.CurrentState;
        if (currentState == GameState.EXECUTION && !isMoving)
        {
            isMoving = true;
            StartCoroutine(Execution());

            Debug.Log("STATE " + gameManager.CurrentState);



        }
    }

    IEnumerator Execution()
    {


        savedMovement = gameManager.SavedMovement;
        levelwin = false;

        foreach (string command in savedMovement)
        {

            float elapsedTime = 0;
            float waitTime = .3f;
            currentPos = player.transform.position;

            gameManager.UpdateExecutionLabel(command);
            moveSound.Play();


            playerTransform = player.transform;
            //  yield return new WaitForSecondsRealtime(.35f);
            RaycastHit2D hitRight = Physics2D.Raycast(playerTransform.position, Vector2.right, 1f);

            RaycastHit2D hitLeft = Physics2D.Raycast(playerTransform.position, Vector2.left, 1f);


            RaycastHit2D hitUp = Physics2D.Raycast(playerTransform.position, Vector2.up, 1f);


            RaycastHit2D hitDown = Physics2D.Raycast(playerTransform.position, Vector2.down, 1f);

            Debug.DrawRay(playerTransform.position, Vector2.left, Color.red);

            if (command == "Up!" && hitUp.collider == null)
            {
                goToPosition = new Vector3(playerTransform.position.x, playerTransform.position.y + 1, 0f);

                //while (elapsedTime < waitTime)
                //{
                // playerTransform.position = Vector3.Lerp(currentPos, goToPosition, (elapsedTime / waitTime));
                //  elapsedTime += Time.deltaTime;
                //}
                 while (elapsedTime < waitTime)
                {
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime > waitTime) elapsedTime = waitTime;
                    float newTime = elapsedTime / waitTime;
                    newTime = newTime * newTime * (3f - 2f * newTime);

                    playerTransform.position = Vector3.Lerp(currentPos, goToPosition, newTime);

                    yield return null;
                }


            }
            else if (command == "Up!" && hitUp.collider.tag == "Freeze")
            {
                isMoving = false;
                gameManager.UpdateGameState(GameState.FROZEN);
                yield break;
            }

            if (command == "Down!" && hitDown.collider == null)
            {
                goToPosition = new Vector3(playerTransform.position.x, playerTransform.position.y - 1, 0f);

                while (elapsedTime < waitTime)
                {
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime > waitTime) elapsedTime = waitTime;
                    float newTime = elapsedTime / waitTime;
                    newTime = newTime * newTime * (3f - 2f * newTime);

                    playerTransform.position = Vector3.Lerp(currentPos, goToPosition, newTime);

                    yield return null;
                }

            }
            else if (command == "Down!" && hitDown.collider.tag == "Freeze")
            {
                isMoving = false;
                gameManager.UpdateGameState(GameState.FROZEN);
                yield break;
            }

            if (command == "Right!" && hitRight.collider == null)
            {
                goToPosition = new Vector3(playerTransform.position.x + 1, playerTransform.position.y, 0f);

                // while (elapsedTime < waitTime)
                //   {
                //      playerTransform.position = Vector3.Lerp(currentPos, goToPosition, (elapsedTime / waitTime));
                //       elapsedTime += Time.deltaTime;
                //   }

                while (elapsedTime < waitTime)
                {
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime > waitTime) elapsedTime = waitTime;
                    float newTime = elapsedTime / waitTime;
                    newTime = newTime * newTime * (3f - 2f * newTime);

                    playerTransform.position = Vector3.Lerp(currentPos, goToPosition, newTime);

                    yield return null;
                }
            }
            else if (command == "Right!" && hitRight.collider.tag == "Freeze")
                {
                    isMoving = false;
                    gameManager.UpdateGameState(GameState.FROZEN);
                    yield break;
                }

                if (command == "Left!" && hitLeft.collider == null)
                {
                    goToPosition = new Vector3(playerTransform.position.x - 1, playerTransform.position.y, 0f);

                   while (elapsedTime < waitTime)
                {
                    elapsedTime += Time.deltaTime;
                    if (elapsedTime > waitTime) elapsedTime = waitTime;
                    float newTime = elapsedTime / waitTime;
                    newTime = newTime * newTime * (3f - 2f * newTime);

                    playerTransform.position = Vector3.Lerp(currentPos, goToPosition, newTime);

                    yield return null;
                }

                }
                else if (command == "Left!" && hitLeft.collider.tag == "Freeze")
                {
                    isMoving = false;
                    gameManager.UpdateGameState(GameState.FROZEN);
                    yield break;
                }

                if (goalMap.HasTile(Vector3Int.FloorToInt(playerTransform.position)))
                {

                    gameManager.UpdateGameState(GameState.LEVELWON);
                    isMoving = false;
                    levelwin = true;
                    yield break;
                }
            }
            if (!levelwin)
            {

                gameManager.UpdateGameState(GameState.WAITING);
                isMoving = false;
            }
        }


    }
