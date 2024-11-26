/*
****************************************************************************
*  Copyright (c) 2024,  Skyline Communications NV  All Rights Reserved.    *
****************************************************************************

By using this script, you expressly agree with the usage terms and
conditions set out below.
This script and all related materials are protected by copyrights and
other intellectual property rights that exclusively belong
to Skyline Communications.

A user license granted for this script is strictly for personal use only.
This script may not be used in any way by anyone without the prior
written consent of Skyline Communications. Any sublicensing of this
script is forbidden.

Any modifications to this script by the user are only allowed for
personal use and within the intended purpose of the script,
and will remain the sole responsibility of the user.
Skyline Communications will not be responsible for any damages or
malfunctions whatsoever of the script resulting from a modification
or adaptation by the user.

The content of this script is confidential information.
The user hereby agrees to keep this confidential information strictly
secret and confidential and not to disclose or reveal it, in whole
or in part, directly or indirectly to any person, entity, organization
or administration without the prior written consent of
Skyline Communications.

Any inquiries can be addressed to:

	Skyline Communications NV
	Ambachtenstraat 33
	B-8870 Izegem
	Belgium
	Tel.	: +32 51 31 35 69
	Fax.	: +32 51 31 01 29
	E-mail	: info@skyline.be
	Web		: www.skyline.be
	Contact	: Ben Vandenberghe

****************************************************************************
*/

namespace UDAPI_DataAPI_Proxy_1
{
	using System;
	using System.Collections.Generic;
	using System.Net;
	using System.Net.Http;
	using System.Net.NetworkInformation;
	using System.Text;

	using Skyline.DataMiner.Automation;

	using Skyline.DataMiner.Net.Apps.UserDefinableApis;
	using Skyline.DataMiner.Net.Apps.UserDefinableApis.Actions;

	/// <summary>
	/// Represents a DataMiner Automation script.
	/// </summary>
	public class Script
	{
		private static readonly string hostFullName = Extensions.GetHostFullName();
		private static readonly HttpClient sharedClient = new HttpClient()
		{
			BaseAddress = new Uri($"https://{hostFullName}/data/"),
			Timeout = TimeSpan.FromMinutes(2),
		};

		/// <summary>
		/// The API trigger.
		/// </summary>
		/// <param name="engine">Link with SLAutomation process.</param>
		/// <param name="requestData">Holds the API request data.</param>
		/// <returns>An object with the script API output data.</returns>
		[AutomationEntryPoint(AutomationEntryPointType.Types.OnApiTrigger)]
		public ApiTriggerOutput OnApiTrigger(IEngine engine, ApiTriggerInput requestData)
		{
			HttpStatusCode responseStatusCode;
			string responseBody;

			try
			{
				var route = requestData.Route.TrimStart("data/");
				switch (route)
				{
					case "parameters":
						(responseStatusCode, responseBody) = PushDataToParameters(requestData);
						break;

					case "config":
						(responseStatusCode, responseBody) = PushDataToConfig(requestData);
						break;

					default:
						throw new NotSupportedException($"Route '{requestData.Route}' is not supported");
				}
			}
			catch (ArgumentException e)
			{
				responseBody = e.Message;
				responseStatusCode = HttpStatusCode.BadRequest;
			}
			catch (Exception e)
			{
				responseBody = e.Message;
				responseStatusCode = HttpStatusCode.InternalServerError;
			}

			return new ApiTriggerOutput
			{
				ResponseBody = responseBody,
				ResponseCode = (int)responseStatusCode,
			};
		}

		private static (HttpStatusCode statusCode, string response) PushDataToParameters(ApiTriggerInput requestData)
		{
			if (requestData.RequestMethod != RequestMethod.Put)
			{
				var exception = $"Received '{requestData.RequestMethod}' request for route '{requestData.Route}', request must use '{RequestMethod.Put}'";
				return (HttpStatusCode.MethodNotAllowed, exception);
			}

			if (!requestData.QueryParameters.TryGetValue("identifier", out string identifier) || !requestData.QueryParameters.TryGetValue("type", out string type))
			{
				var exception = $"Received {requestData.RequestMethod} request for route: '{requestData.Route}', 'identifier' and 'type' are mandatory {Environment.NewLine}{String.Join(", ", requestData.QueryParameters.GetAllKeys())}";
				throw new ArgumentException(exception);
			}

			return PushDataToLocalApi("api/data/parameters", new Dictionary<string, string>
			{
				["Identifier"] = identifier,
				["type"] = type,
			},
				requestData.RawBody);
		}

		private static (HttpStatusCode statusCode, string response) PushDataToConfig(ApiTriggerInput requestData)
		{
			if (requestData.RequestMethod != RequestMethod.Put)
			{
				var exception = $"Received '{requestData.RequestMethod}' request for route '{requestData.Route}', request must use '{RequestMethod.Put}'";
				return (HttpStatusCode.MethodNotAllowed, exception);
			}

			if (!requestData.QueryParameters.TryGetValue("type", out string type))
			{
				var exception = $"Received {requestData.RequestMethod} request for route: '{requestData.Route}', 'type' is mandatory {Environment.NewLine}{String.Join(", ", requestData.QueryParameters.GetAllKeys())}";
				throw new ArgumentException(exception);
			}

			return PushDataToLocalApi("api/config", new Dictionary<string, string>
			{
				["type"] = type,
			},
				requestData.RawBody);
		}

		private static (HttpStatusCode statusCode, string response) PushDataToLocalApi(string url, Dictionary<string, string> headers, string body)
		{
			// Create StringContent with your JSON data
			StringContent content = new StringContent(body, Encoding.UTF8, "application/json");

			// Add headers
			foreach (var header in headers)
			{
				content.Headers.Add(header.Key, header.Value);
			}

			// Execute PUT request on internal Data API
			var result = sharedClient.PutAsync(url, content).Result;
			string responseBody = result.Content.ReadAsStringAsync().Result;

			return (result.StatusCode, responseBody);
		}
	}

	internal static class Extensions
	{
		public static string TrimStart(this string original, string prefixToTrim, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
		{
			if (!original.StartsWith(prefixToTrim, comparisonType))
			{
				return original;
			}

			return original.Substring(prefixToTrim.Length);
		}

		public static string GetHostFullName()
		{
			string hostName = Dns.GetHostName();
			string domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;
			return $"{hostName}.{domainName}";
		}
	}
}