using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media;
using Windows.Media.Capture;
using Windows.Media.Devices;
using Windows.Media.MediaProperties;
using Windows.Storage;

namespace Services
{
    public class CameraCapture : IDisposable
    {
        MediaCapture mediaCapture;
        ImageEncodingProperties imgEncodingProperties;
        MediaEncodingProfile videoEncodingProperties;

        public VideoDeviceController VideoDeviceController
        {
            get { return mediaCapture.VideoDeviceController; }
        }

        public async Task<MediaCapture> Initialize(CaptureUse primaryUse = CaptureUse.Photo)
        {
            // Create MediaCapture and init
            mediaCapture = new MediaCapture();
            await mediaCapture.InitializeAsync();
            mediaCapture.VideoDeviceController.PrimaryUse = primaryUse;

            mediaCapture.SetPreviewRotation(VideoRotation.Clockwise270Degrees);
            mediaCapture.SetRecordRotation(VideoRotation.Clockwise270Degrees);

            // Create photo encoding properties as JPEG and set the size that should be used for photo capturing
            imgEncodingProperties = ImageEncodingProperties.CreateJpeg();
            imgEncodingProperties.Width = 480;
            imgEncodingProperties.Height = 640;

            // Create video encoding profile as MP4 
            // videoEncodingProperties = MediaEncodingProfile.CreateMp4(VideoEncodingQuality.Vga);
            // Lots of properties for audio and video could be set here...

            return mediaCapture;
        }

        public async Task StartPreview()
        {
            // Start Preview stream
            await mediaCapture.StartPreviewAsync();
        }

        public async Task StopPreview()
        {
            // Stop Preview stream
            await mediaCapture.StopPreviewAsync();
        }

        public async Task<StorageFile> CapturePhoto(string desiredName = "photo.jpg")
        {
            // Create new unique file in the pictures library and capture photo into it
            var photoStorageFile = await KnownFolders.PicturesLibrary.CreateFileAsync(desiredName, CreationCollisionOption.GenerateUniqueName);
            await mediaCapture.CapturePhotoToStorageFileAsync(imgEncodingProperties, photoStorageFile);
            return photoStorageFile;
        }

        public void Dispose()
        {
            if (mediaCapture != null)
            {
                mediaCapture.Dispose();
                mediaCapture = null;
            }
        }
    }
}
