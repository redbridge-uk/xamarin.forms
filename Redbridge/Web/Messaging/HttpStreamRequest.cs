﻿using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Redbridge.Web.Messaging
{
	public class HttpStreamRequest : JsonWebRequestFunc<Stream>
	{
		public HttpStreamRequest(Uri baseUri, string requestUri, HttpVerb httpVerb, IHttpClientFactory clientFactory)
			: base(baseUri, requestUri, httpVerb, clientFactory) {}

		public HttpStreamRequest(string requestUri, HttpVerb httpVerb, IHttpClientFactory clientFactory)
			: base(requestUri, httpVerb, clientFactory) { }

		protected override async Task<Stream> OnReadResultBody(HttpResponseMessage responseMessage)
		{
			var stream = await responseMessage.Content.ReadAsStreamAsync();
			if (stream != null)
			{
				var memoryStream = new MemoryStream();
				await stream.CopyToAsync(memoryStream);
				return memoryStream;
			}

			return null;
		}
	}
}
