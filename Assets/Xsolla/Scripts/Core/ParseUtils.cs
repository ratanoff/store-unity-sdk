﻿using System;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;


namespace Xsolla.Core
{
	public static class ParseUtils
	{
		[PublicAPI]
		public static T FromJson<T>(string json) where T : class
		{
			try
			{
				return JsonConvert.DeserializeObject<T>(json);
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}
	
			return null;
		}
		
		public static Error ParseError(string json)
		{
			Error storeError = FromJson<Error>(json);
			if((storeError == null) || (!storeError.IsValid())) {
				Error.Login loginError = FromJson<Error.Login>(json);
				storeError = loginError?.ToStoreError();
			}
			return storeError;
		}
		
		public static string ParseToken(string token)
		{
			try
			{
				var regex = new Regex(@"token=\S*[&#]");
				var match = regex.Match(token).Value.Replace("token=", string.Empty);
				return match.Remove(match.Length - 1);
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}
	}	
}
