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
        private OnRackButtonListener mRackButtonListener;
        private OnRackSubButtonListener mRackSubButtonListener;
        private OnViewButtonListener mViewButtonListener;

        public ImageHolder(Image mImage, ImageItem mItem)
        {
            InitializeComponent();

            if (mImage != null) PictureBox.Image = mImage;
            if (mItem != null)
            {
                OriginalName.Text = mItem.getOriginalName();
                this.currentItem = mItem;
            }

            this.mRackButtonListener = (OnRackButtonListener)MainForm.ActiveForm;
            this.mRackSubButtonListener = (OnRackSubButtonListener)MainForm.ActiveForm;
            this.mViewButtonListener = (OnViewButtonListener)MainForm.ActiveForm;
            
        }

        //dummy
        private void RackButton_Click(object sender, EventArgs e)
        {
            mRackButtonListener.onRackButtonClick(currentItem);
            updateItem();
        }
        
        //dummy
        private void ViewButton_Click(object sender, EventArgs e)
        {
            mViewButtonListener.onViewButtonClick(currentItem);
            updateItem();
        }

        //dummy
        private void RackSubButton_Click(object sender, EventArgs e)
        {
            mRackSubButtonListener.onRackSubButtonClick(currentItem);
            updateItem();
        }

        private void updateItem()
        {
            
            RackButton.Enabled = false;
            RackSubButton.Enabled = false;
            ViewButton.Enabled = false;

            RackButton.Visible = false;
            RackSubButton.Visible = false;
            ViewButton.Visible = false;
        }

        /*
         * 
         *  Callback interface
         * 
         */

        public interface OnRackButtonListener { void onRackButtonClick(ImageItem mItem); }
        public interface OnRackSubButtonListener { void onRackSubButtonClick(ImageItem mItem); }
        public interface OnViewButtonListener { void onViewButtonClick(ImageItem mItem); }
       
    }
}
