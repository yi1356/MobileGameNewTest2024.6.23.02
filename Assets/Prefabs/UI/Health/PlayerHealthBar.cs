using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Image amtImage;
    public TextMeshProUGUI amtText;

    public void UpdateHealthUI(float health, float delta, float maxHealth)
    {
        amtImage.fillAmount = health / maxHealth;
        amtText.text = health.ToString("#0")+"/"+maxHealth;
    }
}
