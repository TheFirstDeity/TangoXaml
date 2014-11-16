using System.Windows;

namespace TangoXaml
{
    public sealed class Apps : ResourceDictionary
    {
        public Apps()
        {
            MergedDictionaries.Add(Tango.DictionariesByCategory[GetType().Name]);
        }
    }
}
