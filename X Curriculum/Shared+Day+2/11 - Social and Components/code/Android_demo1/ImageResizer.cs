namespace EvolveLite
{
    using System;
    using System.IO;

    using Android.Graphics;

    using Path = System.IO.Path;

    /// <summary>
    ///   This class is used to scale an image to the requested size. This ensures
    ///   that the image doesn't use up more RAM than necessary.
    /// </summary>
    public class ImageResizer
    {
        /// <summary>
        ///   This returns a Bitmap that has been scaled to the specified images.
        /// </summary>
        /// <param name="path">The file on disk to resize.</param>
        /// <param name="width">The maximum width of the image.</param>
        /// <param name="height">The maximum height of the image.</param>
        /// <returns>A Bitmap that is scaled to the specified size.</returns>
        private Bitmap GetResizedBitmap(string path, int width, int height)
        {
            var options = new BitmapFactory.Options
                              {
                                  InJustDecodeBounds = true
                              };
            BitmapFactory.DecodeFile(path, options);

            options.InSampleSize = CalculateInSampleSize(options, width, height);
            options.InJustDecodeBounds = false;
            var bitmap = BitmapFactory.DecodeFile(path, options);
            return bitmap;
        }

        /// <summary>
        ///   This will create a new thumbnail file on the device.
        /// </summary>
        /// <param name="pathToOriginal">Path to the original file that we want to resize.</param>
        /// <param name="width">The maximum width of the image.</param>
        /// <param name="height">The maximum height of the image.</param>
        /// <returns>Returns the path to the newly created thumbnail file.</returns>
        public string CreateThumbnailFile(string pathToOriginal, int width, int height)
        {
            var fullPathToThumbnail = GetNameOfThumbnail(pathToOriginal);

            var bitmap = GetResizedBitmap(pathToOriginal, width, height);

            using (var outstream = File.Open(fullPathToThumbnail, FileMode.OpenOrCreate))
            {
                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, outstream);
            }
            return fullPathToThumbnail;
        }

        private static string GetNameOfThumbnail(string pathToOriginal)
        {
            var fileinfo = new FileInfo(pathToOriginal);
            var thumbnailfile = String.Concat(Path.GetFileNameWithoutExtension(fileinfo.Name), "_thumbnail", fileinfo.Extension);
            var fullPathToThumbnail = Path.Combine(fileinfo.DirectoryName, thumbnailfile);
            return fullPathToThumbnail;
        }

        /// <summary>
        ///   This method will calculate a scaling factor that must be applied to get the image down
        ///   to the dimensions specified.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="reqWidth"></param>
        /// <param name="reqHeight"></param>
        /// <returns>A scaling factor to reduce the size of the image.</returns>
        private static int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            float height = options.OutHeight;
            float width = options.OutWidth;
            var inSampleSize = 1d;

            if (height > reqHeight || width > reqWidth)
            {
                if (width > height)
                {
                    inSampleSize = Math.Round(height / reqHeight);
                }
                else
                {
                    inSampleSize = Math.Round(width / reqWidth);
                }
            }

            return (int)inSampleSize;
        }
    }
}
