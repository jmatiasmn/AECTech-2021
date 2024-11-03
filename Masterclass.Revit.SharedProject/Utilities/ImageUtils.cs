using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Media.Imaging;
using UIFramework.PdfImport;

namespace Masterclass.Revit.Utilities
{
    public static class ImageUtils
    {
        public static BitmapImage LoadImage(Assembly assembly, string name)
        {
            BitmapImage image = new BitmapImage();

            try
            {
                string resourceName = assembly
                    .GetManifestResourceNames()
                    .FirstOrDefault(x => x.Contains(name));

                var stream = assembly.GetManifestResourceStream(resourceName);

                image.BeginInit();
                image.StreamSource = stream;
                image.EndInit();

            }
            catch (Exception)
            {

                // ignore
            }
        return image;
        }
    }
}
