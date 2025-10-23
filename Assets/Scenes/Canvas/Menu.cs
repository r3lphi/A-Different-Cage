using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Image image;
    public Canvas menuCanvas;

    [Header("Rotation")]
    public float rotationDegrees = 6f;
    public float rotationSpeed = 0.75f;

    [Header("Scale (Zoom)")]
    public float scaleAmount = 0.06f;
    public float scaleSpeed = 0.6f;

    [Header("Position Bob")]
    public float movePixels = 8f;
    public float moveSpeed = 0.5f;

    RectTransform rt;
    Vector2 startPos;
    Vector3 startScale;
    float startRotZ;
    float phaseRot, phaseScale, phaseMove;

    void Awake()
    {
        if (image == null) image = GetComponent<Image>();
        if (menuCanvas == null) menuCanvas = GetComponentInParent<Canvas>();

        rt = image.rectTransform;
        startPos = rt.anchoredPosition;
        startScale = rt.localScale;
        startRotZ = rt.localEulerAngles.z;

        phaseRot = Random.value * 10f;
        phaseScale = Random.value * 10f;
        phaseMove = Random.value * 10f;
    }

    void Update()
    {
        float t = Time.unscaledTime;

        float rot = Mathf.Sin((t + phaseRot) * rotationSpeed * Mathf.PI * 2f) * rotationDegrees;
        rt.localEulerAngles = new Vector3(0f, 0f, startRotZ + rot);

        float s = 1f + Mathf.Sin((t + phaseScale) * scaleSpeed * Mathf.PI * 2f) * scaleAmount;
        rt.localScale = startScale * s;

        float dx = Mathf.Sin((t + phaseMove) * moveSpeed * Mathf.PI * 2f) * movePixels;
        float dy = Mathf.Sin((t + phaseMove + 0.5f) * moveSpeed * Mathf.PI * 2f) * movePixels * 0.6f;
        rt.anchoredPosition = startPos + new Vector2(dx, dy);
    }

    public void OnPlayPressed()
    {
        // if (menuCanvas != null)
        //     menuCanvas.gameObject.SetActive(false);

        print("Pressed");
        SceneManager.LoadScene("SampleScene");
    }

    public void OnQuitPressed()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}