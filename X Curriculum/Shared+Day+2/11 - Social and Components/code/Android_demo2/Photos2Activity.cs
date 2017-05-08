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

using Xamarin.Media;
using Xamarin.Social;
using Xamarin.Social.Services;

using Environment = Android.OS.Environment;
using JavaFile = Java.IO.File;
using Path = System.IO.Path;
using Service = Xamarin.Social.Service;
using Uri = Android.Net.Uri;

namespace EvolveLite {
	[Activity(Label = "@string/activity_photos_label", MainLauncher = true, Icon = "@drawable/ic_launcher")]
    public class PhotosActivity : Activity {
        /// <summary>
        /// We will keep pictures in a directory that is based on the year and month.
        /// </summary>
        public static readonly string MediaDirectory = "201304";

        private readonly ImageResizer imageResizer = new ImageResizer();
        private readonly Service twitterService = new TwitterService
                           {
                               ConsumerKey = "YOUR CONSUMER KEY HERE",
                               ConsumerSecret = "YOUR CONSUMER SECRET HERE"
                           };
        private EvolveLitePhotoAdapter photoAdapter;
        private GridView gridView;

		#region Context Menu
//		//TODO: AndroidDemo2a: create Context menu
//		public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
//		{
//			base.OnCreateContextMenu(menu, v, menuInfo);
//			MenuInflater.Inflate(Resource.Menu.photo_context_menu, menu);
//		}
//
//		//TODO: AndroidDemo2a: handle Context menu selection
//        public override bool OnContextItemSelected(IMenuItem item)
//        {
//            var info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
//            var thumbnailFile = photoAdapter.GetFileName(info.Position);
//            var photoFile = thumbnailFile.Replace("_thumbnail", String.Empty);
//
//            switch (item.ItemId)
//            {
//                case Resource.Id.menu_tweet_picture:
//                    SharePictureOnTwitter(thumbnailFile);
//                    return true;
//
//				case Resource.Id.menu_email_picture:
//					SharePictureViaEmail(photoFile);
//					return true;
//
//                case Resource.Id.menu_apply_filter:
//                    ApplySepiaFilterToPhoto(thumbnailFile, photoFile);
//                    return true;
//
//                case Resource.Id.menu_delete_picture:
//                    Task.Factory.StartNew(() =>
//                      {
//                          // Delete the picture and the thumbnail, but
//                          // don't do it on the UI thread.
//                          File.Delete(thumbnailFile);
//                          File.Delete(photoFile);
//                          Log.Debug(GetType().FullName, "Deleted files {0} and {1}.", thumbnailFile, photoFile);
//                      });
//
//                    photoAdapter.RemoveFile(thumbnailFile);
//                    return true;
//
//                default:
//                    return base.OnContextItemSelected(item);
//            }
//        }
		#endregion

		#region Activity Menu
//		//TODO: AndroidDemo2a: create Options menu
//        public override bool OnCreateOptionsMenu(IMenu menu)
//        {
//            MenuInflater.Inflate(Resource.Menu.photosactivity_menu, menu);
//            return true;
//        }
//
//		//TODO: AndroidDemo2a: handle Options menu selection
//		public override bool OnOptionsItemSelected(IMenuItem item)
//        {
//            switch (item.ItemId)
//            {
//                case Resource.Id.menu_take_picture:
//                    TakePhoto();
//                    return true;
//            }
//            return base.OnOptionsItemSelected(item);
//        }
		#endregion

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Photos);
            InitializeAdapter();

