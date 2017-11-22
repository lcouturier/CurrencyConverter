namespace CurrencyConverter.Library.Core
{
    using System.IO;
    using System.Text;

    /// <summary>
    /// StringWriter UTF8
    /// </summary>
    internal class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }
    }
}