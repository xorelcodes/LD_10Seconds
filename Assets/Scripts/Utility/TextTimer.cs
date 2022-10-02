using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextTimer : MonoBehaviour
{
    public float CountdownValue = 10.0f;
    public Text DisplayObject;
    // Start is called before the first frame update
    void Start()
    {

    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (CountdownValue > 0)
        {
            CountdownValue -= Time.deltaTime;
        }
        double displayValue = System.Math.Round(CountdownValue, 2);

        DisplayObject.text = displayValue.ToString();

        if (CountdownValue <= 0 && GameManager.Instance.CurrentState == GameState.PLANNING)
        {
            displayValue = 0.00f;
             DisplayObject.text = "0";
            GameManager.Instance.UpdateGameState(GameState.EXECUTION);
            displayValue = 0.00f;
             DisplayObject.text = "0";
        }
    }

    public void StartTimer(){
        CountdownValue = 10;
        
    }
}
