using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Baml2006;
using System.Windows.Markup;
using System.Windows.Media;

namespace TangoXaml
{
    #region Category Classes
    public sealed class Actions : ResourceDictionary
    {
        public Actions()
        {
            MergedDictionaries.Add(Tango.DictionariesByCategory[GetType().Name]);
        }
    }

    public sealed class Apps : ResourceDictionary
    {
        public Apps()
        {
            MergedDictionaries.Add(Tango.DictionariesByCategory[GetType().Name]);
        }
    }

    public sealed class Categories : ResourceDictionary
    {
        public Categories()
        {
            MergedDictionaries.Add(Tango.DictionariesByCategory[GetType().Name]);
        }
    }

    public sealed class Devices : ResourceDictionary
    {
        public Devices()
        {
            MergedDictionaries.Add(Tango.DictionariesByCategory[GetType().Name]);
        }
    }

    public sealed class Emblems : ResourceDictionary
    {
        public Emblems()
        {
            MergedDictionaries.Add(Tango.DictionariesByCategory[GetType().Name]);
        }
    }

    public sealed class Emotes : ResourceDictionary
    {
        public Emotes()
        {
            MergedDictionaries.Add(Tango.DictionariesByCategory[GetType().Name]);
        }
    }

    public sealed class Mimetypes : ResourceDictionary
    {
        public Mimetypes()
        {
            MergedDictionaries.Add(Tango.DictionariesByCategory[GetType().Name]);
        }
    }

    public sealed class Places : ResourceDictionary
    {
        public Places()
        {
            MergedDictionaries.Add(Tango.DictionariesByCategory[GetType().Name]);
        }
    }

    public sealed class Status : ResourceDictionary
    {
        public Status()
        {
            MergedDictionaries.Add(Tango.DictionariesByCategory[GetType().Name]);
        }
    }
    #endregion

    internal static class Tango
    {
        private static readonly string ResourcePath = typeof(Tango).Namespace + ".Compiled.";
        internal static readonly Dictionary<string, ResourceDictionary> DictionariesByCategory;

        internal static string GetCategoryPath(string category)
        {
            return string.Format("{0}{1}.", ResourcePath, category);
        }

        static Tango()
        {
            var splitChar = new char[] { '.' };
            var assembly = typeof(Tango).Assembly;

            var resourceInfo = assembly.GetManifestResourceNames()
                .Where(n => n.StartsWith(ResourcePath))
                .Select(n => new
                {
                    CategoryAndName = n.Substring(ResourcePath.Length).Split(splitChar),
                    Drawing = new Lazy<DrawingGroup>(() => LoadBaml<DrawingGroup>(assembly.GetManifestResourceStream(n)))
                });

            DictionariesByCategory = new Dictionary<string, ResourceDictionary>();

            ResourceDictionary dict;
            foreach (var info in resourceInfo)
            {
                if (!DictionariesByCategory.TryGetValue(info.CategoryAndName[0], out dict))
                {
                    dict = new ResourceDictionary();
                    DictionariesByCategory.Add(info.CategoryAndName[0], dict);
                }
                dict.Add(info.CategoryAndName[1], info.Drawing);
            }
        }

        private static T LoadBaml<T>(Stream stream)
        {
            var reader = new Baml2006Reader(stream);
            return (T)XamlReader.Load(reader);
        }
    }
}
