using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;

namespace edok_kiosk.Utility
{
    public static class ImageHelper
    {
        // Refined method for byte array to BitmapImage conversion
        public static BitmapImage GetImage(byte[] image) =>
            image == null ? GetDefaultImage() : LoadImageFromBytes(image);

        private static BitmapImage LoadImageFromBytes(byte[] image)
        {
            using var stream = new MemoryStream(image);
            var outputImage = new BitmapImage();
            outputImage.BeginInit();
            outputImage.StreamSource = stream;
            outputImage.CacheOption = BitmapCacheOption.OnLoad;
            outputImage.EndInit();
            return outputImage;
        }

        // Method for loading image from a path, with an elegance check for existence
        public static BitmapImage GetImageFromPath(string path) =>
            File.Exists(path) ? new BitmapImage(new Uri($"pack://application:,,,{path}")) : GetDefaultImage();

        // Method for creating an image byte array from a file path, with a touch of sophistication in error handling
        public static byte[]? CreateImage(string path)
        {
            if (!File.Exists(path)) return null;
            try
            {
                return File.ReadAllBytes(path);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                return null;
            }
        }

        // Widescreen aspect ratio check adhering to company standards
        public static bool IsWidescreenAspectRatio(BitmapImage bitmapImage) =>
            Math.Abs(GetAspectRatio(bitmapImage) - 1.7778) <= 0.05;

        // Square aspect ratio check
        public static bool IsEqualAspectRatio(BitmapImage bitmapImage) =>
            Math.Abs(GetAspectRatio(bitmapImage) - 1.0) <= 0.01;

        // Private helper to encapsulate aspect ratio calculation
        private static double GetAspectRatio(BitmapImage bitmapImage) =>
            (double)bitmapImage.PixelWidth / bitmapImage.PixelHeight;

        // Elegant error display method
        private static void ShowError(string message) =>
            MessageBox.Show(message, "Image Processing Error", MessageBoxButton.OK, MessageBoxImage.Error);

        // Loads the default image elegantly
        private static BitmapImage GetDefaultImage() =>
            new BitmapImage(new Uri("pack://application:,,,/Assets/default-pfp.png"));
    }
}
