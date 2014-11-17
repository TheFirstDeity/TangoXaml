using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TangoXaml
{
    public class TangoTest
    {
        internal const string _ManifestResourceName = "TangoXaml.g.resources";

        [Fact]
        public void CheckBaml()
        {
            var assembly = typeof(Actions).Assembly;
            var resources = new Dictionary<string, object>();

            using (var reader = new ResourceReader(assembly.GetManifestResourceStream(_ManifestResourceName)))
            {
                foreach (DictionaryEntry r in reader)
                {
                    resources.Add((string)r.Key, r.Value.GetType().Name);
                }
            }

            // File.WriteAllLines("resources.txt", resources.Keys.Select(k => k + "  "));
        }
    }
}
