using System.Windows;

namespace TangoXaml
{
    public sealed class Places : ResourceDictionary
    {
        public Places()
        {
            MergedDictionaries.Add(Tango.DictionariesByCategory[GetType().Name]);
        }
    }
}
