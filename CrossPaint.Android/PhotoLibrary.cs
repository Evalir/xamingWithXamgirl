using System;
using System.Threading.Tasks;
using Android.Media;
using CrossPaint.Services;
using Java.IO;
using Android.OS;
using CrossPaint.Droid;
using Android.Content;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhotoLibrary))]
namespace CrossPaint.Droid
{
    public class PhotoLibrary : IPhotoLibrary
    {
        public PhotoLibrary()
        {
        }


        public Task<System.IO.Stream> PickPhotoAsync()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            // Start the picture-picker activity (resumes in MainActivity.cs)
            MainActivity.Instance.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Picture"),
                MainActivity.PickImageId);

            // Save the TaskCompletionSource object as a MainActivity property
            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<System.IO.Stream>();

            // Return Task object
            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }

        public async Task<bool> SavePhotoAsync(byte[] data, string folder, string filename)
        {
            try
            {
                File picturesDirectory = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
                File folderDirectory = picturesDirectory;

                if (!string.IsNullOrEmpty(folder))
                {
                    folderDirectory = new File(picturesDirectory, folder);
                    folderDirectory.Mkdirs();
                }

                using (File bitmapFile = new File(folderDirectory, filename))
                {
                    bitmapFile.CreateNewFile();

                    using (FileOutputStream outputStream = new FileOutputStream(bitmapFile))
                    {
                        await outputStream.WriteAsync(data);
                    }

                    // Make sure it shows up in the Photos gallery promptly.
                    MediaScannerConnection.ScanFile(MainActivity.Instance,
                                                    new string[] { bitmapFile.Path },
                                                    new string[] { "image/png", "image/jpeg" }, null);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
