﻿using System;
using System.Collections.Generic;

namespace Xsolla
{
	[Serializable]
	public class XsollaError
	{
		public string statusCode;
		public string errorCode;
		public string errorMessage;

		public ErrorType ErrorType { get; set; }

		public static XsollaError NetworkError
		{
			get { return new XsollaError {ErrorType = ErrorType.NetworkError}; }
		}
		
		public static XsollaError UnknownError
		{
			get { return new XsollaError {ErrorType = ErrorType.UnknownError}; }
		}

		public static readonly Dictionary<string, ErrorType> GeneralErrors =
			new Dictionary<string, ErrorType>()
			{
				{"403", ErrorType.InvalidToken},
				{"405", ErrorType.MethodIsNotAllowed},
			};
		
		public static readonly Dictionary<string, ErrorType> ItemsListErrors =
			new Dictionary<string, ErrorType>()
			{
				{"422", ErrorType.InvalidData},
			};

		public static readonly Dictionary<string, ErrorType> BuyItemErrors =
			new Dictionary<string, ErrorType>()
			{
				{"422", ErrorType.ProductDoesNotExist},
			};
		
		public static readonly Dictionary<string, ErrorType> CreateCartErrors =
			new Dictionary<string, ErrorType>()
			{
				{"403", ErrorType.InvalidToken},
				{"404", ErrorType.UserNotFound},
				{"422", ErrorType.InvalidData},
			};
		
		public static readonly Dictionary<string, ErrorType> AddToCartCartErrors =
			new Dictionary<string, ErrorType>()
			{
				{"403", ErrorType.InvalidToken},
				{"404", ErrorType.CartNotFound},
				{"422", ErrorType.InvalidData},
			};
		
		public static readonly Dictionary<string, ErrorType> GetCartItemsErrors =
			new Dictionary<string, ErrorType>()
			{
				{"403", ErrorType.InvalidToken},
				{"404", ErrorType.CartNotFound},
				{"422", ErrorType.InvalidData},
			};
		
		public static readonly Dictionary<string, ErrorType> DeleteFromCartErrors =
			new Dictionary<string, ErrorType>()
			{
				{"403", ErrorType.InvalidToken},
				{"404", ErrorType.CartNotFound},
				{"422", ErrorType.InvalidData},
			};
		
		public static readonly Dictionary<string, ErrorType> BuyCartErrors =
			new Dictionary<string, ErrorType>()
			{
				{"422", ErrorType.CartNotFound},
			};
		
		public override string ToString()
		{
			return string.Format("Error - Type: {0}. Status code: {1}. Error code: {2}. Error message: {3}.", ErrorType, statusCode, errorCode, errorMessage);
		}
	}
}
