﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Xsolla.Store;

public class VirtualCurrencyBalance : MonoBehaviour
{
	public GameObject VirtualCurrencyBalancePrefab;
	private Dictionary<string, VirtualCurrencyBalanceUI> currencies;

	private void Awake()
	{
		currencies = new Dictionary<string, VirtualCurrencyBalanceUI>();
	}

	public void AddCurrency(StoreItem item)
	{
		if(VirtualCurrencyBalancePrefab == null) {
			Debug.LogError("VirtualCurrencyBalancePrefab is missing!");
			return;
		}
		if(!currencies.ContainsKey(item.name) && item.image_url != null) {
			
			GameObject currencyBalance = Instantiate(VirtualCurrencyBalancePrefab, transform);
			VirtualCurrencyBalanceUI balanceUI = currencyBalance?.GetComponent<VirtualCurrencyBalanceUI>();
			currencies.Add(item.name, balanceUI);
			balanceUI.Initialize(item);
		}
	}

	public void SetCurrencyBalance(UserVirtualCurrencyBalance balance)
	{
		if (currencies.ContainsKey(balance.name)) {
			currencies[balance.name].SetBalance(balance.amount);
		}
	}

	public void SetCurrenciesBalance(UserVirtualCurrenciesBalance balance)
	{
		balance.items.ToList().ForEach(SetCurrencyBalance);
	}

	public void SetCurrencies(StoreItems items)
	{
		currencies.Values.ToList().ForEach(c => Destroy(c.gameObject));
		currencies.Clear();
		items.items.ToList().ForEach(AddCurrency);
	}
}
