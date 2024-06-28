using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controlls death messages and calls scene change function.
/// </summary>
public class DeathMessagePanel : MonoBehaviour
{
    public float fadeDuration = 3.0f;

    private Image panelImage;

    private TMP_Text text;

    /// <summary>
    /// Initializes private variables.
    /// </summary>
    private void Awake()
    {
        panelImage = GetComponent<Image>();
        text = GetComponentInChildren<TMP_Text>();
    }
    /// <summary>
    /// Starts Death courutine.
    /// </summary>
    private void OnEnable()
    {
        StartCoroutine(Death());
    }

    /// <summary>
    /// Handles fade-in effect, calls scene switching function at the end.
    /// </summary>
    /// <returns>Ienumerator for a courutine.</returns>
    private IEnumerator Death()
    {

        float currentTime = 0.0f;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, currentTime / fadeDuration);
            panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, alpha);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            yield return null;
        }

        panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, 1f);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);

        yield return new WaitForSecondsRealtime(2f);

        SaveManager.Instance.MainMenu();

    }

}
