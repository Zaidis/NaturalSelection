using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Shop : MonoBehaviour
{

    public static Shop instance;
    public int m_wallet; //how much money you have

    //menu
    [SerializeField] private GameObject m_shopMenu;
    private bool shopOpen;

    [SerializeField] private TextMeshProUGUI m_walletText;
    [SerializeField] private Image m_shopImage;
    [SerializeField] private TextMeshProUGUI m_description;
    [SerializeField] private TextMeshProUGUI m_upgradeTitle;
    [SerializeField] private Button m_purchaseButton;

    
    private ShopButton selectedUpgrade;


    [SerializeField] private GameObject sawBlades;
    [SerializeField] private GameObject uncleJon;
    [SerializeField] private GameObject truck;

    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    public void AccessShopMenu() {
        if (!shopOpen) {
            m_shopMenu.SetActive(true);
            shopOpen = true;
        } else {
            m_shopMenu.SetActive(false);
            shopOpen = false;
        }
    }

    public void PurchaseUpgrade() {
        m_wallet -= selectedUpgrade.m_cost;
        m_walletText.text = m_wallet.ToString();

        switch (selectedUpgrade.id) {
            case 0:
                //saw blades
                sawBlades.SetActive(true);
                break;
            case 1:
                uncleJon.SetActive(true);
                break;
            case 2:
                truck.GetComponent<CarMovement>().ActivateBoostAbility();
                break;
            case 3:
                break;
        }

    }

    /// <summary>
    /// Updates the description and image for the shop. 
    /// </summary>
    public void UpdateShopLayout(ShopButton button) {
        selectedUpgrade = button;
        m_upgradeTitle.text = button.m_title;
        m_description.text = button.m_Description;
        m_shopImage.sprite = button.m_Image;
        if (CheckIfPurchasable(button.m_cost)) {
            m_purchaseButton.interactable = true;
        } else {
            m_purchaseButton.interactable = false;
        }
    }

    public bool CheckIfPurchasable(int walletAmount) {
        if(m_wallet >= walletAmount) {
            return true;
        }

        return false;
    }

}
