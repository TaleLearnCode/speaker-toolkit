using Microsoft.Azure.Functions.Worker.Http;
using System.Collections.Specialized;
using System.Reflection;
using System.Text.Json;
using System.Web;

namespace TaleLearnCode.SpeakerToolkit.Extensions;

public static class HttpRequestDataExtensions
{

	/// <summary>
	/// Creates a created (HTTP status code 201) response with the Content-Location header value.
	/// </summary>
	/// <param name="httpRequestData">The <see cref="HttpRequestData"/> for this response.</param>
	/// <param name="route">The routing information for the content-location.</param>
	/// <returns>A <see cref="HttpResponseData"/> with a status of Created (201) and a Content-Location header value.</returns>
	public static HttpResponseData CreatedResponse(this HttpRequestData httpRequestData, string route)
	{
		return httpRequestData.CreateCreatedResponse(GetContentLocation(route));
	}

	/// <summary>
	/// Gets the content location of the specified route.
	/// </summary>
	/// <param name="route">The route of the content location.</param>
	/// <returns>A <c>string</c> representing the content-location value.</returns>
	public static string GetContentLocation(this string route)
	{

		string httpProtocol;
		try
		{
			string? httpStatus = Environment.GetEnvironmentVariable("HTTPS");
			if (!string.IsNullOrWhiteSpace(httpStatus) && httpStatus.Equals("on", StringComparison.CurrentCultureIgnoreCase))
				httpProtocol = "https";
			else
				httpProtocol = "http";
		}
		catch
		{
			httpProtocol = "http";
		}

		if (route.StartsWith('/'))
			return $"{httpProtocol}://{Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME")}{route}";
		else
			return $"{httpProtocol}://{Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME")}/{route}";
	}

	/// <summary>
	/// Gets the flag of whether to create an object if it does not already exist from the query string.
	/// </summary>
	/// <returns><c>True</c> if the CreateIfNotExists query string value is present and set to true; otherwise, <c>false</c>.</returns>
	public static bool CreteIfNotExists(this HttpRequestData httpRequestData)
	{
		try
		{
			NameValueCollection queryValues = HttpUtility.ParseQueryString(httpRequestData.Url.Query);
			if (queryValues.HasKeys() && queryValues.AllKeys.Contains("CreateIfNotExists"))
				return (queryValues["CreteIfNotExists"]?.ToLowerInvariant() == "true");
			else
				return false;
		}
		catch (Exception)
		{
			return false;
		}
	}

	/// <summary>
	/// Creates a created (HTTP status code 201) response with appropriate header values.
	/// </summary>
	/// <param name="httpRequestData">The <see cref="HttpRequestData"/> to use for building the response object.</param>
	/// <param name="contentLocation">The location of the created object.</param>
	/// <param name="objectIdentifierName">Name of the type of object that was created.</param>
	/// <param name="objectIdentifierValue">Identifier of the created object.</param>
	/// <returns>A <see cref="HttpResponseData"/> representing the response to send back after an object has been created.</returns>
	public static HttpResponseData CreateCreatedResponse(this HttpRequestData httpRequestData, string contentLocation, string objectIdentifierName, string objectIdentifierValue)
	{
		HttpResponseData response = httpRequestData.CreateCreatedResponse(contentLocation);
		response.Headers.Add(objectIdentifierName, objectIdentifierValue);
		return response;
	}

	/// <summary>
	/// Gets the value of the specified request query string element.
	/// </summary>
	/// <param name="httpRequestData">The <see cref="HttpRequestData"/> containing the request information.</param>
	/// <param name="key">The key of the query string element to get.</param>
	/// <returns>A <c>string></c> representing the value of the specified query string element.</returns>
	public static string? GetQueryStringValue(this HttpRequestData httpRequestData, string key)
	{
		if (TryGetQueryStringValue(httpRequestData, key, out var queryStringValue))
			return queryStringValue;
		else
			return default;
	}

	/// <summary>
	/// Attempts to get the value of a specified request query string element.
	/// </summary>
	/// <param name="httpRequestData">The <see cref="HttpRequestData"/> containing the request information.</param>
	/// <param name="key">The key of the query string element to attempt to get.</param>
	/// <param name="value">The resulting value of the attempted query string element.</param>
	/// <returns><c>True</c> if the specified query string value is returned.</returns>
	public static bool TryGetQueryStringValue(this HttpRequestData httpRequestData, string key, out string? value)
	{
		value = null;
		key = key.ToLowerInvariant();
		if (TryGetQueryStringValues(httpRequestData, out Dictionary<string, string> values) && values.TryGetValue(key, out value))
			return value != null;
		else
			return false;
	}

	/// <summary>
	/// Attempts to get the values from the requests' query string.
	/// </summary>
	/// <param name="httpRequestData">The <see cref="HttpRequestData"/> containing the request information.</param>
	/// <param name="queryStringValues">The resulting dictionary of query string values.</param>
	/// <returns><c>True</c> if the query string values were returned (they were present); otherwise, <c>false</c>.</returns>
	public static bool TryGetQueryStringValues(this HttpRequestData httpRequestData, out Dictionary<string, string> queryStringValues)
	{
		queryStringValues = GetQueryStringValues(httpRequestData);
		return queryStringValues != null;
	}

	/// <summary>
	/// Gets the values from the request's query string.
	/// </summary>
	/// <param name="httpRequestData">The <see cref="HttpRequestData"/> containing the request information.</param>
	/// <returns><A <see cref="Dictionary{string, string}"/> representing the query string values for the value.</returns>
	public static Dictionary<string, string> GetQueryStringValues(this HttpRequestData httpRequestData)
	{
		if (!string.IsNullOrWhiteSpace(httpRequestData.Url.Query))
		{
			NameValueCollection queryValues = HttpUtility.ParseQueryString(httpRequestData.Url.Query);
			if (queryValues.Count >= 1)
			{
				Dictionary<string, string> result = [];
				foreach (string key in queryValues.Keys)
				{
					string? queryStringValue = queryValues[key];
					if (!string.IsNullOrWhiteSpace(queryStringValue))
						result.TryAdd(key.ToLowerInvariant(), queryStringValue);
				}
				return result;
			}
			else
				return [];
		}
		else
			return [];
	}

	public static bool GetBooleanQueryStringValue(this HttpRequestData httpRequestData, string key, bool defaultValue)
	{
		string? queryStringValue = httpRequestData.GetQueryStringValue(key);
		if (queryStringValue is not null)
			if (queryStringValue.Equals("true", StringComparison.InvariantCultureIgnoreCase))
				return true;
			else if (queryStringValue.Equals("false", StringComparison.InvariantCultureIgnoreCase))
				return false;
			else
				return defaultValue;
		else
			return defaultValue;
	}

	public static int GetInt32QueryStringValue(this HttpRequestData httpRequestData, string key, int defaultValue = 0)
		=> int.TryParse(httpRequestData.GetQueryStringValue(key), out int result) ? result : defaultValue;

	public static async Task<T?> GetRequestParameters2Async<T>(this HttpRequestData httpRequestData, Dictionary<string, string> routeValues, JsonSerializerOptions jsonSerializerOptions) where T : new()
	{

		Type? underlyingType = Nullable.GetUnderlyingType(typeof(T));
		bool isNullable = underlyingType == null;

		string queryString = httpRequestData.Url.Query;
		NameValueCollection queryValues = HttpUtility.ParseQueryString(queryString);
		bool queryValuesAvailable = (queryValues.Count == 1 && queryValues.GetKey(0) != null && queryValues?.GetKey(0)?.ToLower() != "code") || queryValues.Count > 1;
		if (httpRequestData.Body == Stream.Null && !queryValuesAvailable && (routeValues == null || routeValues.Count == 0))
		{
			if (isNullable)
				return default;
			else
				throw new HttpRequestDataException("There are no query string values, no route values, or the request body is missing or it is unreadable.");
		}

		T? requestObject = default;
		if (httpRequestData.Body != Stream.Null)
		{
			requestObject = await JsonSerializer.DeserializeAsync<T>(httpRequestData.Body, jsonSerializerOptions);
			if (requestObject == null)
			{
				if (isNullable)
					return default;
				else
					throw new HttpRequestDataException("The request body is not correctly formatted.");
			}
		}

		Dictionary<string, string>? loweredRouteValues = null;
		if (routeValues?.Count > 0)
		{
			loweredRouteValues = [];
			foreach (KeyValuePair<string, string> routeValue2 in routeValues)
			{
				loweredRouteValues.Add(routeValue2.Key.ToLower(), routeValue2.Value);
			}
		}

		if (queryValuesAvailable || (loweredRouteValues?.Count > 0))
		{
			PropertyInfo[] properties = typeof(T).GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (!propertyInfo.CanWrite)
				{
					continue;
				}

				if (queryValuesAvailable)
				{
					string[]? allKeys = queryValues.AllKeys;
					foreach (string key in allKeys)
					{
						if (key.Equals(propertyInfo.Name, StringComparison.CurrentCultureIgnoreCase))
						{
							requestObject ??= new T();
							if (propertyInfo.PropertyType == typeof(int) || propertyInfo.PropertyType == typeof(int?))
								propertyInfo.SetValue(requestObject, Convert.ToInt32(queryValues[key]));
							else if (propertyInfo.PropertyType == typeof(bool) || propertyInfo.PropertyType == typeof(bool?))
								propertyInfo.SetValue(requestObject, Convert.ToBoolean(queryValues[key]));
							else
								propertyInfo.SetValue(requestObject, queryValues[key]);
						}
					}
				}

				if (loweredRouteValues != null && loweredRouteValues.Count != 0 && loweredRouteValues.TryGetValue(propertyInfo.Name.ToLower(), out var routeValue))
				{
					requestObject ??= new T();
					if (propertyInfo.PropertyType == typeof(int) || propertyInfo.PropertyType == typeof(int?))
						propertyInfo.SetValue(requestObject, Convert.ToInt32(routeValue));
					else if (propertyInfo.PropertyType == typeof(bool) || propertyInfo.PropertyType == typeof(bool?))
						propertyInfo.SetValue(requestObject, Convert.ToBoolean(routeValue));
					else
						propertyInfo.SetValue(requestObject, routeValue);
				}

				routeValue = null;
			}
		}

		if (requestObject == null)
		{
			if (isNullable)
				return default;
			else
				throw new HttpRequestDataException("The request body is not correctly formatted.");
		}

		return requestObject;
	}

	public static async Task<T?> GetRequestParameters2Async<T>(this HttpRequestData httpRequestData, JsonSerializerOptions jsonSerializerOptions) where T : new()
		=> await httpRequestData.GetRequestParameters2Async<T>([], jsonSerializerOptions);

	public static async Task<T?> GetRequestParameters2Async<T>(this HttpRequestData httpRequestData) where T : new()
		=> await httpRequestData.GetRequestParameters2Async<T>([], new());

}