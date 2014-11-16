using System.Windows;

namespace TangoXaml
{
    public sealed class Actions : ResourceDictionary
    {
        public Actions()
        {
            MergedDictionaries.Add(Tango.DictionariesByCategory[GetType().Name]);
        }
    }
}
