using System.IO;
using ImageSharp;
using ImageSharp.Formats;
using ImageSharp.Processing;

namespace Tcr.Sage.Services.BusinessServices {
   public class ImageHelper {
      public Stream ScaleLogo2(Stream file, float maxWidthInInches, float maxHeightInInches) {
         ImageSharp.Configuration.Default.AddImageFormat(new JpegFormat());

         var image = new ImageSharp.Image(file);

         var maxWidthInPixels = maxWidthInInches * image.MetaData.HorizontalResolution;
         var maxHeightInPixels = maxHeightInInches * image.MetaData.VerticalResolution;

         image.Resize(new ResizeOptions {
            Size = new ImageSharp.Size((int)maxWidthInPixels, (int)maxHeightInPixels),
            Mode = ResizeMode.Max
         });
         image.MetaData.Quality = 100;
         image.MetaData.ExifProfile = null;

         var memStream = new MemoryStream();
         
         image.Save(memStream);

         memStream.Position = 0;
         return memStream;
      }
   }
}
