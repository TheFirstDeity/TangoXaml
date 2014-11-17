using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Windows.Media;

namespace TangoXaml
{
    public class TangoTest
    {
        internal const string _ManifestResourceName = "TangoXaml.g.resources";

        [Fact]
        public void CheckBaml()
        {
            var assembly = typeof(Actions).Assembly;
            var resources = new Dictionary<string, UnmanagedMemoryStream>();

            using (var reader = new ResourceReader(assembly.GetManifestResourceStream(_ManifestResourceName)))
            {
                foreach (DictionaryEntry r in reader)
                {
                    resources.Add((string)r.Key, (UnmanagedMemoryStream)r.Value);
                }
            }



            // File.WriteAllLines("resources.txt", resources.Keys.Select(k => k + "  "));
        }

        [Fact]
        public void LoadBaml()
        {
            var actions = new Actions();
            foreach (var obj in actions.MergedDictionaries[0].Values)
            {
                var lazy = (Lazy<DrawingGroup>)obj;
                var drawing = lazy.Value;
            }
        }


    }
}
