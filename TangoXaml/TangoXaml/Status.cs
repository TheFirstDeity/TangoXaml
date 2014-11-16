using System.Windows;

namespace TangoXaml
{
    public sealed class Status : ResourceDictionary
    {
        public Status()
        {
            MergedDictionaries.Add(Tango.DictionariesByCategory[GetType().Name]);
        }
    }
}
