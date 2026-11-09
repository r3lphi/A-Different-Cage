using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MessageManager : MonoBehaviour
{
    public TMP_Text dialogue;
    public GameObject majorPanel;
    public TMP_Text majorDialogue;

    public float Lifetime;
    public float FadeSpeed;

    public void SendDialogue(string message)
    {
        if (dialogue.color.a > 0)
        {
            StopCoroutine(PrintDialogue(dialogue.text));
            dialogue.color = new Color(dialogue.color.r, dialogue.color.g, dialogue.color.b, 0);
        }
        StartCoroutine(PrintDialogue(message));
    }
    private IEnumerator PrintDialogue(string message)
    {
        dialogue.text = message;
        while (dialogue.color.a < 1)
        {
            dialogue.color = new Color(dialogue.color.r, dialogue.color.g, dialogue.color.b, dialogue.color.a + FadeSpeed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(Lifetime);
        while (dialogue.color.a > 0)
        {
            dialogue.color = new Color(dialogue.color.r, dialogue.color.g, dialogue.color.b, dialogue.color.a - FadeSpeed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
