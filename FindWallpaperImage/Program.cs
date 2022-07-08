using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Text;

namespace FindWallpaperImage
{
    class Program
    {

        static void Main(string[] args)
        {
            OpenWallpaperDestination();
        }

        static byte[] Slice(byte[] source, int pos)
        {
            byte[] result = new byte[source.Length - pos];
            Array.Copy(source, pos, result, 0, result.Length);
            return result;
        }

        private static void OpenWallpaperDestination()
        {
            try
            {
                // Найти путь к изображению
                using (var key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop"))
                {
                    var path = (byte[])key.GetValue("TranscodedImageCache");

                    var wallpaperFile = Encoding.Unicode.GetString(Slice(path, 24)).TrimEnd("\0".ToCharArray());

                    // Открыть папку и выделить файл
                    Process.Start("explorer.exe", "/select," + wallpaperFile);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка, возможно используеся неподдерживаемая версия Windows (поддерживается Windows 10). \n" 
                    + e.Message);
            }
        }
    }
}
