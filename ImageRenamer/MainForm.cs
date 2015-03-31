using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text.RegularExpressions;
using System.Drawing.Drawing2D;

namespace ImageRenamer
{
    public partial class MainForm : Form, 
        IMessageFilter, 
        ImageHolder.OnRackButtonListener, 
        ImageHolder.OnRackSubButtonListener, 
        ImageHolder.OnViewButtonListener
    {
        private String SelectedFolder = "";
        private List<ImageItem> mInitialFileList, mRenameFileList;
        private int ViewCount = 0;
        private int RackCount = 0;
        private int RackSubCount = 0;
        private const Boolean IsView = true;
        //private BackgroundWorker ImageLoader;
        //private WaitForm mWaitForm;
        private String FilePrefix = "";

        public MainForm()
        {
            InitializeComponent();
            // reduce flickering!
            DoubleBuffered = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
           
            mInitialFileList = new List<ImageItem>();
            mRenameFileList = new List<ImageItem>();

            //MouseWheel
            Application.AddMessageFilter(this);

            //ImageLoader = new BackgroundWorker();
            //ImageLoader.DoWork += new DoWorkEventHandler(this.ImageLoader_DoWork);

            //mWaitForm = new WaitForm();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            // These lines draw Gradient
            //LinearGradientBrush mBrush = new LinearGradientBrush(this.ClientRectangle, Color.WhiteSmoke, Color.SteelBlue, 90F);
            //e.Graphics.FillRectangle(mBrush, this.ClientRectangle);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            // We are starting our application in Maximized mode, but
            // resize event is not fired for some reason, so we need
            // to do this manually here to ensure proper layout arrangement.
            MainForm_Resize(sender, e);


        }

        // Performs dynamic arrangement of the UI controls
        private void MainForm_Resize(object sender, EventArgs e)
        {
            try
            {
                this.Invalidate();
                FlowImagePanel.Height = MainForm.ActiveForm.Height - FlowImagePanel.Top - 40;
                FlowImagePanel.Width = MainForm.ActiveForm.Width - FlowImagePanel.Left - 20;
                FlowButtonsPanel.Width = MainForm.ActiveForm.Width;
            }
            catch (Exception ex) { }

            //int HolderDifferential = MainForm.ActiveForm.Width % 262;
            //int HolderMultiplier = 0;
            //while (HolderMultiplier * 262 < MainForm.ActiveForm.Width) HolderMultiplier++;

            //FlowImagePanel.Margin = new Padding((HolderDifferential / HolderMultiplier));
            //MessageBox.Show(FlowImagePanel.Margin.ToString());
        }
        
       #region MouseWheel hack
        /*
         * [MouseWheel]
         *  Application.AddMessageFilter(this) is added in MainForm constructor
         *  which instructs that the Handler for this event is implemented in
         *  MainForm.
         *  The handler itself is PreFilterMessage method which is method
         *  inherited from IMessageFilter interface that our MainForm extends!
         *  
         *  http://stackoverflow.com/questions/479284/mouse-wheel-event-c
         */

                public bool PreFilterMessage(ref Message m)
                {
                    if (m.Msg == 0x20a)
                    {
                        // WM_MOUSEWHEEL, find the control at screen position m.LParam
                        Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                        IntPtr hWnd = WindowFromPoint(pos);
                        if (hWnd != IntPtr.Zero && hWnd != m.HWnd && Control.FromHandle(hWnd) != null)
                        {
                            SendMessage(hWnd, m.Msg, m.WParam, m.LParam);
                            return true;
                        }
                    }
                    return false;
                }

                // P/Invoke declarations
                [DllImport("user32.dll")]
                private static extern IntPtr WindowFromPoint(Point pt);
                [DllImport("user32.dll")]
                private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        #endregion


       #region MainForm UI interactions

        /*
         *  UI events
         */
        private void OpenFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog mDialog = new FolderBrowserDialog();
            mDialog.RootFolder = Environment.SpecialFolder.Desktop;
            mDialog.Description = "Избери папка със снимки";
            mDialog.ShowDialog();

            // No directory is selected
            if (mDialog.SelectedPath.Equals("")) {
                MessageBox.Show("Не сте избрали папка!");
            // Directory contains no files
            } else if (Directory.GetFiles(mDialog.SelectedPath).Length == 0) {
                MessageBox.Show("Избраната папка не съдържа файлове!");
            // Oh yes, everything is fine!
            } else { 
                SelectedFolder = mDialog.SelectedPath;
                updateWindowTitle(SelectedFolder);
                PopulateControls(SelectedFolder);
                //mWaitForm.Show();
                //ImageLoader.RunWorkerAsync();
            }
        }

