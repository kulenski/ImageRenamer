using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageRenamer
{
    public partial class ImageHolder : Label
    {
        public ImageHolder(Image mImage, string mTitle)
        {
            InitializeComponent();
            this.Height = 280;
            this.Width = 256;
            this.TextAlign = ContentAlignment.BottomCenter;
            this.BackColor = Color.Transparent;
            
            if (mImage == null) throw new NullReferenceException();
            if (mTitle == null) throw new NullReferenceException();

            this.Image = mImage;
            this.Text = mTitle;
        }
    }
}
