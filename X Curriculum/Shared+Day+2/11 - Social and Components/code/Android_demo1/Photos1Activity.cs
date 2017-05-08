using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

using Xamarin.Media; // Xamarin.Mobile component

using Environment = Android.OS.Environment;
using JavaFile = Java.IO.File;
using Path = System.IO.Path;
using Uri = Android.Net.Uri;

namespace EvolveLite {
	[Activity(Label = "@string/activity_photos_label", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class PhotosActivity : Activity {
        /// <summary>We will keep pictures in a directory that is based on the year and month</summary>
        static readonly string MediaDirectory = "201304";

        readonly ImageResizer imageResizer = new ImageResizer();
        EvolveLitePhotoAdapter photoAdapter;
        GridView gridView;
		Button photoButton;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Photos);
            InitializeAdapter();

            gridView = FindViewById<GridView>(Resource.Id.gridView);
            gridView.Adapter = photoAdapter;

			//TODO: AndroidDemo1a: wire up the Take Photo button
//			photoButton = FindViewById<Button> (Resource.Id.takePhoto);
//			photoButton.Click += (sender, e) => {
//				TakePhoto();
//			};


			//TODO: AndroidDemo1b: enable filter when clicked
//			gridView.ItemClick += (sender, e) => {
//				var thumbnailFile = photoAdapter.GetFileName(e.Position);
//				var photoFile = thumbnailFile.Replace("_thumbnail", String.Empty);
//				ApplySepiaFilterToPhoto(thumbnailFile, photoFile);
//				Toast.MakeText(this, Resources.GetString(Resource.String.apply_sepia_filter_done), ToastLength.Short);
//			};
        }

		//TODO: AndroidDemo1a: Take a photo using Xamarin.Mobile
//		void TakePhoto()
//		{
//			var picker = new MediaPicker(this);
//
//			if (!picker.IsCameraAvailable || !picker.PhotosSupported) {
//				Toast.MakeText(this, Resources.GetString(Resource.String.camera_not_available), ToastLength.Short);
//				return;
//			}
//			
//			var fileName = string.Format("EvolveLite2013_{0:yyyyMMddHHmmss}.jpg", DateTime.UtcNow);
//			var mediaOptions = new StoreCameraMediaOptions {
//				Directory = MediaDirectory,
//				Name = fileName
//			};
//			
//			picker.TakePhotoAsync(mediaOptions).ContinueWith( t => {
//					if (t.IsCanceled || t.IsFaulted) 
//						return;
//					// resize and save the picture
//					PictureTaken(t);
//				}, TaskScheduler.FromCurrentSynchronizationContext());
//		}

		/// <summary>Called when the picture is taken with Xamarin.Media.MediaPicker</summary>
	 	void PictureTaken(Task<MediaFile> task)
		{
			var file = task.Result.Path;
			AddPictureToAndroidGallery(file);
			Log.Debug(GetType().FullName, "Picture saved to {0}.", file);
			var thumbnail = imageResizer.CreateThumbnailFile(file, 220, 220);
			photoAdapter.AddFile(thumbnail);
		}


        /// <summary>
        /// This method will ensure that the picture gets added to the default Gallery
        /// application on the Android device.
        /// </summary>
        void AddPictureToAndroidGallery(string pathToImage)
        {
            var mediaScanIntent = new Intent("android.intent.action.MEDIA_SCANNER_SCAN_FILE");
            var file = new JavaFile(pathToImage);
            var contentUri = Uri.FromFile(file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);
        }

       
		//TODO: AndroidDemo1b: apply a filter to a photo - on click!
		/// <summary>
		/// This method will apply a sepia filter to two files.
		/// </summary>
		/// <remarks>Happens in parallel on two seperate threads.</remarks>
//		void ApplySepiaFilterToPhoto(string thumbnailFile, string photoFile)
//		{
//			var tasks = new Task[2];
//			tasks[0] = new Task(() =>
//			                    {
//				var filter = new SepiaFilter();
//				filter.ApplyToFile(thumbnailFile);
//			});
//			tasks[1] = new Task(() =>
//			                    {
//				var filter = new SepiaFilter();
//				filter.ApplyToFile(photoFile);
//			});
//			
//			for (var i = 0; i < tasks.Length; i++)
//			{
//				tasks[i].Start();
//			}
//			Task.WaitAll(tasks);
//			photoAdapter.NotifyDataSetChanged();
//		}



        void InitializeAdapter()
        {
            // Get a list of all existing thumbnails to seed the adapter.
            var pictureDirectory = GetExternalFilesDir(Environment.DirectoryPictures);
            var evolveLitePictureDirectory = Path.Combine(pictureDirectory.ToString(), MediaDirectory);
            IEnumerable<string> thumbnailfiles = Directory.Exists(evolveLitePictureDirectory) ? Directory.GetFiles(evolveLitePictureDirectory, "*_thumbnail.jpg") : new string[0];
            photoAdapter = new EvolveLitePhotoAdapter(this, thumbnailfiles);
        }
    }
}