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
    public partial class ImageHolder : UserControl
    {
        public ImageHolder(Image mImage, ImageItem mItem)
        {
            InitializeComponent();

            if (mImage != null) PictureBox.Image = mImage;
            if (mItem != null) OriginalName.Text = mItem.getOriginalName();
        }


        private void RackButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Rack");
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("View");
        }

        private void RackSubButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("RackSub");
        }


 
       
    }
}