        private void RenameButton_Click(object sender, EventArgs e)
        {
            // Disable clear button, because there's no turning back after rename.
            ClearSelectionButton.Enabled = false;
            int i = PerformRename();
            if (i > 0) MessageBox.Show(i + " файлове бяха преименувани!");

        }

        private void ClearSelection_Click(object sender, EventArgs e)
        {
            mRenameFileList.Clear();
            foreach (ImageHolder mImageHolder in FlowImagePanel.Controls)
            {
                mImageHolder.RestoreOriginalState();
            }
            
            // reset counters
            RackCount = 0; RackSubCount = 0; ViewCount = 0;
        }

        // Logic here is simple, if you change prefix, then the counters are reset.
        // This is useful when we have images for more than one room for example
        // and each room has different prefix!

        private void PrefixBox_Leave(object sender, EventArgs e)
        {   
            if (!PrefixBox.Text.Equals(FilePrefix))
            {
                RackCount = 0; RackSubCount = 0; ViewCount = 0;

                if (PrefixBox.Text.Count() > 0)
                {
                    PrefixBox.Text = Regex.Replace(PrefixBox.Text, @"\s+", "_");
                    FilePrefix = PrefixBox.Text + "_";
                }
                else FilePrefix = "";
            }
            
        }

       #endregion



        #region ImageHolder callbacks

        /*
         * 
         * ImagerHolder callback interface implementations
         * 
         */ 

        public void onRackButtonClick(ImageItem tItem, ImageHolder mReference) 
        {
            this.RackCount++;
            this.RackSubCount = 1;
            updateItem(tItem, mReference ,!IsView);
            
        }

        public void onRackSubButtonClick(ImageItem tItem, ImageHolder mReference)
        {
            if (RackCount == 0) RackCount++;
            this.RackSubCount++;
            updateItem(tItem, mReference, !IsView);
            
        }

        public void onViewButtonClick(ImageItem tItem, ImageHolder mReference)
        {
            this.ViewCount++;
            updateItem(tItem,mReference, IsView);
        }

        #endregion


       #region HelperFunctions
        /*
         * Functions
         */


        //private void ImageLoader_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    BackgroundWorker worker = sender as BackgroundWorker;
        //    PopulateControls(SelectedFolder);
        //}

        //private void ImageLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    mWaitForm.HideWaitForm();
        //}


        private void updateItem(ImageItem mItem, ImageHolder holder, Boolean isView) {

            if (isView) mItem.setNewName(FilePrefix, "View" + ViewCount + ".jpg");
            else mItem.setNewName(FilePrefix, RackCount + "." + RackSubCount + ".jpg");

            holder.RenameLabel(mItem.getNewName());
            mRenameFileList.Add(mItem);
        }

        private void PopulateControls(string FolderPath)
        {
            int i = 0;
            // Clear previously saved list
            mInitialFileList.Clear();

            // reset counters
            RackCount = 0; RackSubCount = 0; ViewCount = 0;

            // Remove previously loaded controls in the flow menu
            // in case someome clicks Open button consecutive times
            // and load images twice
            if (FlowImagePanel.Controls.Count > 0)
            {
                foreach (ImageHolder mHolder in FlowImagePanel.Controls)
                {
                    mHolder.Dispose();
                }
                FlowImagePanel.Controls.Clear();
            }

            // Load new stuff here!
            foreach (String filePath in Directory.GetFiles(FolderPath, "*.jpg"))
            {
                try {
                    mInitialFileList.Add(new ImageItem(filePath));
                    FlowImagePanel.Controls.Add(new ImageHolder(mInitialFileList[i],this));
                    i++;
                }
                catch (OutOfMemoryException e) { 
                    MessageBox.Show("Тръде много снимки. Не достига памет!");
                    break;
                }
                
            }

            // Enable clear button
            ClearSelectionButton.Enabled = true;
        }

        public int PerformRename()
        {
           int counter = 0;
           foreach (ImageItem mItem in mRenameFileList)
            {
        
                try
                {
                    File.Move(mItem.getOriginalPath(), mItem.getNewPath());
                    counter++;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Съществува файл с такова име: " + mItem.getNewPath());
                    counter--;
                }
            }
           mRenameFileList.Clear();
           return counter;
        }

        private void updateWindowTitle(string title)
        {
            MainForm.ActiveForm.Text = "ImageRenamer: " + title;
        }

        #endregion

      

        
    }
}
