using UnityEngine;
using TMPro; 


public enum CharacterState 
{
    Idle, 
    Listening, 
    Processing, 
    Speaking, 
}

public class CharacterStateController : MonoBehaviour
{
    [SerializeField] private CharacterExpressionController expressionController;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterLightController lightController;
    [SerializeField] private RandomLook randomLook;
    [SerializeField] private int idleExpression = -1;
    [SerializeField] private int listeningExpression = -1;
    [SerializeField] private int processingExpression = -1;
    [SerializeField] private int speakingExpression = -1;

    private CharacterState currentState;
    void Start()
    {
        SetIdle();
    }

    private void ChangeState(CharacterState state)
    {
        currentState = state;
        string animationState = "Idle";
        int expressionNum = -1;
        if (lightController != null) 
        {
            lightController.SetAnimationControl(state == CharacterState.Processing);
        }
        if (randomLook != null) 
        {
            bool enable = state != CharacterState.Processing;
            randomLook.SetAutomaticLook(enable);
        }

        switch (state)
        {
            case CharacterState.Idle:
                animationState = "Idle";
                expressionNum = idleExpression;
                break;
            case CharacterState.Listening:
                animationState = "Listening";
                expressionNum = listeningExpression;
                break;
            case CharacterState.Processing:
                animationState = "Processing";
                expressionNum = processingExpression;
                break;
            case CharacterState.Speaking:
                animationState = "Idle";
                expressionNum = speakingExpression;
                break;
        }

        if (expressionController != null)
        {
            expressionController.SetExpression(expressionNum);
        }
        if (animator != null)
        {
            string stateName = "Base Layer." + animationState;
            animator.CrossFadeInFixedTime(stateName, 0.15f, 0, 0f);
        }
        else 
        {
            Debug.LogWarning("Animator is not assigned");
        }
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
}
