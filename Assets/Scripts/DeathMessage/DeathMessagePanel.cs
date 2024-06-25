using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathMessagePanel : MonoBehaviour
{
    public float fadeDuration = 3.0f;

    private Image panelImage;

    private TMP_Text text;

    private void Awake()
    {
        panelImage = GetComponent<Image>();
        text = GetComponentInChildren<TMP_Text>();
    }
    private void OnEnable()
    {
        StartCoroutine(Death());
    }

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

        SaveManager.Instance.MainMenu();

    }

}
