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

namespace ImageRenamer
{
    public partial class MainForm : Form, IMessageFilter 
    {
        public String SelectedFolder = "";
        public List<ImageItem> mItemsList;
        public ImageList mImageList;
        public int ViewCount = 1;
        public int RackCount = 1;
        public int RackSubCount = 1;

        public MainForm()
        {
            InitializeComponent();
            //updateWindowTitle("няма избрана папка!");
            mItemsList = new List<ImageItem>();
            mImageList = new ImageList();
            mImageList.ImageSize = new Size(256, 256);
            mImageList.ColorDepth = ColorDepth.Depth32Bit;


            //MouseWheel
            Application.AddMessageFilter(this);
        }

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
                //BackgroundImageLoader.RunWorkerAsync();
                PopulateControls(SelectedFolder);
            }
        }

        private void RenameButton_Click(object sender, EventArgs e)
        {
            //int i = PerformRename();
            //if (i > 0) MessageBox.Show(i + " файлове бяха преименувани!");
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            // We are starting our application in Maximized mode, but
            // resize event is not fired for some reason, so we need
            // to do this manually here to ensure proper layout arrangement.
            MainForm_Resize(sender, e);
        }


        // Performs dynamic arrangement of the UI controls
        private void MainForm_Resize(object sender, EventArgs e)
        {
            FlowImagePanel.Height = MainForm.ActiveForm.Height - FlowImagePanel.Top - 40;
            FlowImagePanel.Width = MainForm.ActiveForm.Width - FlowImagePanel.Left - 20;
        }


        private void BackgroundImageLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            PopulateControls(SelectedFolder);
        }


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


        /*
         * 
         * Public methods that will be used outside of MainForm
         * 
         */

                public int getRackCount() { return this.RackCount; }
                public int getRackSubCount() { return this.RackSubCount; }
                public int getViewCount() { return this.ViewCount; }

                public void increaseRackCounter() { this.RackCount++; }
                public void increaseRackSubCounter() { this.RackSubCount++;  }
                public void increaseViewCounter() {this.ViewCount++; }

                public void updateItemsList(ImageItem mItem)
                {
                    int index;
                    for (index = 0; index < mItemsList.Count; index++)
                    {
                        if (mItemsList[index].getOriginalName().Equals(mItem.getOriginalName())) break;
                    }

                    mItemsList.RemoveAt(index);
                    mItemsList.Add(mItem);
                }

        /*
         * Functions
         */

        private void PopulateControls(string FolderPath)
        {
            int i = 0;
            // Clear previously saved list
            mItemsList.Clear();

            //// Show wait form.
            //WaitForm.StartWaitForm();
            foreach (String filePath in Directory.GetFiles(FolderPath, "*.jpg"))
            {

                try {
                    mItemsList.Add(new ImageItem(filePath));
                    mImageList.Images.Add(Image.FromFile(filePath));
                    FlowImagePanel.Controls.Add(new ImageHolder(mImageList.Images[i], mItemsList[i]));
                    i++;
                }
                catch (OutOfMemoryException e) { 
                    MessageBox.Show("Тръде много снимки. Не достига памет!");
                    break;
                }
                
            }

            ////Hide wait form.
            //WaitForm.HideWaitForm();
            //// WaitForm does not hide properly.. need more work.
        }

        public int PerformRename()
        {
            int counter = 0;
            String OriginalPath, NewPath;
            foreach (ImageItem mItem in mItemsList)
            {
                OriginalPath = mItem.getOriginalPath();
                NewPath = mItem.getNewPath();
                // skip identical items
                if (OriginalPath.Equals(NewPath)) continue;
                // rename others
                try
                {
                    File.Move(OriginalPath, NewPath);
                    counter++;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Съществува файл с такова име: " + NewPath);
                    counter--;
                }
                
            }

            return counter;
        }

        private void updateWindowTitle(string title)
        {
            MainForm.ActiveForm.Text = "ImageRenamer: " + title;
        }
        
    }
}
