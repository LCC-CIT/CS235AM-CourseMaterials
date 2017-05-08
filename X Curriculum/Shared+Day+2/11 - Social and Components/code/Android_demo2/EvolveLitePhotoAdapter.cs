using System.Collections.Generic;
using System.IO;
using System.Linq;

using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using Android.Widget;

using Java.Lang;

using String = System.String;

namespace EvolveLite {
  
    /// <summary>
    /// This adapter is used to display a 220x220 bitmap in an ImageView.
    /// </summary>
    public class EvolveLitePhotoAdapter : BaseAdapter<Bitmap> {
        readonly Context context;
        readonly List<string> files;

        public EvolveLitePhotoAdapter(Context context, IEnumerable<string> files)
        {
            this.files = files.ToList();
            this.context = context;
        }

        public override int Count { get { return files.Count; } }

        /// <summary>
        /// The Bitmap of the image file at the specified position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns>A Bitmap.</returns>
        public override Bitmap this[int position]
        {
            get
            {
                var key = files[position];
                if (File.Exists(key))
                {
                    var bitmap = BitmapFactory.DecodeFile(key);
                    return bitmap;
                }
                Log.Wtf("EvolveLite", "Where is the thumbnail {0}.", key);
                return null;
            }
        }

        /// <summary>
        /// Add a new file to the underlying dataset.
        /// </summary>
        /// <param name="pathToFile"></param>
        public void AddFile(string pathToFile)
        {
            if (String.IsNullOrWhiteSpace(pathToFile)) {
                return;
            }
            if (!File.Exists(pathToFile)) {
                return;
            }

            if (files.Contains(pathToFile)) {
                return;
            }
            files.Add(pathToFile);
            base.NotifyDataSetChanged();
        }

        public string GetFileName(int position)
        {
            return files[position];
        }

        public override Object GetItem(int position)
        {
            return this[position];
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView as ImageView ?? new ImageView(context);
            view.SetBackgroundResource(Android.Resource.Drawable.PictureFrame);
            view.SetImageBitmap(this[position]);

            return view;
        }

        public void RemoveFile(string thumbnailFile)
        {
            if (files.Contains(thumbnailFile)) {
                files.Remove(thumbnailFile);
            }
            NotifyDataSetChanged();
        }
    }
}
