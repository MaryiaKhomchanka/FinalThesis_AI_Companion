using UnityEngine;
using Live2D.Cubism.Framework.Expression;
[RequireComponent (typeof(CubismExpressionController))]

public class CharacterExpressionController : MonoBehaviour
{
    private CubismExpressionController expressionController; 
    private void Awake()
    {
        expressionController = GetComponent<CubismExpressionController>();
    }

    public void SetExpression(int num)
    {
        CubismExpressionData[] expressions = null; 

        if (expressionController.ExpressionsList != null)
        {
            expressions = expressionController.ExpressionsList.CubismExpressionObjects;
        }

        if (expressions == null || num < -1 || num >= expressions.Length) 
        {
            Debug.LogWarning("Invalid index or missing expression list");
            return;
        }

        expressionController.CurrentExpressionIndex = num; 
    }

    public void NextExpression()
    {
        CubismExpressionData[] expressions = null;

        if (expressionController.ExpressionsList != null)
        {
            expressions = expressionController.ExpressionsList.CubismExpressionObjects;
        }

        if (expressions == null || expressions.Length == 0) 
        {
            Debug.LogWarning("No expressions found");
            return;
        }

        int nextNum = expressionController.CurrentExpressionIndex + 1; 
        if (nextNum >= expressions.Length) 
        {
            nextNum = 0;
        }

        SetExpression(nextNum);
    }

    public void ResetExpression() 
    {
        SetExpression(-1);
    }
}
