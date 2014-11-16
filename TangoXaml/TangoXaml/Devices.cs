using System.Windows;

namespace TangoXaml
{
    public sealed class Devices : ResourceDictionary
    {
        public Devices()
        {
            MergedDictionaries.Add(Tango.DictionariesByCategory[GetType().Name]);
        }
    }
}
