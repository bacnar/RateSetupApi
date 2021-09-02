using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;
using System.IO;

namespace RateSetup.Helpers.ImageHelpers
{
    public static class ImageResizing
    {
        public static Stream ResizeImage(int height, int width, Stream imageStream, IResampler resampler)
        {
            using (var outStream = new MemoryStream())
            using (var image = Image.Load(imageStream, out IImageFormat format))
            {
                image.Mutate(x => x.Resize(width, height, resampler /*KnownResamplers.NearestNeighbor*/));

                image.Save(outStream, format);

                return outStream;
            }
        }
    }
}
