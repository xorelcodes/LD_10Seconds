using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.CurrentState == GameState.LEVELWON && Input.GetKeyDown("space")){
            Scene currentScene = SceneManager.GetActiveScene();
            if(currentScene.name =="LevelFinal"){
                SceneManager.LoadScene("EndScreen");

            }else{
                SceneManager.LoadScene(currentScene.buildIndex +1);
            }
        }
    }
}
