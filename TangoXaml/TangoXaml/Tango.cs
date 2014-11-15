using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace TangoXaml
{
    [ValueConversion(typeof(Lazy<DrawingGroup>), typeof(DrawingGroup))]
    public class Tango : ResourceDictionary, IValueConverter
    {
        private static readonly string ResourcePath = typeof(Tango).Namespace + ".Scalable.";
        private readonly Dictionary<string, ResourceDictionary> _DictionariesByCategory;

        private ResourceDictionary _Actions;
        private ResourceDictionary _Apps;
        private ResourceDictionary _Categories;
        private ResourceDictionary _Devices;
        private ResourceDictionary _Emblems;
        private ResourceDictionary _Emotes;
        private ResourceDictionary _Mimetypes;
        private ResourceDictionary _Places;
        private ResourceDictionary _Status;

        public ResourceDictionary Actions { get { return _Actions = _Actions ?? _DictionariesByCategory["Actions"]; } }
        public ResourceDictionary Apps { get { return _Apps = _Apps ?? _DictionariesByCategory["Apps"]; } }
        public ResourceDictionary Categories { get { return _Categories = _Categories ?? _DictionariesByCategory["Categories"]; } }
        public ResourceDictionary Devices { get { return _Devices = _Devices ?? _DictionariesByCategory["Devices"]; } }
        public ResourceDictionary Emblems { get { return _Emblems = _Emblems ?? _DictionariesByCategory["Emblems"]; } }
        public ResourceDictionary Emotes { get { return _Emotes = _Emotes ?? _DictionariesByCategory["Emotes"]; } }
        public ResourceDictionary Mimetypes { get { return _Mimetypes = _Mimetypes ?? _DictionariesByCategory["Mimetypes"]; } }
        public ResourceDictionary Places { get { return _Places = _Places ?? _DictionariesByCategory["Places"]; } }
        public ResourceDictionary Status { get { return _Status = _Status ?? _DictionariesByCategory["Status"]; } }
        public IValueConverter Converter { get { return this; } }

        internal static string GetCategoryPath(string category)
        {
            return string.Format("{0}{1}.", ResourcePath, category);
        }

        public Tango()
        {
            var splitChar = new char[] { '.' };
            var assembly = GetType().Assembly;

            var resourceInfo = assembly.GetManifestResourceNames()
                .Where(n => n.StartsWith(ResourcePath))
                .Select(n => new {
                    CategoryAndName = n.Substring(ResourcePath.Length).Split(splitChar, 2),
                    Drawing = new Lazy<DrawingGroup>(() => (DrawingGroup)XamlReader.Load(assembly.GetManifestResourceStream(n)))
                });

            _DictionariesByCategory = new Dictionary<string, ResourceDictionary>();

            ResourceDictionary dict;
            foreach (var info in resourceInfo)
            {
                if (!_DictionariesByCategory.TryGetValue(info.CategoryAndName[0], out dict))
                {   
                    dict = new ResourceDictionary();
                    _DictionariesByCategory.Add(info.CategoryAndName[0], dict);
                }
                dict.Add(info.CategoryAndName[1], info.Drawing);
            }
        }


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(DrawingGroup)) throw new ArgumentException("Can only convert to " + typeof(DrawingGroup).FullName);
            var lazy = (Lazy<DrawingGroup>)value;
            return lazy.Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(DrawingGroup)) throw new ArgumentException("Can only convert back to " + typeof(Lazy<DrawingGroup>).FullName);
            var draw = (DrawingGroup)value; // Do the casting here so it throws an exception right away if it's the wrong type
            return new Lazy<DrawingGroup>(() => draw);
        }
    }
}
