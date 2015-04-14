using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using System.Collections;
using System.IO;

namespace eu.kulenski.appkitchen.ImageRenamer {
    public static class ImageCompressor {

        // Compression ratio to quality lookup
        //http://www.graphicsmill.com/blog/2014/11/06/Compression-ratio-for-different-JPEG-quality-values#.VS1dU_msXDs
        private static Dictionary<int, double> mQualityRatioLookup = new Dictionary<int, double>() {
            {55,43.27},
            {60,36.90},
            {65,34.24},
            {70,31.50},
            {75,26.00},
            {80,25.06},
            {85,19.08},
            {90,14.30},
            {95,9.88}
        };
       
        public static void Compress(ImageItem mItem, String compressFolder) {
            EncoderParameters mParams = new EncoderParameters(1);
            mParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 80L);

            Bitmap mImage = new Bitmap(mItem.getNewPath());
            ImageCodecInfo mEncoder = GetEncoder(mImage.RawFormat);

            String compressPath  = GetCompressPath(compressFolder,mItem.getNewPath());
            mImage.Save(compressPath + "\\" + mItem.getNewName(),mEncoder,mParams);
            mImage.Dispose();
        }

        private static String GetCompressPath(String mFolderName, String mFilepath) {
            String mParentDir = Directory.GetParent(mFilepath).ToString();
            String mCompressDir = mParentDir + "\\" + mFolderName;

            if(!Directory.Exists(mCompressDir)) {
                Directory.CreateDirectory(mCompressDir);
            }

            return mCompressDir;
        }

        // MSDN reference url: https://msdn.microsoft.com/en-us/library/bb882583(v=vs.110).aspx
        private static ImageCodecInfo GetEncoder(ImageFormat format) {
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
