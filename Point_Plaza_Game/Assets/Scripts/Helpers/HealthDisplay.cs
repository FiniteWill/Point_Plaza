using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private PlatformerPlayer player;
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image display;
    private int maxHealth = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(playerHealth != null)
        {
            maxHealth = playerHealth.GetStartHealth;
        }
        playerHealth.onHealthChanged += UpdateDisplay;
        if(display != null)
        {
            display.fillMethod = Image.FillMethod.Radial360;
        }
    }

    private void UpdateDisplay(int newHealth)
    {
        display.fillAmount = newHealth * (1 / maxHealth);
    }
}
