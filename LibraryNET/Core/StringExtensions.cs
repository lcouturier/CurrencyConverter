
namespace CurrencyConverter.Library.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    /// <summary>
		/// M�thodes d'extensions des chaines de caract�res
		/// </summary>
		public static class StringExtensions
		{
			#region Public Methods

			/// <summary>
			/// Determines whether the specified value is base64.
			/// </summary>
			/// <param name="value">The value.</param>
			/// <returns></returns>
			/// <exception cref="ArgumentNullException"><paramref name="input" /> ou <paramref name="pattern" /> est null.</exception>
			/// <exception cref="ArgumentException">Une erreur d'analyse d'expression r�guli�re s'est produite.</exception>
			public static bool IsBase64(this string value)
			{
				Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(value));

				return Regex.IsMatch(value, "^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$");
			}

			/// <summary>
			/// Try the parse base64.
			/// </summary>
			/// <param name="value">The value.</param>
			/// <returns></returns>
			/// <remarks>
			/// Retourne Option.None si le param�tre est nnull ou vide
			/// </remarks>
			public static Option<byte[]> TryParseBase64(this string value)
			{
				return TryParse(value, x => x.IsBase64(), Convert.FromBase64String);
			}

			/// <summary>
			/// Tries the parse.
			/// </summary>
			/// <typeparam name="T"></typeparam>
			/// <param name="value">The value.</param>
			/// <param name="valid">The valid.</param>
			/// <param name="convert">The convert.</param>
			/// <returns></returns>
			[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", MessageId = "1")]
			public static Option<T> TryParse<T>(this string value, Predicate<string> valid, Func<string, T> convert)
			{
				Contract.Requires<ArgumentNullException>(valid != null);
				Contract.Requires<ArgumentNullException>(convert != null);
				Contract.Ensures(Contract.Result<Option<T>>() != null);

				return string.IsNullOrEmpty(value) ? Option.None : valid(value) ? Option.Of(convert(value)) : Option.None;
			}

			/// <summary>
			/// Lecture d'un fichier Texte.
			/// </summary>
			/// <param name="fileName">Name of the file.</param>
			/// <returns></returns>
			/// <exception cref="FileNotFoundException">Le fichier est introuvable. </exception>
			/// <exception cref="DirectoryNotFoundException">Le chemin d'acc�s sp�cifi� est non valide, il se trouve par exemple sur un lecteur non mapp�. </exception>
			/// <exception cref="IOException"><paramref name="path" /> comprend une syntaxe incorrecte ou non valide pour les noms de fichiers, les noms de r�pertoires ou les noms de volumes. </exception>
			/// <exception cref="OutOfMemoryException">La m�moire disponible est insuffisante pour allouer une m�moire tampon pour la cha�ne retourn�e. </exception>
			public static IEnumerable<string> ReadAllLines(this string fileName)
			{
				Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(fileName));
				Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);

				using (var sr = new StreamReader(fileName))
				{
					while (!sr.EndOfStream)
					{
						yield return sr.ReadLine();
					}
				}
			}

			/// <summary>
			/// Equalses the specified left.
			/// </summary>
			/// <param name="left">The left.</param>
			/// <param name="right">The right.</param>
			/// <returns></returns>
			public static bool IsEqualTo(this string left, string right)
			{
				Contract.Requires<ArgumentNullException>(right != null);
				Contract.Requires<ArgumentNullException>(right != null);

				return string.Compare(left, right, StringComparison.OrdinalIgnoreCase) == 0;
			}

			/// <summary>
			///   Deserialise la valeur en param�tre
			/// </summary>
			/// <typeparam name="T"> Type <see cref="T" /> </typeparam>
			/// <param name="value"> Valeur � s�serialiser </param>
			/// <returns> Valeur d�serialis�e </returns>
			/// <exception cref="System.InvalidOperationException">Une erreur s'est produite lors de la d�s�rialisation.L'exception d'origine est disponible via l'utilisation de la propri�t� <see cref="P:System.Exception.InnerException" />.</exception>
			/// <exception cref="Exception">A delegate callback throws an exception. </exception>
			/// <exception cref="ArgumentNullException">Le param�tre <paramref name="s" /> est null. </exception>
			[SuppressMessage(
				"Microsoft.Reliability",
				"CA2000:Supprimer les objets avant la mise hors de port�e",
				Justification = "Impl�menter dans la m�thode Use")]
			public static T Deserialize<T>(this string value)
				where T : class
			{
				Contract.Requires<ArgumentNullException>(value != null);
				Contract.Ensures(Contract.Result<T>() != null);

				return new StringReader(value).Use(sr => new XmlSerializer(typeof(T)).Deserialize(sr) as T);
			}

			/// <exception cref="ArgumentNullException">Exception lev�e lorsque l'argument <paramref name="function" /> a la valeur Null.</exception>
			/// <exception cref="InvalidOperationException">Une erreur s'est produite lors de la d�s�rialisation.L'exception d'origine est disponible via l'utilisation de la propri�t� <see cref="P:System.Exception.InnerException" />.</exception>
			public static Task<T> DeserializeAsync<T>(this string value)
				where T : class
			{
				return Task.Factory.StartNew(() => Deserialize<T>(value));
			}

			[SuppressMessage(
				"Microsoft.Reliability",
				"CA2000:Supprimer les objets avant la mise hors de port�e",
				Justification = "Impl�menter dans la m�thode Use")]
			public static T DeserializeJson<T>(this string value)
				where T : class
			{
				Contract.Requires<ArgumentNullException>(value != null);
				Contract.Ensures(Contract.Result<T>() != null);

				return new StringReader(value).Use(sr => new JsonSerializer().Deserialize(sr, typeof(T))) as T;
			}

			/// <summary>
			/// Deserializes the json.
			/// </summary>
			/// <typeparam name="T"></typeparam>
			/// <param name="value">The value.</param>
			/// <param name="obj">The object.</param>
			/// <returns></returns>
			/// <example>
			/// <code>
			/// <![CDATA[
			///    var value = result.DeserializeJson(new { Code = 0, Message = string.Empty });
			/// ]]>
			/// </code>
			/// </example>
			public static T DeserializeJson<T>(this string value, T obj) where T : class
			{
				Contract.Requires<ArgumentNullException>(value != null);
				Contract.Requires<ArgumentNullException>(obj != null);
				Contract.Ensures(Contract.Result<T>() != null);

				return new StringReader(value).Use((sr) => new JsonSerializer().Deserialize(sr, obj.GetType())) as T;
			}

			public static Task<T> DeserializeJsonAsync<T>(this string value)
				where T : class
			{
				return Task.Factory.StartNew(() => DeserializeJson<T>(value));
			}

			public static Task<string> SerializeJsonAsync<T>(this T obj)
				where T : class
			{
				return Task.Factory.StartNew(() => SerializeJson<T>(obj));
			}

			[SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")]
			public static string SerializeJson<T>(this T obj)
				where T : class
			{
				Contract.Requires<ArgumentNullException>(obj != null);
				Contract.Ensures(Contract.Result<string>() != null);

				return new StringWriter().Use(
					sw =>
					{
						new JsonSerializer().Serialize(sw, obj);
						return sw.ToString();
					});
			}

			/// <summary>
			/// S�rialization d'une classe en cha�ne Xml
			/// </summary>
			/// <typeparam name="T">Type � s�rializer</typeparam>
			/// <param name="obj">The <paramref name="obj"/>.</param>
			/// <returns>Retourne une chaine</returns>
			[SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")]
			public static string Serialize<T>(this T obj)
				where T : class
			{
				Contract.Requires<ArgumentNullException>(obj != null);
				Contract.Ensures(Contract.Result<string>() != null);

				return new Utf8StringWriter().Use(
					sw =>
					{
						new XmlSerializer(obj.GetType()).Serialize(sw, obj);
						return sw.ToString();
					});
			}

			/// <summary>
			/// S�rialization d'une classe en cha�ne Xml
			/// </summary>
			/// <typeparam name="T">Type � s�rializer</typeparam>
			/// <param name="obj">The <paramref name="obj"/>.</param>
			/// <returns>Retourne une chaine</returns>
			[SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")]
			public static string SerializeUtf16<T>(this T obj)
				where T : class
			{
				Contract.Requires<ArgumentNullException>(obj != null);
				Contract.Ensures(Contract.Result<string>() != null);

				return new StringWriter().Use(
					sw =>
					{
						new XmlSerializer(obj.GetType()).Serialize(sw, obj);
						return sw.ToString();
					});
			}

			/// <summary>
			/// Serializes the asynchronous.
			/// </summary>
			/// <typeparam name="T"></typeparam>
			/// <param name="obj">The object.</param>
			/// <returns></returns>
			/// <exception cref="ArgumentNullException">Exception lev�e lorsque l'argument <paramref name="function" /> a la valeur Null.</exception>
			public static Task<string> SerializeAsync<T>(this T obj)
				where T : class
			{
				return Task.Factory.StartNew(() => Serialize(obj));
			}

			/// <summary>
			/// S�rialization d'une classe en cha�ne Xml
			/// </summary>
			/// <typeparam name="T">Type � s�rializer</typeparam>
			/// <param name="obj">The <paramref name="obj" />.</param>
			/// <param name="ns">Liste des namespaces.</param>
			/// <returns>
			/// Retourne une chaine
			/// </returns>
			/// <exception cref="System.InvalidOperationException">Une erreur s'est produite lors de la s�rialisation.L'exception d'origine est disponible via l'utilisation de la propri�t� <see cref="P:System.Exception.InnerException" />.</exception>
			/// <exception cref="Exception">A delegate callback throws an exception. </exception>
			[SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e")]
			public static string Serialize<T>(this T obj, XmlSerializerNamespaces ns)
				where T : class
			{
				Contract.Requires<ArgumentNullException>(obj != null, "l'objet est null");
				Contract.Requires<ArgumentNullException>(ns != null, "l'objet XmlSerializerNamespaces est null");
				Contract.Ensures(Contract.Result<string>() != null);

				return new Utf8StringWriter().Use(
					sw =>
					{
						new XmlSerializer(obj.GetType()).Serialize(sw, obj, ns);
						return sw.ToString();
					});
			}

			/// <summary>
			///   Determines whether [is null or empty] [the specified value].
			/// </summary>
			/// <param name="item"> The value. </param>
			/// <returns> <c>true</c> if [is null or empty] [the specified value]; otherwise, <c>false</c> . </returns>
			public static bool IsNullOrEmpty(this string item)
			{
				return string.IsNullOrEmpty(item);
			}

			public static string IsNullOrEmpty(this string obj, Func<string> orElse)
			{
				Contract.Requires<ArgumentNullException>(orElse != null);
				Contract.Ensures(Contract.Result<string>() != null);

				return string.IsNullOrEmpty(obj) ? orElse() : obj;
			}

			/// <summary>
			///   Determines whether [is null or white space] [the specified value].
			/// </summary>
			/// <param name="item"> The value. </param>
			/// <returns> <c>true</c> if [is null or white space] [the specified value]; otherwise, <c>false</c> . </returns>
			public static bool IsNullOrWhiteSpace(this string item)
			{
				return string.IsNullOrWhiteSpace(item);
			}

			public static string IsNullOrWhiteSpace(this string obj, Func<string> orElse)
			{
				Contract.Requires<ArgumentNullException>(orElse != null);
				Contract.Ensures(Contract.Result<string>() != null);

				return string.IsNullOrWhiteSpace(obj) ? orElse() : obj;
			}

			/// <summary>
			/// Lefts the specified value.
			/// </summary>
			/// <param name="value">The value.</param>
			/// <param name="length">The length.</param>
			/// <returns></returns>
			/// <exception cref="ArgumentOutOfRangeException"></exception>
			[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", MessageId = "0")]
			public static string Left(this string value, int length)
			{
				Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(value));
				Contract.Requires<ArgumentOutOfRangeException>(length > 0);
				Contract.Requires<ArgumentOutOfRangeException>(length < value.Length);
				Contract.Requires<ArgumentOutOfRangeException>(0 <= (value.Length - length));
				Contract.Ensures(Contract.Result<string>() != null);

				return value.Substring(0, length);
			}

			/// <summary>
			/// Parses the specified value.
			/// </summary>
			/// <typeparam name="T"></typeparam>
			/// <param name="value">The value.</param>
			/// <returns></returns>
			/// <exception cref="System.InvalidOperationException"></exception>
			/// <exception cref="InvalidOperationException">Conversion impossible</exception>
			/// <exception cref="System.NotSupportedException">Impossible de convertir la cha�ne vers l'objet appropri�.</exception>        
			/// <exception cref="ArgumentNullException"><paramref name="component" /> est null. </exception>
			[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", MessageId = "0")]
			public static T Parse<T>(this string value)
			{
				Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(value));

				var tc = TypeDescriptor.GetConverter(typeof(T));
				if (!tc.CanConvertFrom(typeof(string)))
				{
					throw new InvalidOperationException(string.Format("Conversion de la valeur {0} impossible", value));
				}

				var result = (T)tc.ConvertFromString(value.Trim());
				return result;
			}

			/// <summary>
			/// Parse une valeur, si celui �choue alors on retourne la valeur de la function orElse
			/// </summary>
			/// <typeparam name="T"></typeparam>
			/// <param name="value">Valeur � pars�e.</param>
			/// <param name="orElse">Valeur a retourn�e dans le cas ou le parse �choue.</param>
			/// <returns>Valeur Pars�e</returns>
			public static T Parse<T>(this string value, Func<T> orElse)
			{
				Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(value));
				Contract.Requires<ArgumentNullException>(orElse != null);

				var parse = Fun.Create(() => value.Parse<T>()).OnExceptionEither();						
				var resultOfParse = parse();
				return resultOfParse.GetOrElse(orElse);
			}

			/// <summary>
			/// Regexes the replace.
			/// </summary>
			/// <param name="value">The value.</param>
			/// <param name="expression">The expression.</param>
			/// <returns></returns>
			/// <exception cref="System.ArgumentException">Une erreur d'analyse d'expression r�guli�re s'est produite.</exception>
			/// <exception cref="System.ArgumentNullException">input, pattern ou replacement est null.</exception>
			[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", MessageId = "0")]
			public static string RegexReplace(this string value, string expression)
			{
				Contract.Requires<ArgumentNullException>(value != null);
				Contract.Requires<ArgumentNullException>(expression != null);
				Contract.Ensures(Contract.Result<string>() != null);

				return Regex.Replace(value, expression, string.Empty);
			}

			/// <summary>
			/// Removes the accented chars.
			/// </summary>
			/// <param name="value">The value.</param>
			/// <returns></returns>
			/// <exception cref="System.ArgumentNullException">s est null.</exception>
			/// <exception cref="System.Text.EncoderFallbackException">Un secours s'est produit (consultez Fonctionnement des encodages pour obtenir une explication compl�te)� et �<see cref="P:System.Text.Encoding.EncoderFallback" /> a la valeur <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
			/// <exception cref="System.ArgumentException">Le tableau d'octets contient des points de code Unicode non valides.</exception>
			/// <exception cref="System.Text.DecoderFallbackException">Un secours s'est produit (consultez Fonctionnement des encodages pour obtenir une explication compl�te)� et �<see cref="P:System.Text.Encoding.DecoderFallback" /> a la valeur <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
			[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", MessageId = "0")]
			public static string RemoveAccentedChars(this string value)
			{
				Contract.Requires<ArgumentNullException>(value != null);
				Contract.Ensures(Contract.Result<String>() != null);

				return Encoding.ASCII.GetString(Encoding.GetEncoding(1251).GetBytes(value));
			}

			/// <summary>
			/// Removes the HTML tags.
			/// </summary>
			/// <param name="value">The value.</param>
			/// <returns></returns>
			/// <exception cref="System.ArgumentException">Une erreur d'analyse d'expression r�guli�re s'est produite.</exception>
			/// <exception cref="System.ArgumentNullException">input, pattern ou replacement est null.</exception>
			[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", MessageId = "0")]
			public static string RemoveHtmlTags(this string value)
			{
				Contract.Requires<ArgumentNullException>(value != null);
				Contract.Ensures(Contract.Result<string>() != null);

				return value.RegexReplace(@"<style>(.|\n)*?</style>").RegexReplace(@"<xml>(.|\n)*?</xml>")
					.RegexReplace(@"<(.|\n)*?>");
			}

			/// <summary>
			/// Rights the specified value.
			/// </summary>
			/// <param name="value">The value.</param>
			/// <param name="length">The length.</param>
			/// <returns></returns>
			/// <exception cref="System.ArgumentOutOfRangeException">startIndex plus length indique une position qui n'est pas dans l'instance.��ou�� startIndex ou length est inf�rieur � z�ro.</exception>
			[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", MessageId = "0")]
			public static string Right(this string value, int length)
			{
				Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(value));
				Contract.Requires<ArgumentOutOfRangeException>(length > 0);
				Contract.Requires<ArgumentOutOfRangeException>(length < value.Length);
				Contract.Requires<ArgumentOutOfRangeException>(0 <= (value.Length - length));
				Contract.Ensures(Contract.Result<string>() != null);

				return value.Substring(value.Length - length, length);
			}

			/// <summary>
			/// Parse d'une chaine de caract�re
			/// </summary>
			/// <typeparam name="T"></typeparam>
			/// <param name="value">chaine � parser</param>
			/// <returns></returns>
			/// <exception cref="ArgumentNullException"><paramref name="value" />is
			/// <c>null</c>.</exception>
			/// <exception cref="System.NotSupportedException">Impossible de
			/// convertir la cha�ne vers l'objet appropri�.</exception>
			/// <example>
			///   <code>
			///      <![CDATA[
			///         var result = "123.02".TryParse<Single>();
			///      ]]>
			///   </code>
			/// </example>
			/// <exception cref="Exception">A delegate callback throws an exception.</exception>
			[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", MessageId = "0")]
			public static Option<T> TryParse<T>(this string value)
				where T : struct
			{
				Contract.Ensures(Contract.Result<Option<T>>() != null);

				if (string.IsNullOrWhiteSpace(value))
				{
					return Option.None;
				}

				bool isTypeDate = (typeof(T) == typeof(DateTime));

				Func<TypeConverter> getConverter = () =>
					isTypeDate ? new DateTimeConverter() : TypeDescriptor.GetConverter(typeof(T));
				var converter = getConverter();
				Contract.Assert(converter != null);

				bool isValid = isTypeDate || converter.CanConvertFrom(typeof(string));

				Func<string, Option<T>> convert = x => Option.Of((T)converter.ConvertFromString(x));
				var f = convert.OnExceptionNone();

				return isValid ? f(value) : Option.None;
			}

			/// <summary>
			/// Called when [exception none].
			/// </summary>
			/// <typeparam name="T"></typeparam>
			/// <param name="operation">The operation.</param>
			/// <returns></returns>
			internal static Func<string, Option<T>> OnExceptionNone<T>(this Func<string, Option<T>> operation)
			{
				return args =>
				{
					try
					{
						return operation(args);
					}
					catch (Exception)
					{
						return Option.None;
					}
				};
			}

			/// <summary>
			/// Validation d'une chaine Xml via un sch�ma
			/// </summary>
			/// <param name="value">chaine Xml</param>
			/// <param name="schema">The schema.</param>
			/// <returns></returns>
			/// <exception cref="System.ArgumentNullException">value est null.</exception>
			/// <exception cref="System.ArgumentNullException">value est null.</exception>
			/// <exception cref="System.Xml.Schema.XmlSchemaException">Le sch�ma
			/// n'est pas valide.</exception>
			/// <example>
			///   <code><![CDATA[
			/// var value = new MyClass().Serialize();
			/// var result = value.IsValid("Collaborateurs.xsd");
			/// ]]></code>
			/// </example>
			[SuppressMessage("Microsoft.Reliability", "CA2000:Supprimer les objets avant la mise hors de port�e"),
			 SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
			public static bool IsValidSchema(this string value, string schema)
			{
				Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(value));
				Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(schema));

				return new StringReader(value).Use(
					sr =>
					{
						var settings = new XmlReaderSettings();
						settings.Schemas.Add(null, schema);
						settings.ValidationType = ValidationType.Schema;
						return XmlReader.Create(sr, settings).Use(
							x =>
							{
								bool isFailed = false;
								try
								{
									while (x.Read())
									{
									}
								}
								catch (Exception)
								{
									isFailed = true;
								}
								return !isFailed;
							});
					});
			}

			/// <summary>
			/// Determines whether the specified value is valid.
			/// </summary>
			/// <param name="value">The value.</param>
			/// <param name="pattern">The pattern.</param>
			/// <returns>
			///   <c>true</c> if the specified value is valid; otherwise, <c>false</c>.
			/// </returns>
			/// <exception cref="System.ArgumentException">Une erreur d'analyse d'expression r�guli�re s'est produite.</exception>
			/// <exception cref="System.ArgumentNullException">input ou pattern est null.</exception>
			/// <exception cref="System.ArgumentOutOfRangeException">options n'est pas une valeur <see cref="T:System.Text.RegularExpressions.RegexOptions" /> valide.</exception>
			/// <example>
			///   <code><![CDATA[
			/// const string Pattern = "^([a-zA-Z0-9_\\-])+(\\.([a-zA-Z0-9_\\-])+)*@((\\[(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5])))\\.(((([0-1])?([0-9])?[0-9])|(2[0-4][0-9])|(2[0-5][0-5]))\\]))|((([a-zA-Z0-9])+(([\\-])+([a-zA-Z0-9])+)*\\.)+([a-zA-Z])+(([\\-])+([a-zA-Z0-9])+)*))$";
			/// bool result = value.IsValid(Pattern);
			/// ]]></code>
			/// </example>
			[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", MessageId = "0")]
			public static bool IsValid(this string value, string pattern)
			{
				Contract.Requires<ArgumentNullException>(value != null);
				Contract.Requires<ArgumentNullException>(pattern != null);

				return Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase);
			}

			/// <summary>
			/// Validation du format d'une addresse Email
			/// </summary>
			/// <param name="value">Cha�ne � valider.</param>
			/// <returns>
			///   <c>true</c> si l'addresse Email est valide true; sinon, <c>false</c>.
			/// </returns>
			/// <exception cref="System.ArgumentException">Une erreur d'analyse d'expression r�guli�re s'est produite.</exception>
			/// <exception cref="System.ArgumentNullException">input ou pattern est null.</exception>
			/// <exception cref="System.ArgumentOutOfRangeException">options n'est pas une valeur <see cref="T:System.Text.RegularExpressions.RegexOptions" /> valide.</exception>
			[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", MessageId = "0")]
			public static bool IsValidEmailAddress(this string value)
			{
				Contract.Requires<ArgumentNullException>(value != null);

				return Regex.IsMatch(
					value,
					@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
					RegexOptions.IgnoreCase);
			}



			/// <summary>
			/// Converts to.
			/// </summary>
			/// <typeparam name="T"></typeparam>
			/// <param name="value">The value.</param>
			/// <returns></returns>
			/// <exception cref="InvalidCastException">Cette conversion n'est pas prise en charge.  ��ou��<paramref name="value" /> est null et <paramref name="typeCode" /> sp�cifie un type valeur.��ou��<paramref name="value" /> n'impl�mente pas l'interface <see cref="T:System.IConvertible" />.</exception>
			/// <exception cref="OverflowException"><paramref name="value" /> repr�sente un nombre qui est hors de la plage du type <paramref name="typeCode" />.</exception>
			/// <exception cref="FormatException"><paramref name="value" /> n'est pas dans un format reconnu par le type <paramref name="typeCode" />.</exception>
			/// <exception cref="ArgumentException"><paramref name="typeCode" /> n'est pas valide. </exception>
			internal static T ConvertTo<T>(object value)
			{
				Contract.Requires<ArgumentNullException>(value != null);

				var tc = Type.GetTypeCode(typeof(T));
				return (T)Convert.ChangeType(value, tc);
			}

			/// <summary>
			/// Extrait une chaine d'une position de d�but � une position de fin
			/// </summary>
			/// <param name="value">Chaine.</param>
			/// <param name="start">Position de d�but.</param>
			/// <param name="end">Position de fin.</param>
			/// <returns>Chaine extraite</returns>
			/// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex" /> plus <paramref name="length" /> indique une position qui n'est pas dans l'instance.��ou�� <paramref name="startIndex" /> ou <paramref name="length" /> est inf�rieur � z�ro. </exception>
			[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", MessageId = "0")]
			public static string SubstringFromStartToEnd(this string value, int start, int end)
			{
				Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(value));
				Contract.Requires<ArgumentOutOfRangeException>(start > -1);
				Contract.Requires<ArgumentOutOfRangeException>(end > -1);
				Contract.Requires<ArgumentOutOfRangeException>(end < value.Length);
				Contract.Requires<ArgumentOutOfRangeException>(start < end - 1);
				Contract.Ensures(Contract.Result<string>() != null);

				return value.Substring(start, end - start + 1);
			}

			/// <summary>
			/// Extrait des sous-chaines � partir d'une expression r�guli�re.
			/// </summary>
			/// <param name="value">Valeur trait�e.</param>
			/// <param name="pattern">Expression R�guli�re.</param>
			/// <returns></returns>
			/// <example>
			/// <code>
			/// <![CDATA[
			///    var result = "{CustomerCode} Equals 42 and {voucher} Equals 1".Extract("{(.*?)}");
			/// ]]>
			/// </code>
			/// </example>
			public static IEnumerable<string> Extract(this string value, string pattern)
			{
				Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(value));
				Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(pattern));
				Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);

				return from Match item in Regex.Matches(value, pattern, RegexOptions.IgnoreCase) select item.Value;
			}

			/// <summary>
			/// Heads the specified value.
			/// </summary>
			/// <param name="value">The value.</param>
			/// <returns></returns>
			internal static string Head(this string value)
			{
				Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(value));
				Contract.Requires<ArgumentOutOfRangeException>(value.Length > 0);
				Contract.Ensures(Contract.Result<string>() != null);

				return value.Substring(0, 1);
			}

			/// <summary>
			/// Tails the specified value.
			/// </summary>
			/// <param name="value">The value.</param>
			/// <returns></returns>
			internal static string Tail(this string value)
			{
				Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(value));
				Contract.Requires<ArgumentOutOfRangeException>(value.Length > 0);
				Contract.Ensures(Contract.Result<string>() != null);

				return value.Substring(1, value.Length - 1);
			}

			/// <summary>
			/// Reverses the specified value.
			/// </summary>
			/// <param name="value">The value.</param>
			/// <returns></returns>
			[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", MessageId = "0")]
			public static string Reverse(this string value)
			{
				Contract.Requires<ArgumentNullException>(value != null);
				Contract.Ensures(Contract.Result<string>() != null);

				return value.Length == 0 ? value : string.Concat(Reverse(value.Tail()), value.Head());
			}

			/// <summary>
			/// To the stream.
			/// </summary>
			/// <param name="str">The string.</param>
			/// <returns></returns>
			public static Stream ToStream(this string str)
			{
				Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(str));
				Contract.Ensures(Contract.Result<Stream>() != null);

				byte[] byteArray = Encoding.UTF8.GetBytes(str);
				return new MemoryStream(byteArray);
			}

			/// <summary>
			/// Supprime le premier et dernier caract�re d'une chaine
			/// </summary>
			/// <param name="value">Valeur.</param>
			/// <returns>Retourne la chaine sans le premier et dernier caract�re d'une chaine</returns>
			public static string Peel(this string value)
			{
				return Peel(value, 1);
			}

			/// <summary>
			/// Peels the specified value.
			/// </summary>
			/// <param name="value">The value.</param>
			/// <param name="length">The length.</param>
			/// <returns></returns>
			[SuppressMessage("Microsoft.Design", "CA1062:Valider les arguments de m�thodes publiques", MessageId = "0")]
			public static string Peel(this string value, int length)
			{
				Contract.Requires<ArgumentNullException>(value != null);
				Contract.Requires<ArgumentOutOfRangeException>(value.Length >= (length * 2) + 1);
				Contract.Requires<ArgumentOutOfRangeException>(length > 0);
				Contract.Ensures(Contract.Result<string>() != null);

				return value.Substring(length, (value.Length - (length * 2)));
			}

			#endregion Public Methods

			/// <summary>
			/// Removes the last character.
			/// </summary>
			/// <param name="value">The value.</param>
			/// <returns></returns>
			public static string RemoveLastCharacter(this String value)
			{
				Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(value));
				Contract.Ensures(Contract.Result<string>() != null);

				return value.Substring(0, value.Length - 1);
			}

			/// <summary>
			/// Removes the last.
			/// </summary>
			/// <param name="value">The value.</param>
			/// <param name="number">The number.</param>
			/// <returns></returns>
			public static string RemoveLast(this String value, int number)
			{
				Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(value));
				Contract.Requires<ArgumentOutOfRangeException>(number > 0);
				Contract.Requires<ArgumentOutOfRangeException>(number < value.Length);
				Contract.Ensures(Contract.Result<string>() != null);

				return value.Substring(0, value.Length - number);
			}

			/// <summary>
			/// Removes the first character.
			/// </summary>
			/// <param name="value">The value.</param>
			/// <returns></returns>
			public static string RemoveFirstCharacter(this String value)
			{
				Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(value));
				Contract.Ensures(Contract.Result<string>() != null);

				return value.Substring(1);
			}

			/// <summary>
			/// Removes the first.
			/// </summary>
			/// <param name="value">The value.</param>
			/// <param name="number">The number.</param>
			/// <returns></returns>
			public static string RemoveFirst(this String value, int number)
			{
				Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(value));
				Contract.Requires<ArgumentOutOfRangeException>(number > 0);
				Contract.Requires<ArgumentOutOfRangeException>(number < value.Length);
				Contract.Ensures(Contract.Result<string>() != null);

				return value.Substring(number);
			}
		}
}
