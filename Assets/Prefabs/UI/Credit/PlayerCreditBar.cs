using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCreditBar : MonoBehaviour
{
    public UIManager uiManager;
    public CreditComponent creditComponent;
    public Button btnShop;
    public TextMeshProUGUI creditText;
    //public TextMeshProUGUI creditText2;
    // Start is called before the first frame update
    void Start()
    {
        creditComponent.onCreditUpdated += UpdateCredit;
        creditComponent.UpdateCreditImmaditly();
        btnShop.onClick.AddListener(()=> { uiManager.SwitchToShop(); });
    }
    private void UpdateCredit(int credit)
    {
        creditText.text = "$" + credit;
        //creditText2.text = "$" + credit;
    }
}
