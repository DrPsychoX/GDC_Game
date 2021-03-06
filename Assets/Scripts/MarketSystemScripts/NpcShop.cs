using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class NpcShop : MonoBehaviour
{
    [SerializeField] ItemsInShop itemsInShop;

    [SerializeField] Transform shopUi;


    Button buyingOptionButton;
    Button sellingOptionButton;

    bool isTheBuyingOptionEnabled;

    InventoryBehaviour inventoryBehaviour;

    CoinSystem coinSystem;

    List<Transform> activeSlots=new List<Transform>();


    // Start is called before the first frame update
    void Start()
    {
       Invoke("GetTheCoinAndInventorySystem",1f);

        SetTheBuyingAndSellingButtons();

        SetTheBuyingOrSellingOption(true);
    }

    private void GetTheCoinAndInventorySystem()
    {
        inventoryBehaviour = FindObjectOfType<InventoryBehaviour>();

        coinSystem = FindObjectOfType<CoinSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        DisplayShop();
    }

    private void OnTriggerExit(Collider other)
    {
        CloseTheShop();
    }

    void DisplayShop()
    {
        Transform itemSlot;

        shopUi.gameObject.SetActive(true);

        for(int i=0; i< itemsInShop.items.Count; i++)
        {
            itemSlot = shopUi.GetChild(1).GetChild(i);

            itemSlot.gameObject.SetActive(true);

            SetTheItemDetails(itemSlot, i);

            activeSlots.Add(itemSlot);
        }
    }

    void CloseTheShop()
    {
        shopUi.gameObject.SetActive(false);

        foreach(Transform slot in activeSlots)
        {
            slot.gameObject.SetActive(false);
        }

        activeSlots.Clear();
    }


    void SetTheItemDetails(Transform itemSlot,int itemIndex)
    {
        itemSlot.GetChild(0).GetComponent<Image>().sprite= itemsInShop.items[itemIndex].image;

        itemSlot.GetChild(1).GetComponent<TextMeshProUGUI>().text= itemsInShop.items[itemIndex].name;

        itemSlot.GetChild(2).
            GetChild(0).
            transform.
            GetComponentInChildren<TextMeshProUGUI>().
            text= itemsInShop.items[itemIndex].price.ToString();

        itemSlot.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();

       itemSlot.GetChild(2).GetComponent<Button>().
            onClick.AddListener(delegate { PurchaseOrSellItem(itemsInShop.items[itemIndex]); });



    }

    void PurchaseOrSellItem(ItemsInShop.Item item)
    {
        if(isTheBuyingOptionEnabled==true)
        {
            if(inventoryBehaviour.AreThereEmptySlots()==true)
            {
                if(item.price<coinSystem.CoinsAmount())
                {
                    inventoryBehaviour.AddItemToInventory(item.image);

                    coinSystem.RemoveCoins(item.price);
                }
           

                print("works purchase");
            }

        }
        else
        {
            if(inventoryBehaviour.DoesItemExistInInvenotry(item.image))
            {
                inventoryBehaviour.RemoveItemFromInventory(item.image);


                coinSystem.AddCoins(item.price);

            }
        }
    }


    void SetTheBuyingAndSellingButtons()
    {
        buyingOptionButton = shopUi.GetChild(2).GetComponent<Button>();

        buyingOptionButton.onClick.AddListener(delegate { SetTheBuyingOrSellingOption(true); });

        sellingOptionButton = shopUi.GetChild(3).GetComponent<Button>();

        sellingOptionButton.onClick.AddListener(delegate { SetTheBuyingOrSellingOption(false); });

    }

    void SetTheBuyingOrSellingOption(bool activateBuyingOption)
    {
        if(activateBuyingOption==true)
        {
            buyingOptionButton.GetComponent<Image>().color = Color.green;

            sellingOptionButton.GetComponent<Image>().color = Color.white;

            isTheBuyingOptionEnabled = true;
        }
        else
        {
            sellingOptionButton.GetComponent<Image>().color = Color.green;

            buyingOptionButton.GetComponent<Image>().color = Color.white;

            isTheBuyingOptionEnabled = false;
        }
    }
}


