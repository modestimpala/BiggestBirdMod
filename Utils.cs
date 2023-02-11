using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BiggestBirdMod
{
    internal class Utils
    {
        public static AssetBundle LoadAssetBundle(string bundleName, Assembly resourceAssembly)
        {
            if (resourceAssembly == null)
            {
                throw new ArgumentNullException("Parameter resourceAssembly can not be null.");
            }

            string text = null;
            try
            {
                text = resourceAssembly.GetManifestResourceNames().Single((string str) => str.EndsWith(bundleName));
            }
            catch (Exception)
            {
            }

            if (text == null)
            {
                return null;
            }

            using Stream stream = resourceAssembly.GetManifestResourceStream(text);
            return AssetBundle.LoadFromStream(stream);
        }
    }
}
