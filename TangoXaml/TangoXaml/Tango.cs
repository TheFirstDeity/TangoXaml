using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace TangoXaml
{
    internal static class Tango
    {
        private static readonly string ResourcePath = typeof(Tango).Namespace + ".Scalable.";
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
                .Select(n => new {
                    CategoryAndName = n.Substring(ResourcePath.Length).Split(splitChar),
                    Drawing = new Lazy<DrawingGroup>(() => (DrawingGroup)XamlReader.Load(assembly.GetManifestResourceStream(n)))
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
    }
}
