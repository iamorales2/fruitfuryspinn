using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NinjaSkinPrice : MonoBehaviour
{
    public TMP_Text coinsValue; // Displays the user's coin balance
    public Button itemButton; // Button for the ninja skin
    public TMP_Text priceText; // Displays the price or state (BUY, EQUIP, EQUIPPED)
    private int price; // Price of the skin
    private SkinState currentState = SkinState.BUY; // Initial state
    public GameObject PopUpMessage;
    public TextMeshProUGUI SkinPrice;
    public Button Confirm;
    public Button Decline;
    public Image SkinImage;

    // Static reference to the currently equipped skin
    private static NinjaSkinPrice currentlyEquippedSkin;


    private enum SkinState
    {
        FREE,
        BUY,
        EQUIP,
        EQUIPPED
    }

    void Start()
    {
        // Load coins and skin state from PlayerPrefs
        if (itemButton == null || PopUpMessage == null || Confirm == null || Decline == null || SkinImage == null)
        {
            Debug.LogError("One or more required references are missing in the Inspector!");
            return;
        }

        if (!int.TryParse(priceText.text, out price))
        {
            Debug.LogError("Invalid price format in priceText!");
            price = 0;
        }

        UpdateButtonState();
    }

    public void BuySkin()
    {
        if (!int.TryParse(coinsValue.text, out int myCoins))
        {
            Debug.LogError("Invalid coin value in coinsValue.text!");
            return;
        }

        if (price == 0 && currentState == SkinState.FREE)
        {
            EquipSkin();
            return;
        }

        if (myCoins >= price && currentState == SkinState.BUY)
        {
            myCoins -= price;
            coinsValue.text = myCoins.ToString();
            EquipSkin();
            return;
        }

        if (currentState == SkinState.EQUIP)
        {
            EquipSkin();
            return;
        }

        Debug.Log("Action not allowed: Insufficient coins or invalid state.");
    }

    private void EquipSkin()
    {
        // Unequip the previously equipped skin
        if (currentlyEquippedSkin != null && currentlyEquippedSkin != this)
        {
            currentlyEquippedSkin.UnequipSkin();
        }

        // Equip this skin
        currentState = SkinState.EQUIPPED;
        currentlyEquippedSkin = this;

        UpdateButtonState();
        HidePopup();
    }

    private void UnequipSkin()
    {
        currentState = SkinState.EQUIP;
        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        switch (currentState)
        {
            case SkinState.FREE:
                priceText.text = "FREE";
                priceText.color = Color.white;
                itemButton.interactable = true;
                break;
            case SkinState.BUY:
                priceText.text = $"{price}";
                priceText.color = Color.white;
                itemButton.interactable = true;
                break;
            case SkinState.EQUIP:
                priceText.text = "EQUIP";
                priceText.color = Color.yellow;
                itemButton.interactable = true;
                break;
            case SkinState.EQUIPPED:
                priceText.text = "EQUIPPED";
                priceText.color = Color.green;
                itemButton.interactable = false;
                break;
        }
    }

    public void ShowPopup()
    {
        if (currentState == SkinState.EQUIP)
        {
            HidePopup();
            BuySkin();
            return;
        }

        SkinImage.sprite = itemButton.GetComponent<Image>().sprite;
        SkinPrice.text = priceText.text;
        PopUpMessage.SetActive(true);

        Confirm.onClick.RemoveAllListeners();
        Decline.onClick.RemoveAllListeners();

        Confirm.onClick.AddListener(BuySkin);
        Decline.onClick.AddListener(HidePopup);
    }

    public void HidePopup()
    {
        PopUpMessage.SetActive(false);
    }
}
