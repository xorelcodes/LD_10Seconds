using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreenSceneManager : MonoBehaviour
{
    private Scene currentScene;
    // Start is called before the first frame update
    void Start()
    {
            currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")){
            if(currentScene.name == "EndScreen"){

            SceneManager.LoadScene(0);
            }else
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
    }
}
