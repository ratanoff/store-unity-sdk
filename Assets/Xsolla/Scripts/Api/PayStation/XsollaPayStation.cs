﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;
using Xsolla.Core;
using Xsolla.Paystation;

namespace Xsolla.PayStation
{
	public class XsollaPayStation : MonoSingleton<XsollaPayStation>
	{
		void OpenPurchaseUi(Token xsollaToken)
		{
			Application.OpenURL("https://secure.xsolla.com/paystation3/?access_token=" + xsollaToken.token);
		}

		public void OpenPayStation()
		{
			//RequestToken(OpenPurchaseUi, print);
			
			StartCoroutine(PostRequest("https://livedemo.xsolla.com/paystation/token_unreal.php", (s) => OpenPurchaseUi(new Token() {token = s}), () => print("Error occured!")));
		}

		void RequestToken([NotNull] Action<Token> onSuccess, [CanBeNull] Action<Error> onError)
		{
			var urlBuilder = new StringBuilder(string.Format("https://api.xsolla.com/merchant/v2/merchants/{0}/token", XsollaSettings.MerchantId));
			
			var headers = new List<WebRequestHeader>() { WebRequestHeader.ContentTypeHeader(), WebRequestHeader.AuthBasic("API KEY")};

			var jsonData = JsonUtility.ToJson(GenerateTestToken());
			print(jsonData);
			
			WebRequestHelper.Instance.PostRequest(urlBuilder.ToString(), jsonData, new WWWForm(), headers, onSuccess, onError);
		}

		// This is temporary
		IEnumerator PostRequest(string url, Action<string> onComplete, Action onError)
		{
			var webRequest = UnityWebRequest.Post(url, new WWWForm());

#if UNITY_2018_1_OR_NEWER
			yield return webRequest.SendWebRequest();
#else
			yield return webRequest.Send();
#endif

			if (webRequest.isNetworkError)
			{
				onError();
			}
			else
			{
				onComplete(webRequest.downloadHandler.text);
			}
		}

		// This is temporary
		TokenRequest GenerateTestToken()
		{
			var token = new TokenRequest();

			token.user = new User();
			token.user.id = new Id();
			token.user.email = new Email();
			token.user.id.value = "user_test";
			token.user.email.value = "user@test.com";
			
			token.settings = new Settings();
			token.settings.project_id = XsollaSettings.PayStationProjectId;
			
			token.purchase = new Purchase();
			token.purchase.checkout = new Checkout();
			token.purchase.checkout.amount = 9.99f;
			token.purchase.checkout.currency = "USD";
			
			return token;
		}
	}
}
