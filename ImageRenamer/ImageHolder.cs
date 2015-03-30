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
        private MainForm mParentForm;
        
        public ImageHolder(ImageItem mItem, MainForm mInstance)
        {
            InitializeComponent();

            mParentForm = mInstance;
                   
            if (mItem != null)
            {
                HolderLabel.Text = mItem.getOriginalName();
                this.currentItem = mItem;
                LoadImage();
            }

            // Assign listeners
            this.mRackButtonListener = (OnRackButtonListener)mParentForm;
            this.mRackSubButtonListener = (OnRackSubButtonListener)mParentForm;
            this.mViewButtonListener = (OnViewButtonListener)mParentForm; 
        }


        private void LoadImage()
        {
            Bitmap mBitmap = new Bitmap(currentItem.getOriginalPath());
            Bitmap resizedBitmap = new Bitmap(mBitmap, new Size(256, 256));
            PictureBox.Image = resizedBitmap;
            mBitmap.Dispose();
        }

      
        private void RackButton_Click(object sender, EventArgs e)
        {
            mRackButtonListener.onRackButtonClick(currentItem, this);
            setButtonsEnabled(false);

            // MainForm handles the MouseWheel event properly,
            // but when we click on some of the buttons the
            // focus is being moved to ImageHolder and
            // MouseWheel stop working. So we focus back to MainForm
            // so we can have consistent MouseWheel event behavior.
            mParentForm.Focus();            
        }

        private void RackSubButton_Click(object sender, EventArgs e)
        {
            mRackSubButtonListener.onRackSubButtonClick(currentItem, this);
            setButtonsEnabled(false);
            
            // We need this for MouseWheel event handling. See above remark.
            mParentForm.Focus();
        }
  
        private void ViewButton_Click(object sender, EventArgs e)
        {
            mViewButtonListener.onViewButtonClick(currentItem, this);
            setButtonsEnabled(false);
            
            // We need this for MouseWheel event handling. See above remark.
            mParentForm.Focus();
            
        }

        private void setButtonsEnabled(Boolean state)
        {
            RackButton.Enabled = state;
            RackSubButton.Enabled = state;
            ViewButton.Enabled = state;

            RackButton.Visible = state;
            RackSubButton.Visible = state;
            ViewButton.Visible = state;

            if (state)
            {
                this.BackColor = Color.FromArgb(64,64,64);
                HolderLabel.ForeColor = Color.White;
            }
            else { 
                this.BackColor = Color.LightGray;
                HolderLabel.ForeColor = Color.DimGray;
            }
        }


        /*
         * 
         *  Public methods that are used for calling from the parent form or control 
         * 
         */

        public void RenameLabel(string newName) { HolderLabel.Text = newName; }

        public void RestoreOriginalState()
        {
            setButtonsEnabled(true);
            HolderLabel.Text = currentItem.getOriginalName();
            
        }


        /*
         * 
         *  Callback interface
         * 
         */

        public interface OnRackButtonListener { void onRackButtonClick(ImageItem mItem, ImageHolder reference); }
        public interface OnRackSubButtonListener { void onRackSubButtonClick(ImageItem mItem, ImageHolder reference); }
        public interface OnViewButtonListener { void onViewButtonClick(ImageItem mItem, ImageHolder reference); }

        
       
    }
}
