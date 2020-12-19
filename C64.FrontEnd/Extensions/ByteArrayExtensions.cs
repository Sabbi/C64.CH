using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace C64.FrontEnd.Extensions
{
    public static class ByteArrayExtensions
    {
        public static byte[] ResizeImage(this byte[] imageBytes, double scale)
        {
            using (var source = new Bitmap(new MemoryStream(imageBytes)))
            {
                using (var destination = new Bitmap((int)(source.Width * scale), (int)(source.Height * scale)))
                {
                    using (var graphics = Graphics.FromImage(destination))
                    {
                        var srcRectangle = new Rectangle(0, 0, source.Width, source.Height);
                        var dstRectangle = new Rectangle(0, 0, destination.Width, destination.Height);

                        graphics.DrawImage(source, dstRectangle, srcRectangle, GraphicsUnit.Pixel);
                    }

                    using (var stream = new MemoryStream())
                    {
                        destination.Save(stream, GetEncoder(ImageFormat.Png), null);
                        return stream.ToArray();
                    }
                }
            }
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                    return codec;
            }
            return null;
        }
    }
}