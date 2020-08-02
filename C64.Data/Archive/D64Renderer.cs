//using C64.Data.Properties;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.IO;

//namespace C64.Data.Archive
//{
//    public class D64Renderer
//    {
//        private readonly D64ReaderCore d64Reader;

//        public D64Renderer(D64ReaderCore d64Reader)
//        {
//            this.d64Reader = d64Reader;
//        }

//        public byte[] GeneratePng()
//        {
//            var formattedDir = FormattedDirectory();
//            using (var image = new Bitmap(240, 8 * formattedDir.Count))
//            {
//                var chars = LoadFromByteArray(Resources.base_png);
//                var charsInv = LoadFromByteArray(Resources.baseinv_png);

//                using (var graphics = Graphics.FromImage(image))
//                {
//                    graphics.Clear(Color.FromArgb(80, 69, 155));

//                    var current = 0;
//                    foreach (var line in formattedDir)
//                    {
//                        for (var i = 0; i < line.Length; i++)
//                        {
//                            var source = line[i];

//                            var srcRectangle = new Rectangle(8 * (source % 32), 8 * (source / 32), 8, 8);
//                            var dstRectangle = new Rectangle(8 * i, 8 * current, 8, 8);

//                            graphics.DrawImage(current == 0 && i > 1 ? charsInv : chars, dstRectangle, srcRectangle, GraphicsUnit.Pixel);
//                        }
//                        current++;
//                    }
//                }

//                using (var stream = new MemoryStream())
//                {
//                    chars.Dispose();
//                    charsInv.Dispose();
//                    image.Save(stream, ImageFormat.Png);
//                    return stream.ToArray();
//                }
//            }
//        }

//        private Bitmap LoadFromByteArray(byte[] data)
//        {
//            using (var ms = new MemoryStream(data))
//            {
//                return new Bitmap(ms);
//            }
//        }

//        private ICollection<string> FormattedDirectory()
//        {
//            var retVal = new List<string>();

//            retVal.Add($"0 \"{D64Reader.DiskName}\" {D64Reader.DiskId}");

//            foreach (var item in 64Reader.DirectoryItems)
//            {
//                var entry = item.Blocks.ToString().PadRight(5) + $"\"{item.Name}\" {item.Type}";
//                retVal.Add(entry);
//            }

//            retVal.Add($"{D64Reader.FreeBlocks} BLOCKS FREE.");

//            return retVal;
//        }
//    }
//}