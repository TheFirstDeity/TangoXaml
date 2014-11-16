using System.Windows;

namespace TangoXaml
{
    public sealed class Categories : ResourceDictionary
    {
        public Categories()
        {
            MergedDictionaries.Add(Tango.DictionariesByCategory[GetType().Name]);
        }
    }
}
