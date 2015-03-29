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

            // Make shade transparent
            this.ShadeLayer.BackColor = Color.FromArgb(0, Color.Black);

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
            updateItem();
            mRackButtonListener.onRackButtonClick(currentItem, this);
            // MainForm handles the MouseWheel event properly,
            // but when we click on some of the buttons the
            // focus is being moved to ImageHolder and
            // MouseWheel stop working. So we focus back to MainForm
            // so we can have consistent MouseWheel event behavior.
            MainForm.ActiveForm.Focus();
            
        }
        
        //dummy
        private void ViewButton_Click(object sender, EventArgs e)
        {
            updateItem();
            mViewButtonListener.onViewButtonClick(currentItem, this);
            // We need this for MouseWheel event handling. See above remark.
            MainForm.ActiveForm.Focus();
            
        }

        //dummy
        private void RackSubButton_Click(object sender, EventArgs e)
        {
            updateItem();
            mRackSubButtonListener.onRackSubButtonClick(currentItem, this);
            // We need this for MouseWheel event handling. See above remark.
            MainForm.ActiveForm.Focus();
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

        public void RenameLabel(string newName) 
        {
            OriginalName.Text = newName;
        }

        /*
         * 
         *  Callback interface
         * 
         */

        public interface OnRackButtonListener { void onRackButtonClick(ImageItem mItem, ImageHolder reference); }
        public interface OnRackSubButtonListener { void onRackSubButtonClick(ImageItem mItem, ImageHolder reference); }
        public interface OnViewButtonListener { void onViewButtonClick(ImageItem mItem, ImageHolder reference); }

        // MUST fix not placing in center behavior and add dynamic resize of the ImageHolder,
        // because prefix names can be broader than the width of the ImageHolder
        // and in this case the label should expand the text and resize the Holder.
        private void OriginalName_TextChanged(object sender, EventArgs e)
        {
            
            OriginalName.Left = ((200 - OriginalName.Width) / 2);
        }
       
    }
}
