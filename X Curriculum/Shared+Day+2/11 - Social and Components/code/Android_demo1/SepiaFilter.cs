using System.IO;

using Android.Graphics;
using Android.Util;

namespace EvolveLite {

	//TODO: AndroidDemo1b: helper class that applies a filter to a photo file
    /// <summary>
    /// This class will apply a sepia filter to a file. It will overwrite the existing file.
    /// </summary>
    public class SepiaFilter {
        /// <summary>
        /// The path to a file on disk.
        /// </summary>
        /// <param name="pathToPhoto">location of photo file</param>
        public void ApplyToFile(string pathToPhoto)
        {
            var originalBitmap = BitmapFactory.DecodeFile(pathToPhoto);
            var sepiaBitmap = ApplySepia(originalBitmap);
            using (var outfile = File.Open(pathToPhoto, FileMode.Create)) {
                sepiaBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, outfile);
            }
            Log.Debug(GetType().FullName, "Applied sepia filter to {0}.", pathToPhoto);
        }

        static ColorMatrixColorFilter CreateColourFilter()
        {
            var matrixA = new ColorMatrix();
            matrixA.SetSaturation(0);

            var matrixB = new ColorMatrix();
            matrixB.SetScale(1f, .95f, .82f, 1.0f);
            matrixA.SetConcat(matrixB, matrixA);

            var filter = new ColorMatrixColorFilter(matrixA);
            return filter;
        }

        Bitmap ApplySepia(Bitmap originalBitmap)
        {
            var filter = CreateColourFilter();
            var changedBitmap = originalBitmap.Copy(Bitmap.Config.Argb8888, true);

            var paint = new Paint();
            paint.SetColorFilter(filter);

            var myCanvas = new Canvas(changedBitmap);
            myCanvas.DrawBitmap(changedBitmap, 0, 0, paint);
            return changedBitmap;
        }
    }
}