            gridView = FindViewById<GridView>(Resource.Id.gridView);
            gridView.Adapter = photoAdapter;
            RegisterForContextMenu(gridView);
        }

        /// <summary>
        /// This method will ensure that the picture gets added to the default Gallery
        /// application on the Android device.
        /// </summary>
        /// <param name="pathToImage"></param>
        private void AddPictureToAndroidGallery(string pathToImage)
        {
            var mediaScanIntent = new Intent("android.intent.action.MEDIA_SCANNER_SCAN_FILE");
            var file = new JavaFile(pathToImage);
            var contentUri = Uri.FromFile(file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);
        }

        /// <summary>
        ///   This method will apply a sepia filter to two files.
        /// </summary>
        /// <remarks>Happens in parallel on two seperate threads.</remarks>
        /// <param name="thumbnailFile"></param>
        /// <param name="photoFile"></param>
        private void ApplySepiaFilterToPhoto(string thumbnailFile, string photoFile)
        {
            var tasks = new Task[2];
            tasks[0] = new Task(() =>
                                    {
                                        var filter = new SepiaFilter();
                                        filter.ApplyToFile(thumbnailFile);
                                    });
            tasks[1] = new Task(() =>
                                    {
                                        var filter = new SepiaFilter();
                                        filter.ApplyToFile(photoFile);
                                    });

            for (var i = 0; i < tasks.Length; i++) {
                tasks[i].Start();
            }
            Task.WaitAll(tasks);
            photoAdapter.NotifyDataSetChanged();
        }

        private void InitializeAdapter()
        {
            // Get a list of all existing thumbnails to seed the adapter.
            var pictureDirectory = GetExternalFilesDir(Environment.DirectoryPictures);
            var evolveLitePictureDirectory = Path.Combine(pictureDirectory.ToString(), MediaDirectory);
            IEnumerable<string> thumbnailfiles = Directory.Exists(evolveLitePictureDirectory) ? Directory.GetFiles(evolveLitePictureDirectory, "*_thumbnail.jpg") : new string[0];
            photoAdapter = new EvolveLitePhotoAdapter(this, thumbnailfiles);
        }

        void TakePhoto()
        {
            var picker = new MediaPicker(this);
			if (!picker.IsCameraAvailable || !picker.PhotosSupported) {
                Toast.MakeText(this, Resources.GetString(Resource.String.camera_not_available), ToastLength.Short);
				return;
            }

            var fileName = string.Format("EvolveLite2013_{0:yyyyMMddHHmmss}.jpg", DateTime.UtcNow);
            var mediaOptions = new StoreCameraMediaOptions
                                   {
                                       Directory = MediaDirectory,
                                       Name = fileName
                                   };

            picker.TakePhotoAsync(mediaOptions)
                  .ContinueWith(PhotoTaken, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// This is called when the picture is taken with Xamarin.Media.MediaPicker.
        /// </summary>
        /// <param name="task"></param>
        private void PhotoTaken(Task<MediaFile> task)
        {
            if (task.IsCanceled || task.IsFaulted) {
                return;
            }
            var file = task.Result.Path;
            AddPictureToAndroidGallery(file);
            Log.Debug(GetType().FullName, "Picture saved to {0}.", file);
            var thumbnail = imageResizer.CreateThumbnailFile(file, 220, 220);
            photoAdapter.AddFile(thumbnail);
        }

		//TODO: AndroidDemo2b: TWITTER
//        private void SharePictureOnTwitter(String pathToPhoto)
//        {
//            var photo = BitmapFactory.DecodeFile(pathToPhoto);
//            var item = new Item("Now I'm sharing things. #testing");
//            var imageData = new ImageData(photo);
//            item.Images.Add(imageData);
//
//            var intent = twitterService.GetShareUI(this, item, shareResult =>{});
//
//            StartActivity(intent);
//        }

		//TODO: AndroidDemo2b: EMAIL
//		void SharePictureViaEmail (String pathToPhoto)
//		{
//			var email = new Intent (Android.Content.Intent.ActionSend);
//			email.PutExtra (Android.Content.Intent.ExtraEmail, new string[]{"hello@xamarin.com"} );
//			email.PutExtra (Android.Content.Intent.ExtraSubject, "Hello Xamarin Evolve");
//			email.PutExtra (Android.Content.Intent.ExtraText, "Hello from Austin, Texas!");
//			email.PutExtra (Android.Content.Intent.ExtraStream, Uri.Parse("file://"+pathToPhoto)); // attachment
//
//			// Will let the user choose Messages or Email to handle intent
//			//email.SetType("text/plain");
//			// Will force Email to handle intent, if it has been configured
//			email.SetType ("message/rfc822");
//			StartActivity (email);
//		}
    }
}
