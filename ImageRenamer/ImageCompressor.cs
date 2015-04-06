using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace eu.kulenski.appkitchen.ImageRenamer {
    public static class ImageCompressor {

        public static void Compress(String filepath) { }
        public static int doCompressTest(String filepath) { return 0;  }

        // MSDN reference url: https://msdn.microsoft.com/en-us/library/bb882583(v=vs.110).aspx
        private ImageCodecInfo GetEncoder(ImageFormat format) {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs) {
                if (codec.FormatID == format.Guid) {
                    return codec;
                }
            }
            return null;
        }

    }
}
