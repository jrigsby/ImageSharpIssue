using System.IO;
using ImageSharp;
using ImageSharp.Formats;
using ImageSharp.Processing;

namespace Tcr.Sage.Services.BusinessServices {
   public class ImageHelper {
      public Stream ScaleLogo2(Stream file, float maxWidthInInches, float maxHeightInInches) {
         ImageSharp.Configuration.Default.AddImageFormat(new JpegFormat());

         var image = new ImageSharp.Image(file);

         var maxWidthInPixels = maxWidthInInches * image.MetaData.HorizontalResolution; //(image.Width / image.InchWidth);
         var maxHeightInPixels = maxHeightInInches * image.MetaData.VerticalResolution; //(image.Height / image.InchHeight);

         image.Resize(new ResizeOptions {
            Size = new ImageSharp.Size((int)maxWidthInPixels, (int)maxHeightInPixels),
            Mode = ResizeMode.Max
         });
         image.MetaData.Quality = 100;
         image.MetaData.ExifProfile = null;
         image.MetaData.HorizontalResolution = 72;
         image.MetaData.VerticalResolution = 72;

         var memStream = new MemoryStream();
         //image.SaveAsJpeg(memStream, new JpegEncoderOptions() { IgnoreMetadata = false, Quality = 100});
         image.Save(memStream);
         //using (var output = File.OpenWrite("c:\\temp\\output.jpg")) {
         //   image.Save(output);
         //}

         memStream.Position = 0;
         return memStream;
      }
   }
}
