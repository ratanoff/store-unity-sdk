﻿using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using Xsolla;

public class ItemUI : MonoBehaviour
{
    [SerializeField]
    private Image _item_Image;
    [SerializeField]
    private Text _item_Name;
    [SerializeField]
    private Text _item_Description;
    [SerializeField]
    private Text _item_Price;
    [SerializeField]
    private Button _buy_Button;

    [SerializeField]
    Button addToCartButton;

    private Coroutine _loading_Routine;
    private Xsolla.XsollaStoreItem _itemInformation;
    private void Awake()
    {
	    var storeController = FindObjectOfType<StoreController>();
	    
        _buy_Button.onClick.AddListener(() => { 
	        Xsolla.XsollaStore.Instance.BuyItem(_itemInformation.sku, error =>
	        {
		        Debug.Log(error.ToString());
	        }); 
        });
        
        addToCartButton.onClick.AddListener(() =>
        {
	        var cart = storeController.Cart;
	        if (cart != null)
	        {
		        Xsolla.XsollaStore.Instance.AddItemToCart(cart, _itemInformation, new XsollaQuantity {quantity = 1},
			        () =>
			        {
				        print("item added");
				        Xsolla.XsollaStore.Instance.GetCartItems(cart, items => print("heeeey"), error => print("error"));
			        }, error => print(error.ToString()));
	        }
        });
    }
    public void Initialize(Xsolla.XsollaStoreItem itemInformation)
    {
        _itemInformation = itemInformation;

        if (_itemInformation.prices.Length != 0)
        _item_Price.text = _itemInformation.prices[0].amount + " " + _itemInformation.prices[0].currency;

        _item_Name.text = _itemInformation.name;
        _item_Description.text = _itemInformation.description;
    }
    private void OnEnable()
    {
        if (_loading_Routine == null && _item_Image.sprite == null && _itemInformation.image_url != "")
            _loading_Routine = StartCoroutine(LoadImage(_itemInformation.image_url));
    }
    private IEnumerator LoadImage(string url)
    {
        using (WWW www = new WWW(url))
        {
            yield return www;
            Sprite sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f));
            _item_Image.sprite = sprite;
        }
    }
}
