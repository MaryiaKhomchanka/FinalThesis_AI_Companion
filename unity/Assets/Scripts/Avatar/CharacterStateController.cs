using UnityEngine;
using TMPro; 


public enum CharacterState 
{
    Idle, 
    Listening, 
    Processing, 
    Speaking, 
    Paused
}

public class CharacterStateController : MonoBehaviour
{
    [SerializeField] private CharacterExpressionController expressionController;
    [SerializeField] private CharacterLightController lightController;
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private int idleExpression = -1;
    [SerializeField] private int listeningExpression = -1;
    [SerializeField] private int processingExpression = -1;
    [SerializeField] private int speakingExpression = -1;
    [SerializeField] private int pausedExpression = -1;

    private CharacterState currentState;
    void Start()
    {
        SetIdle();
    }

    private void ChangeState(CharacterState state)
    {
        currentState = state;
        int expressionNum = -1;

        switch (state)
        {
            case CharacterState.Idle:
                expressionNum = idleExpression;
                break;
            case CharacterState.Listening:
                expressionNum = listeningExpression;
                break;
            case CharacterState.Processing:
                expressionNum = processingExpression;
                break;
            case CharacterState.Speaking:
                expressionNum = speakingExpression;
                break;
            case CharacterState.Paused:
                expressionNum = pausedExpression;
                break;
        }

        if (expressionController != null)
        {
            expressionController.SetExpression(expressionNum);
        }
        if (statusText != null)
        {
            statusText.text = "Demo state: " + state;
        }
        Debug.Log("Companion state: " + state);
    }

    public void SetIdle()
    {
        ChangeState(CharacterState.Idle);
    }
    public void SetListening()
    {
        ChangeState(CharacterState.Listening);
    }
    public void SetProcessing()
    {
        ChangeState(CharacterState.Processing);
    }
    public void SetSpeaking()
    {
        ChangeState(CharacterState.Speaking);
    }
    public void SetPaused()
    {
        ChangeState(CharacterState.Paused);
    }

}
