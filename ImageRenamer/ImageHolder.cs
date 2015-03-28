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
        private ImageItem currentItem;

        public ImageHolder(Image mImage, ImageItem mItem)
        {
            InitializeComponent();

            if (mImage != null) PictureBox.Image = mImage;
            if (mItem != null)
            {
                OriginalName.Text = mItem.getOriginalName();
                this.currentItem = mItem;
            }
            
        }

        //dummy
        private void RackButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Rack");
        }
        
        //dummy
        private void ViewButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("View");
        }

        //dummy
        private void RackSubButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("RackSub");
            
        }
       
    }
}
