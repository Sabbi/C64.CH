using D64Reader;
using D64Reader.Renderers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace C64.FrontEnd.Helpers
{
    public class CustomD64Renderer : ID64Renderer<byte[]>
    {
        public byte[] Render(D64Directory directory)
        {
            var directoryContents = DirectoryContents(directory);

            using (var image = new Image<Rgba32>(240, 8 * directoryContents.Count()))
            {
                image.Mutate(p =>
                {
                    p.Fill(Color.FromRgb(80, 69, 155));

                    var data = Convert.FromBase64String(charsEncoded);

                    var charsImage = Image.Load<Rgba32>(data);

                    var current = 0;
                    foreach (var line in directoryContents)
                    {
                        for (var i = 0; i < line.Length; i++)
                        {
                            var source = PetsciiToScreenCode((byte)line[i]);

                            var srcRectangle = new Rectangle(8 * (source % 16), 8 * (source / 16), 8, 8);
                            var dstRectangle = new Rectangle(8 * i, 8 * current, 8, 8);

                            if (current == 0 && i > 1)
                                srcRectangle.Y += 64;

                            var toPlot = Extract(charsImage, srcRectangle);

                            p.DrawImage(toPlot, new Point((int)8 * i, (int)8 * current), 1.0f);
                        }
                        current++;
                    }

                    charsImage.Dispose();
                });

                using (var stream = new MemoryStream())
                {
                    image.SaveAsPng(stream);
                    return stream.ToArray();
                }
            }
        }

        private static Image<Rgba32> Extract(Image<Rgba32> sourceImage, Rectangle sourceArea)
        {
            Image<Rgba32> targetImage = new(sourceArea.Width, sourceArea.Height);
            int height = sourceArea.Height;
            sourceImage.ProcessPixelRows(targetImage, (sourceAccessor, targetAccessor) =>
            {
                for (int i = 0; i < height; i++)
                {
                    Span<Rgba32> sourceRow = sourceAccessor.GetRowSpan(sourceArea.Y + i);
                    Span<Rgba32> targetRow = targetAccessor.GetRowSpan(i);

                    sourceRow.Slice(sourceArea.X, sourceArea.Width).CopyTo(targetRow);
                }
            });

            return targetImage;
        }

        private byte PetsciiToScreenCode(byte source)
        {
            if (source < 32) source += 128;
            else if (source >= 64 && source < 96) source -= 64;
            else if (source >= 96 && source < 128) source -= 32;
            else if (source >= 128 && source < 160) source += 64;
            else if (source >= 160 && source < 192) source -= 64;
            else if (source >= 192 && source < 255) source -= 128;
            else if (source == 255) source = 94;

            return source;
        }

        private IEnumerable<string> DirectoryContents(D64Directory directory)
        {
            var retVal = new List<string>();
            retVal.Add($"0 \"{directory.DiskName}\" {directory.DiskId}");

            foreach (var item in directory.DirectoryItems)
            {
                var entry = (item.Blocks.ToString().PadRight(5) + $"\"{item.Name}\"").PadRight(24) + $"{item.Type}";
                retVal.Add(entry);
            }

            retVal.Add($"{directory.FreeBlocks} BLOCKS FREE.");

            return retVal;
        }

        private string charsEncoded = "iVBORw0KGgoAAAANSUhEUgAAAIAAAACACAIAAABMXPacAAAEs2lUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4KPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iWE1QIENvcmUgNS41LjAiPgogPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4KICA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIgogICAgeG1sbnM6ZXhpZj0iaHR0cDovL25zLmFkb2JlLmNvbS9leGlmLzEuMC8iCiAgICB4bWxuczp0aWZmPSJodHRwOi8vbnMuYWRvYmUuY29tL3RpZmYvMS4wLyIKICAgIHhtbG5zOnBob3Rvc2hvcD0iaHR0cDovL25zLmFkb2JlLmNvbS9waG90b3Nob3AvMS4wLyIKICAgIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIKICAgIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIgogICAgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIKICAgZXhpZjpQaXhlbFhEaW1lbnNpb249IjEyOCIKICAgZXhpZjpQaXhlbFlEaW1lbnNpb249IjEyOCIKICAgZXhpZjpDb2xvclNwYWNlPSIxIgogICB0aWZmOkltYWdlV2lkdGg9IjEyOCIKICAgdGlmZjpJbWFnZUxlbmd0aD0iMTI4IgogICB0aWZmOlJlc29sdXRpb25Vbml0PSIyIgogICB0aWZmOlhSZXNvbHV0aW9uPSI5Ni4wIgogICB0aWZmOllSZXNvbHV0aW9uPSI5Ni4wIgogICBwaG90b3Nob3A6Q29sb3JNb2RlPSIzIgogICBwaG90b3Nob3A6SUNDUHJvZmlsZT0ic1JHQiBJRUM2MTk2Ni0yLjEiCiAgIHhtcDpNb2RpZnlEYXRlPSIyMDIx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";
    }
}