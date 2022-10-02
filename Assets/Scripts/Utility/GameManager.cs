using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState CurrentState;
    public List<string> SavedMovement;
    public TextTimer gameTimer;
    public Text inputLabel;
    public Text movesDisplay;
    public Text infoLabel;
    public Color playerColor;
    public Color waitColor;
    private GameObject infoTextObject;
    private int numberOfMoves = 0;
    public AudioSource bgm;

    void Awake()
    {
        Instance = this;
        SavedMovement = new List<string>();
        UpdateBottomInfoLabel("");
        UpdateGameState(GameState.PLANNING);
        inputLabel.color = waitColor;
        movesDisplay.text = "0";
        numberOfMoves = 0;
    }
    // Start is called before the first frame update

    private void Update()
    {

        if (CurrentState == GameState.LEVELWON && Input.GetKeyDown("space"))
            infoLabel.text = "Loading Next Level...";
    }
    public void UpdateGameState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log("UPDATING STATE " + newState);

        switch (newState)
        {
            case GameState.PLANNING:
                inputLabel.color = waitColor;
                bgm.Play();
                gameTimer.StartTimer();
                break;

            case GameState.EXECUTION:
                bgm.Stop();
                UpdateBottomInfoLabel("");
                inputLabel.color = playerColor;
                break;

            case GameState.WAITING:
                ClearMovement();
                UpdateBottomInfoLabel("Quick! 10 more seconds!");
                UpdateGameState(GameState.PLANNING);
                break;

            case GameState.LEVELWON:
                inputLabel.color = playerColor;
                ClearMovement();
                AudioManager.PlaySound(AudioManager.Sound.Victory);
                UpdateBottomInfoLabel("Press space to continue");
                UpdateExecutionLabelWinner();
                break;

            case GameState.FROZEN:
                ClearMovement();
                bgm.Stop();
                AudioManager.PlaySound(AudioManager.Sound.FreezeTrap);
                UpdateBottomInfoLabel("Trap hit! Remaining Moves Lost!");
                UpdateGameState(GameState.PLANNING);
                break;

        }
    }

    public void AddMovement(string command)
    {
        inputLabel.text = command;
        StartCoroutine(FadeTextToZeroAlpha(2, inputLabel));
        numberOfMoves++;
        movesDisplay.text = numberOfMoves.ToString();

        SavedMovement.Add(command);
    }

    public void ClearMovement()
    {
        SavedMovement.Clear();
    }

    public void UpdateBottomInfoLabel(string text)
    {

        infoLabel.text = text;
    }
    public void UpdateExecutionLabelWinner()
    {
        inputLabel.color = new Color(inputLabel.color.r, inputLabel.color.g, inputLabel.color.b, 1);
        inputLabel.text = "Goal Get";
    }
    public void UpdateExecutionLabel(string command)
    {
        inputLabel.text = command;
        StartCoroutine(FadeTextToZeroAlpha(2, inputLabel));
    }
    IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
