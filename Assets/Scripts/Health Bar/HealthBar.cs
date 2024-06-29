using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The HealthBar class is responsible for managing the visual representation of a health bar in the UI.
/// It updates the health bar's value and color based on the current and maximum health of a character or object.
/// </summary>
public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    /// <summary>
    /// Sets the current health value of the health bar.
    /// It updates the slider's value and adjusts the fill color based on the health value.
    /// </summary>
    /// <param name="health">The current health value to be displayed by the health bar.</param>
    public void setHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    /// <summary>
    /// Sets the maximum health value for the health bar.
    /// It initializes the slider's maximum value and sets the fill color to represent full health.
    /// </summary>
    /// <param name="maxHealth">The maximum health value that the health bar can display.</param>
    public void setMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        fill.color = gradient.Evaluate(1f);
    }
}
