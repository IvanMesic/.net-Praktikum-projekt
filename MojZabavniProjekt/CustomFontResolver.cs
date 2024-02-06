using PdfSharp.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojZabavniProjekt
{
    public class CustomFontResolver : IFontResolver
    {
        private readonly Dictionary<string, string> _fontMappings;

        public CustomFontResolver()
        {
            // Add font mappings for Times New Roman and Arial
            _fontMappings = new Dictionary<string, string>
        {
            {"Open Sans", "OpenSans.ttf"} 
        };
        }

        public byte[] GetFont(string faceName)
        {
            if (_fontMappings.TryGetValue(faceName, out var fontPath))
            {
                return File.ReadAllBytes(fontPath);
            }

            throw new ArgumentException($"Font '{faceName}' not found.");
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            return new FontResolverInfo(familyName, isBold, isItalic);
        }
    }
}
