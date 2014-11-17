using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
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
            var name = GetType().Name.ToLower();
            MergedDictionaries.Add(Tango.Instance.DictionariesByCategory[GetType().Name.ToLower()]);
        }
    }

    public sealed class Apps : ResourceDictionary
    {
        public Apps()
        {
            MergedDictionaries.Add(Tango.Instance.DictionariesByCategory[GetType().Name.ToLower()]);
        }
    }

    public sealed class Categories : ResourceDictionary
    {
        public Categories()
        {
            MergedDictionaries.Add(Tango.Instance.DictionariesByCategory[GetType().Name.ToLower()]);
        }
    }

    public sealed class Devices : ResourceDictionary
    {
        public Devices()
        {
            MergedDictionaries.Add(Tango.Instance.DictionariesByCategory[GetType().Name.ToLower()]);
        }
    }

    public sealed class Emblems : ResourceDictionary
    {
        public Emblems()
        {
            MergedDictionaries.Add(Tango.Instance.DictionariesByCategory[GetType().Name.ToLower()]);
        }
    }

    public sealed class Emotes : ResourceDictionary
    {
        public Emotes()
        {
            MergedDictionaries.Add(Tango.Instance.DictionariesByCategory[GetType().Name.ToLower()]);
        }
    }

    public sealed class Mimetypes : ResourceDictionary
    {
        public Mimetypes()
        {
            MergedDictionaries.Add(Tango.Instance.DictionariesByCategory[GetType().Name.ToLower()]);
        }
    }

    public sealed class Places : ResourceDictionary
    {
        public Places()
        {
            MergedDictionaries.Add(Tango.Instance.DictionariesByCategory[GetType().Name.ToLower()]);
        }
    }

    public sealed class Status : ResourceDictionary
    {
        public Status()
        {
            MergedDictionaries.Add(Tango.Instance.DictionariesByCategory[GetType().Name.ToLower()]);
        }
    }
    #endregion

    internal class Tango
    {
        private static readonly string ResourceManifestName = typeof(Tango).Namespace + ".g.resources";
        private static readonly string ResourcePath = "scalable/";

        private Assembly _Assembly = typeof(Tango).Assembly;
        internal Dictionary<string, ResourceDictionary> DictionariesByCategory;

        private static Tango _Instance;
        public static Tango Instance { get { return _Instance = _Instance ?? new Tango(); } }

        private Tango()
        {
            var splitChar = new char[] { '.', '/' };
            _Assembly = typeof(Tango).Assembly;
            var names = new List<string>();

            using (var reader = new ResourceReader(_Assembly.GetManifestResourceStream(ResourceManifestName)))
            {
                foreach (DictionaryEntry r in reader) names.Add((string)r.Key);
            }
            
            var resourceInfo = names
                    .Where(n => n.StartsWith(ResourcePath))
                    .Select(n => new
                    {
                        CategoryAndName = n.Substring(ResourcePath.Length).Split(splitChar),
                        Drawing = new Lazy<DrawingGroup>(() => LoadBaml(n))
                        // Drawing = new Lazy<DrawingGroup>(() => (DrawingGroup)Application.LoadComponent(new Uri("TangoXaml.g.resources/" + n, UriKind.Relative)))
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

        private DrawingGroup LoadBaml(string resourceName)
        {
            string typeName;
            byte[] data;

            using (var reader = new ResourceReader(_Assembly.GetManifestResourceStream(ResourceManifestName)))
            {
                reader.GetResourceData(resourceName, out typeName, out data);
            }

            using (var stream = new MemoryStream(data))
            {
                return LoadBaml<DrawingGroup>(stream);
            }
        }

        /// <remarks>https://social.msdn.microsoft.com/Forums/en-US/b89860ad-1121-4797-9027-42c89281825f/load-a-resource-dictionary-from-an-assembly-from-a-different-appdomain?forum=wpf</remarks>
        private static T LoadBaml<T>(Stream stream)
        {
            var reader = new Baml2006Reader(stream);
            var writer = new System.Xaml.XamlObjectWriter(reader.SchemaContext);
            while (reader.Read())
                writer.WriteNode(reader);
            return (T)writer.Result;
        } 
    }

}
