using UnityEngine;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework;

public enum LightColor 
{
    Blue, 
    Red,
    Yellow,
    Green
}

public class CharacterLightController : MonoBehaviour, ICubismUpdatable
{
    [SerializeField] private string lightId;
    [SerializeField] private string redId;
    [SerializeField] private string yellowId;
    [SerializeField] private string greenId;

    [SerializeField, Range(0f, 1f)]
    private float minBrightness = 0.15f;

    [SerializeField, Range(0f, 1f)]
    private float maxBrightness = 0.4f;


    [SerializeField, Min(0.2f)]
    private float pulseDuration = 2f;

    private CubismParameter lights;
    private CubismParameter red;
    private CubismParameter yellow;
    private CubismParameter green;

    private LightColor currentColor = LightColor.Blue;
    private bool lightsOn = true;
    private bool isPulsing = false;
    private float pulseTime = 0f;

    public int ExecutionOrder => 9000;
    public bool NeedsUpdateOnEditing => false;
    public bool HasUpdateController { get; set; }

    private void Start()
    {
        HasUpdateController = GetComponent<CubismUpdateController>() != null;
        CubismModel model = GetComponent<CubismModel>();

        if (model == null)
        {
            Debug.LogError("Model not found");
            return;
        }

        lights = model.Parameters.FindById(lightId);
        red = model.Parameters.FindById(redId);
        yellow = model.Parameters.FindById(yellowId);
        green = model.Parameters.FindById(greenId);

        if (lights == null || red == null || yellow == null || green == null)
        {
            Debug.LogError("Error in parameter IDs");
        }

    }

    public void SetBlue()
    {
        currentColor = LightColor.Blue;
        lightsOn = true;
    }

    public void SetRed()
    {
        currentColor = LightColor.Red;
        lightsOn = true;
    }

    public void SetYellow()
    {
        currentColor = LightColor.Yellow;
        lightsOn = true;
    }

    public void SetGreen()
    {
        currentColor = LightColor.Green;
        lightsOn = true;
    }

    public void TurnOff()
    {
        lightsOn = false;
    }

    public void TurnOn()
    {
        lightsOn = true;
    }

    public void StartPulsing()
    {
        isPulsing = true;
        lightsOn = true;
        pulseTime = 0f;
    }

    public void StopPulsing() 
    {
        isPulsing = false;
    }

    public void OnLateUpdate() 
    {
        if (lights == null || red == null || yellow == null || green == null) 
        {
            return;
        }

        float brightness = 0f;

        if (lightsOn)
        {
            brightness = maxBrightness;

            if (isPulsing) 
            {
                pulseTime += Time.deltaTime;
                float wave = (Mathf.Cos(2f * Mathf.PI * pulseTime / pulseDuration) + 1f) / 2f;
                brightness = Mathf.Lerp(minBrightness, 1f, wave);
            }
        }

        lights.Value = brightness;

        if (currentColor == LightColor.Red)
        {
            red.Value = 1f;
        }
        else 
        {
            red.Value = 0f;
        }

        if (currentColor == LightColor.Yellow)
        {
            yellow.Value = 1f;
        }
        else 
        {
            yellow.Value = 0f;
        }

        if (currentColor == LightColor.Green)
        {
            green.Value = 1f;
        }
        else 
        {
            green.Value = 0f;
        }
    }

    private void LateUpdate() 
    {
        if (!HasUpdateController) 
        {
            OnLateUpdate();
        }
    }

}
