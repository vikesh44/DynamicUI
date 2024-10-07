using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Pxic.DynamicUI.Helper
{
    public class HelperFunctions
    {
        public BitmapImage? ByteArrayToImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return null;

            BitmapImage image = new();
            using (MemoryStream ms = new(imageData))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
            }
            return image;
        }

        #region Singleton Instance Implementation

        private HelperFunctions()
        {
        }

        private static readonly object objLock = new();

        private static HelperFunctions? instance = null;
        public static HelperFunctions Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (objLock)
                    {
                        instance ??= new HelperFunctions();
                    }
                }
                return instance;
            }
        }

        #endregion
    }
}
