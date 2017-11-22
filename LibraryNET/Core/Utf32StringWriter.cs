namespace CurrencyConverter.Library.Core
{
    using System.IO;
    using System.Text;

    /// <summary>
    /// StringWriter UTF16
    /// </summary>
    internal class Utf32StringWriter : StringWriter
    {
        public override Encoding Encoding
        {
            get
            {
                return Encoding.UTF32;
            }
        }
    }
}