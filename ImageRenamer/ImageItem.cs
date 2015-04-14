using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eu.kulenski.appkitchen.ImageRenamer {

    public partial class ImageItem {

        private String OriginalName;
        private String OriginalPath;
        private String NewName;
        private String NewPath;

        public ImageItem(String filepath) {
            if (filepath == null) throw new ArgumentNullException("Path cannot be null!");
            this.OriginalPath = filepath;
            this.OriginalName = System.IO.Path.GetFileName(filepath);
            this.NewName = this.OriginalName;
        }

        public String getNewName() { return this.NewName; }

        public void setNewName(String prefix, String newName) {
            if (newName == null || prefix == null) throw new ArgumentNullException("New name cannot be null");
            this.NewName = prefix + newName;
            this.NewPath = System.IO.Directory.GetParent(this.OriginalPath) + "\\" + this.NewName;
        }

        public String getNewPath() { return this.NewPath; }

        public String getOriginalPath() { return this.OriginalPath; }

        public String getOriginalName() { return this.OriginalName; }

        public Bitmap getThumbnail(int x, int y) {
            Bitmap mImage = new Bitmap(this.OriginalPath);
            Bitmap mThumbnail = new Bitmap(mImage, new Size(x, y));
            mImage.Dispose();
            return mThumbnail;
        }


    }
}
