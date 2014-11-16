using System.Windows;

namespace TangoXaml
{
    public sealed class Emotes : ResourceDictionary
    {
        public Emotes()
        {
            MergedDictionaries.Add(Tango.DictionariesByCategory[GetType().Name]);
        }
    }
}
