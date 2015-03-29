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
        private int ViewCount = 1;
        private int RackCount = 1;
        private int RackSubCount = 1;
        private const Boolean IsView = true;
        private BackgroundWorker ImageLoader;
        private WaitForm mWaitForm;
        private String FilePrefix = "";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // We are starting our application in Maximized mode, but
            // resize event is not fired for some reason, so we need
            // to do this manually here to ensure proper layout arrangement.
            MainForm_Resize(sender, e);

            mInitialFileList = new List<ImageItem>();
            mRenameFileList = new List<ImageItem>();

            //MouseWheel
            Application.AddMessageFilter(this);

            ImageLoader = new BackgroundWorker();
            ImageLoader.DoWork += new DoWorkEventHandler(this.ImageLoader_DoWork);

            mWaitForm = new WaitForm();
        }


        // Performs dynamic arrangement of the UI controls
        private void MainForm_Resize(object sender, EventArgs e)
        {
            FlowImagePanel.Height = MainForm.ActiveForm.Height - FlowImagePanel.Top - 40;
            FlowImagePanel.Width = MainForm.ActiveForm.Width - FlowImagePanel.Left - 20;
            FlowButtonsPanel.Width = MainForm.ActiveForm.Width;
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
                //ImageLoader.RunWorkerAsync();
            }
        }

        private void RenameButton_Click(object sender, EventArgs e)
        {

            int i = PerformRename();
            if (i > 0) MessageBox.Show(i + " файлове бяха преименувани!");
        }

        // Logic here is simple, if you change prefix, then the counters are reset.
        // This is useful when we have images for more than one room for example
        // and each room has different prefix!

        private void PrefixBox_Leave(object sender, EventArgs e)
        {
            if (!PrefixBox.Text.Equals(FilePrefix))
            {
                RackCount = 1; RackSubCount = 1; ViewCount = 1;
                PrefixBox.Text = Regex.Replace(PrefixBox.Text, @"\s+", "_");
                
                if (PrefixBox.Text.Count() > 0) FilePrefix = PrefixBox.Text + "_";
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
            
            updateItem(tItem, mReference ,!IsView);
            this.RackCount++;
            this.RackSubCount = 1;
            //MessageBox.Show(RackCount.ToString() + " " + RackSubCount.ToString() + " " + ViewCount.ToString());
        }

        public void onRackSubButtonClick(ImageItem tItem, ImageHolder mReference)
        {
            updateItem(tItem,mReference , !IsView);
            this.RackSubCount++;
            //MessageBox.Show(RackCount.ToString() + " " + RackSubCount.ToString() + " " + ViewCount.ToString());
        }

        public void onViewButtonClick(ImageItem tItem, ImageHolder mReference)
        {
            updateItem(tItem,mReference, IsView);
            this.ViewCount++;
            //MessageBox.Show(RackCount.ToString() + " " + RackSubCount.ToString() + " " + ViewCount.ToString());
        }

        #endregion


       #region HelperFunctions
        /*
         * Functions
         */


        private void ImageLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            mWaitForm.Show();

            PopulateControls(SelectedFolder);
        }

        private void ImageLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mWaitForm.HideWaitForm();
        }


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
            foreach (String filePath in Directory.GetFiles(FolderPath, "*.jpg"))
            {

                try {
                    mInitialFileList.Add(new ImageItem(filePath));
                    FlowImagePanel.Controls.Add(new ImageHolder(mInitialFileList[i]));
                    
                    i++;
                }
                catch (OutOfMemoryException e) { 
                    MessageBox.Show("Тръде много снимки. Не достига памет!");
                    break;
                }
                
            }
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
