using UnityEngine;
using TMPro;
using System.Collections;

public class TextBlinker : MonoBehaviour
{
    public TextMeshProUGUI textToBlink; // Reference to the TMP text to blink
    public float blinkInterval = 1f; // Total time for one blink cycle (fade in + fade out)
    public float fadeSpeed = 1f; // Speed of fading (higher = faster)

    private bool isBlinking = false;

    void Start()
    {
        if (textToBlink != null)
        {
            StartBlinking();
        }
    }

    // Start the blinking coroutine
    public void StartBlinking()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            StartCoroutine(BlinkText());
        }
    }

    // Stop the blinking coroutine
    public void StopBlinking()
    {
        if (isBlinking)
        {
            isBlinking = false;
            StopCoroutine(BlinkText());
            SetAlpha(1f); // Ensure text is fully visible when stopped
        }
    }

    // Coroutine to handle blinking with fade
    private IEnumerator BlinkText()
    {
        while (isBlinking)
        {
            // Fade out
            yield return FadeText(1f, 0f);

            // Pause (optional, remove if continuous blinking is desired)
            yield return new WaitForSeconds(blinkInterval / 2);

            // Fade in
            yield return FadeText(0f, 1f);

            // Pause (optional, remove if continuous blinking is desired)
            yield return new WaitForSeconds(blinkInterval / 2);
        }
    }

    // Fades the text's alpha from startAlpha to endAlpha
    private IEnumerator FadeText(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color color = textToBlink.color;

        while (elapsedTime < fadeSpeed)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeSpeed);
            textToBlink.color = color;
            yield return null;
        }

        // Ensure exact final alpha value
        color.a = endAlpha;
        textToBlink.color = color;
    }

    // Set text alpha directly
    private void SetAlpha(float alpha)
    {
        Color color = textToBlink.color;
        color.a = alpha;
        textToBlink.color = color;
    }
}