using UnityEngine;
using Live2D.Cubism.Framework.LookAt;

public class RandomLook : MonoBehaviour, ICubismLookTarget
{
    [SerializeField] private Transform model;
    [SerializeField] private CubismLookController lookController;

    [SerializeField, Range(0f, 1f)] private float horizontal = 0.35f;
    [SerializeField, Range(0f, 1f)] private float vertical = 0.35f;

    [SerializeField] private float minPause = 1.5f;
    [SerializeField] private float maxPause = 4f;

    private Vector3 target = Vector3.zero;
    private float nextMoveTime;
    private bool automaticLook = true;

    private void Start()
    {
        nextMoveTime = Time.time + 1f;
    }

    private void Update()
    {
        if (!automaticLook)
        {
            return;
        }

        if (Time.time >= nextMoveTime)
        {
            float x = Random.Range(-horizontal, horizontal);
            float y = Random.Range(-vertical, vertical);

            target = new Vector3(x, y, 0f);
            if (Random.value < 0.3f)
            {
                target = Vector3.zero;
            }

            float pause = Random.Range(minPause, Mathf.Max(minPause, maxPause));

            nextMoveTime = Time.time + pause;
        }
    }

    public Vector3 GetPosition() 
    {
        if (model == null) 
        {
            return transform.position; 
        }
        return model.TransformPoint(target);
    }

    public bool IsActive() 
    {
        return automaticLook && isActiveAndEnabled; 
    }

    public void SetAutomaticLook(bool enabled)
    {
        automaticLook = enabled;
        if (lookController != null) 
        {
            lookController.enabled = enabled; 
        }

        if (enabled) 
        {
            target = Vector3.zero;
            nextMoveTime = Time.time + 1f;
        }
    }
}
