using System;
using System.Globalization;
using System.IO;
using Microsoft.Maui.Controls;

namespace PM2E2GRUPO3.Config
{
    internal class Base64ToVideoConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            string base64Video = (string)value;
            byte[] videoBytes = System.Convert.FromBase64String(base64Video);

            // Guardar el video en un archivo temporal
            string tempFilePath = Path.Combine(FileSystem.CacheDirectory, $"{Guid.NewGuid()}.mp4");
            File.WriteAllBytes(tempFilePath, videoBytes);

            // Devolver la ruta del archivo
            return tempFilePath;
            
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
