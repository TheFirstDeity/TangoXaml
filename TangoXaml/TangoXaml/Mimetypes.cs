using System.Windows;

namespace TangoXaml
{
    public sealed class Mimetypes : ResourceDictionary
    {
        public Mimetypes()
        {
            MergedDictionaries.Add(Tango.DictionariesByCategory[GetType().Name]);
        }
    }
}
