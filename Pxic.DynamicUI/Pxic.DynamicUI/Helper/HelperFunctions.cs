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
        /// <summary>
        /// Converts the <see cref="byte[]"/> to <see cref="BitmapImage"/>
        /// Generally images are stoed in <see cref="byte[]"/> format in database and required to be converted.
        /// </summary>
        /// <param name="imageData"><see cref="byte[]"/> of the image</param>
        /// <returns>Returns converted <see cref="BitmapImage"/> object</returns>
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
        /// <summary>
        /// Gets the singleton instance of the <see cref="HelperFunctions"/> class.
        /// </summary>
        /// <remarks>
        /// This property ensures that only one instance of the <see cref="HelperFunctions"/> class 
        /// is created and provides a globally accessible point for application level generic operations. 
        /// The implementation is thread-safe, using double-checked locking to ensure 
        /// that the instance is created only once in a multithreaded environment as well.
        /// </remarks>
        /// <returns>
        /// The single instance of the <see cref="HelperFunctions"/> class.
        /// </returns>
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
