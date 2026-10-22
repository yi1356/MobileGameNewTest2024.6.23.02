using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminBar : MonoBehaviour
{
    public Image amtImage;
    public TextMeshProUGUI amtText;

    public void UpdateStaminUI(float stamin, float delta, float maxStamin)
    {
        amtImage.fillAmount = stamin / maxStamin;
        amtText.text = stamin.ToString("#0") + "/" + maxStamin;
    }
}
