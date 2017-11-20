using System;
using System.Diagnostics;
using System.Net;


namespace CurrencyConverter
{
	internal static class UrlContext
	{
		internal class Format
		{
			private readonly DataFormat _dataFormat;

			private readonly string _url;

			public Format(string url)
			{
				this._url = url;
			}

			public Format(DataFormat dataFormat, string url) : this(url)
			{
				this._dataFormat = dataFormat;
			}
			public Action Json
			{
				get
				{
					return new Action(Method.GET, this._url, DataFormat.Json);
				}
			}

			public Action Xml
			{
				get
				{
					return new Action(Method.GET, this._url, DataFormat.Xml);
				}
			}
		}

		internal class Action
		{
			private readonly Method _method;
			private readonly string _url;
			private readonly DataFormat _dataFormat;


			public Action(string url, DataFormat dataFormat)
			{
				this._url = url;
				this._dataFormat = dataFormat;
			}

			public Action(Method method, string url, DataFormat format) : this(url, format)
			{
				this._method = method;
			}
			public Executor Get
			{
				get
				{
					return new Executor(this._url, this._dataFormat, Method.GET);
				}
			}


			public Executor Post(string value)
			{
				return new Executor(this._url, this._dataFormat, Method.POST, Option.Of(value));
			}

			public Executor Patch(string value)
			{
				return new Executor(this._url, this._dataFormat, Method.PATCH, Option.Of(value));
			}
		}

		internal class Executor
		{
			private readonly Method _method;
			private readonly string _url;
			private readonly DataFormat _dataFormat;
			private readonly Option<string> _body;

			/// <summary>
			/// Initializes a new instance of the <see cref="Executor"/> class.
			/// </summary>
			/// <param name="url">The URL.</param>
			/// <param name="format">The format.</param>
			/// <param name="mth">The MTH.</param>
			/// <exception cref="System.ArgumentNullException">url</exception>
			/// <exception cref="ArgumentNullException"><paramref name="url" /> is <see langword="null" />.</exception>
			public Executor([NotNull] string url, DataFormat format, Method mth)
			{
				if (url == null)
				{
					throw new ArgumentNullException("url");
				}
				this._url = url;
				this._dataFormat = format;
				this._method = mth;
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Executor"/> class.
			/// </summary>
			/// <param name="url">The URL.</param>
			/// <param name="format">The format.</param>
			/// <param name="mth">The MTH.</param>
			/// <param name="body">The body.</param>
			/// <exception cref="ArgumentNullException"><paramref name="url"/> is <see langword="null" />.</exception>
			public Executor([NotNull] string url, DataFormat format, Method mth, [NotNull] Option<string> body) : this(url, format, mth)
			{
				if (url == null)
				{
					throw new ArgumentNullException("url");
				}
				if (body == null)
				{
					throw new ArgumentNullException("body");
				}
				this._body = body;
			}

			/// <summary>
			///     Post
			/// </summary>
			/// <returns>
			///     Retourne le <see cref="HttpStatusCode" /> et une chaîne au format Json de la resource récupérée
			/// </returns>
			internal static readonly Func<DataFormat, Option<string>, string, Pair<HttpStatusCode, string>> Post = new Func<Method, DataFormat, Option<string>, string, Pair<HttpStatusCode, string>>((mth, format, value, url) => Action(mth, format, value, url)).Curry(Method.POST);

			/// <summary>
			///     Patch
			/// </summary>
			/// <returns>
			///     Retourne le <see cref="HttpStatusCode" /> et une chaîne au format Json de la resource récupérée
			/// </returns>
			internal static readonly Func<DataFormat, Option<string>, string, Pair<HttpStatusCode, string>> Patch = new Func<Method, DataFormat, Option<string>, string, Pair<HttpStatusCode, string>>((mth, format, value, url) => Action(mth, format, value, url)).Curry(Method.PATCH);

			/// <summary>
			/// Gets the specified URL.
			/// </summary>
			/// <param name="format">The format.</param>
			/// <param name="url">The URL.</param>
			/// <returns></returns>
			/// <exception cref="System.ArgumentNullException">url</exception>
			internal static Pair<HttpStatusCode, string> Get(DataFormat format, string url)
			{
				if (url == null)
				{
					throw new ArgumentNullException("url");
				}
				Trace.WriteLine(url);

				var client = new RestClient(url);
				var request = new RestRequest(Method.GET) { RequestFormat = format };
				var response = client.Execute(request);

				Trace.WriteLine(response.StatusDescription);
				return response.StatusCode == HttpStatusCode.OK ? Pair.Of(HttpStatusCode.OK, response.Content) : Pair.Of(response.StatusCode, response.Content);
			}

			/// <summary>
			/// Actions the specified MTH.
			/// </summary>
			/// <param name="mth">The MTH.</param>
			/// <param name="format">The format.</param>
			/// <param name="value">The value.</param>
			/// <param name="url">The URL.</param>
			/// <returns></returns>
			/// <exception cref="System.ArgumentNullException">url
			/// or
			/// value</exception>
			internal static Pair<HttpStatusCode, string> Action(Method mth, DataFormat format, [NotNull] Option<string> value, [NotNull] string url)
			{
				if (url == null)
				{
					throw new ArgumentNullException("url");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}


				Trace.WriteLine(url);
				Trace.WriteLine(value);

				var client = new RestClient(url);
				var request = new RestRequest(mth) { RequestFormat = format };
				if (value.HasValue)
				{
					// TODO : géré le cas Xml
					request.AddParameter("application/json; charset=utf-8", value.Value, ParameterType.RequestBody);
				}
				var response = client.Execute(request);

				Trace.WriteLine(response.Content);

				return Pair.Of(mth, response.StatusCode).Match()
					.With(x => x.First == Method.POST && x.Second == HttpStatusCode.Created, x => Pair.Of(HttpStatusCode.OK, response.Content))
					.With(x => x.First == Method.PATCH && x.Second == HttpStatusCode.OK, x => Pair.Of(HttpStatusCode.OK, response.Content))
					.Else(x => Pair.Of(response.StatusCode, response.Content))
					.Do();
			}

			/// <summary>
			/// Execution de la requête 
			/// </summary>
			/// <returns><see cref="Pair{T1,T2}"/></returns>
			public Pair<HttpStatusCode, string> Do()
			{
				return this._method.Match()
						.With(x => x == Method.GET, x => Get(this._dataFormat, this._url))
						.With(x => x == Method.POST, x => Post(this._dataFormat, this._body, this._url))
						.With(x => x == Method.PATCH, x => Patch(this._dataFormat, this._body, this._url))
						.Else(x => Pair.Of(HttpStatusCode.BadRequest, "Type d'opération non gérée !!!"))
						.Do();
			}
		}

		/// <summary>
		/// Initialisation d'une requête REST.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns><see cref="Format"/></returns>
		/// <exception cref="System.ArgumentNullException">value</exception>
		public static Format Rest(this string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return new Format(value);
		}	 }
}
