﻿using System;

namespace Xsolla.Store
{
	[Serializable]
	public class CartPrice
	{
		public float amount;
		public float amount_without_discount;
		public string currency;
	}
}