using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    
    [Header("Shop Settings")]
    public GameObject shopCanvas;
    public bool ShopIsOpen => shopIsOpen;

    [Header("Database objects")]
    public List<bookMark> allBookmarksAvailable;

    [Header("UI elements")]
    public Button[] bookmarkButtons;
    public TextMeshProUGUI[] priceTexts;
    public Image[] iconImages;
    public Image[] inkIcon;

    public GameObject[] sellPoster;
    
    private bookMark[] currentShopItems = new bookMark[4];
    private bool shopIsOpen = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(shopIsOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseShop();
        }
    }

    public void SetUpShopUI(GameObject canvas, Button[] buttons, TextMeshProUGUI[] prices, Image[] icons, Image[] inks, GameObject[] posters)
    {
        this.shopCanvas = canvas;
        this.bookmarkButtons = buttons;
        this.priceTexts = prices;
        this.iconImages = icons;
        this.inkIcon = inks;
        this.sellPoster = posters;

        for (int i = 0; i < bookmarkButtons.Length; i++)
        {
            int index = i; // Capturamos el índice para el evento
            bookmarkButtons[i].onClick.RemoveAllListeners(); // Limpiamos anteriores
            bookmarkButtons[i].onClick.AddListener(() => BuyItem(index));
        }
    }

    public void GenerateShopItems()
    {
        if(MapManager.Instance == null || MapManager.Instance.currentRoomData.roomType != RoomType.Shop)
            return;

        shopIsOpen = true;
        shopCanvas.SetActive(true);
        GameManager.Instance.FreezePlayer(true);

        for(int i = 0; i < currentShopItems.Length; i++)
        {
            bookMark selectedBookmark = allBookmarksAvailable[i];

            currentShopItems[i] = selectedBookmark;

            //calculate the price
            int finalPrice = CalculatePrice(selectedBookmark);

            //Update the UI
            priceTexts[i].text = finalPrice.ToString();
            iconImages[i].sprite = selectedBookmark.icon;

            iconImages[i].enabled = true;
            bookmarkButtons[i].interactable = true;
            sellPoster[i].SetActive(false);
        }
    }

    private int CalculatePrice(bookMark selected)
    {
        float multiplier = GameManager.Instance != null ? GameManager.Instance.priceMultiplier : 1f;
        return Mathf.RoundToInt(selected.basePrice * multiplier);
    }

    //function to the OnClick function of the button
    public void BuyItem(int buttonIndex)
    {
        bookMark itemToBuy = currentShopItems[buttonIndex];

        int finalPrice = CalculatePrice(itemToBuy);

        //Has enough money?
        if(InkManager.instance != null && InkManager.instance.SpendInk(finalPrice))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            string popUpInfo = itemToBuy.ApplyEffect(player);

            bookmarkButtons[buttonIndex].interactable = false;
            iconImages[buttonIndex].enabled = false;
            priceTexts[buttonIndex].enabled = false;
            inkIcon[buttonIndex].enabled = false;
            sellPoster[buttonIndex].SetActive(true);

            if(NotificationPopup.Instance != null)
            {
                NotificationPopup.Instance.ShowNotification(popUpInfo);
            }
        }
    }

    //when we exit the shop
    public void CloseShop()
    {
        shopIsOpen = false;

        GameManager.Instance.FreezePlayer(false);

        shopCanvas.SetActive(false);
    }
}
