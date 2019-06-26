using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EntityToCsv
{
    public static class EntityToCsvSerializer
    {
        private static char _delimiter;
        private static volatile StringBuilder _stringBuilder;
        private static PropertyDescriptorCollection _propertyDescriptorCollection;
        private static readonly object Locker = new object();

        //Common Format and MIME Type for CSV: https://tools.ietf.org/html/rfc4180
        private const string EscapedDoubleQuote = "\"";
        private static readonly char[] CsvEscapedTokens = { '\'', '\"', ',', '\n', '\r' };

        public static string SerializeToDelimitedText<T>(IEnumerable<T> enumerable, char delimiter = ',')
        {
            try
            {
                lock (Locker)
                {
                    Setup<T>(delimiter);
                    Serialize(enumerable);
                    return GetSerializedText();
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message, exception);
            }
        }

        private static void Setup<T>(char delimiter)
        {
            _delimiter = delimiter;
            _stringBuilder = new StringBuilder();
            _propertyDescriptorCollection = TypeDescriptor.GetProperties(typeof(T));
        }

        private static void Serialize<T>(IEnumerable<T> records)
        {
            WriteHeader();
            WriteValues(records);
        }

        private static void WriteHeader()
        {
            var propertyCount = 0;
            foreach (PropertyDescriptor property in _propertyDescriptorCollection)
            {
                if (propertyCount++ > 0) _stringBuilder.Append(_delimiter);
                _stringBuilder.Append(property.DisplayName);
            }

            _stringBuilder.AppendLine();
        }

        private static void WriteValues<T>(IEnumerable<T> records)
        {
            foreach (var item in records)
            {
                var propertyCount = 0;
                foreach (PropertyDescriptor property in _propertyDescriptorCollection)
                {
                    if (propertyCount++ > 0) _stringBuilder.Append(_delimiter);
                    _stringBuilder.Append(GetCsvComplianceValue(property, item));
                }

                _stringBuilder.AppendLine();
            }
        }

        private static string GetCsvComplianceValue<T>(PropertyDescriptor property, T item)
        {
            var value = property.Converter?.ConvertToString(property.GetValue(item) ?? string.Empty);
            if (value?.IndexOfAny(CsvEscapedTokens) >= 0)
            {
                value = EscapedDoubleQuote + value.Replace(EscapedDoubleQuote, "\"\"") + EscapedDoubleQuote;
            }

            return value;
        }

        private static string GetSerializedText()
        {
            return _stringBuilder.ToString();
        }
    }
}
