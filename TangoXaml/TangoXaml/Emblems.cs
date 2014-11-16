using System.Windows;

namespace TangoXaml
{
    public sealed class Emblems : ResourceDictionary
    {
        public Emblems()
        {
            MergedDictionaries.Add(Tango.DictionariesByCategory[GetType().Name]);
        }
    }
}
