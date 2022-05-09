using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace C64.FrontEnd.Extensions
{
    public static class ByteArrayExtensions
    {
        public static byte[] ResizeImage(this byte[] imageBytes, double scale)
        {
            using (var source = Image.Load(imageBytes))
            {
                var newSize = new Size(source.Width, source.Height);
                newSize.Width = (int)(source.Width * scale);
                newSize.Height = (int)(source.Height * scale);
                source.Mutate(p => p.Resize(newSize));
                using (var saveStream = new MemoryStream())
                {
                    source.Save(saveStream, new PngEncoder());
                    return saveStream.ToArray();
                }
            }
        }
    }
}