using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TakeDrugEffects : MonoBehaviour
{
    public Image BlackScreen;
    public TMP_Text Message;
    public bool IsFainting = false;
    public float FadeSpeed = 2f;

    private float targetAlpha = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EffectOnIntake(0f);
    }

    // Update is called once per frame
    public GameManager gameManager;
    void Update()
    {
        targetAlpha = IsFainting ? 1f : 0f;

        Color c = BlackScreen.color;

        c.a = Mathf.Lerp(c.a, targetAlpha, Time.deltaTime * FadeSpeed);
        BlackScreen.color = c;

        if (BlackScreen.color.a >= 0.999)
        {
            TakeDrug("");
            gameManager.EndingLevel();
        }
    }

    public void TakeDrug(string message)
    {
        IsFainting = !IsFainting;
        Message.enabled = !Message.enabled;
        Message.text = message;
    }

    public void EffectOnIntake(float alpha)
    {
        Color c = BlackScreen.color;
        c.a = alpha;
        BlackScreen.color = c;
    }
}