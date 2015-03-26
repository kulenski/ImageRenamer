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

namespace ImageRenamer
{
    public partial class MainForm : Form, IMessageFilter 
    {
        public String SelectedFolder = "";
        public List<MyFileItem> mItemsList;
        public ImageList mImageList;

        public class MyFileItem
        {
            public String OriginalName;
            public String Path;
            public String NewName;
            public int ImageListId = -1;

            public MyFileItem(String original, String path, String newname)
            {
                this.OriginalName = original;
                this.Path = path;
                this.NewName = newname;
            }

        }

        public MainForm()
        {
            InitializeComponent();
            //updateWindowTitle("няма избрана папка!");
            mItemsList = new List<MyFileItem>();
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
                //PopulateItemList(SelectedFolder);
                //PopulateCheckBoxList();
                //LoadThumbnails(SelectedFolder);

                PopulateControls(SelectedFolder);
            }
        }

        private void RenameButton_Click(object sender, EventArgs e)
        {
            int i = PerformRename();
            if (i > 0) MessageBox.Show(i + " файлове бяха преименувани!");
        }

        private void imagesCheckBoxList_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            imagesCheckBoxList.Height = MainForm.ActiveForm.Height - imagesCheckBoxList.Top - 40;
            FlowImagePanel.Height = MainForm.ActiveForm.Height - FlowImagePanel.Top - 50;
            FlowImagePanel.Width = MainForm.ActiveForm.Width - FlowImagePanel.Left - 20;
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
         * Functions
         */

        private void PopulateControls(string FolderPath)
        {
            String OriginalFileName, NewFileName;
            int i = 0;
            // Clear previously saved list
            mItemsList.Clear();

            // Show wait form.
            WaitForm.StartWaitForm();
            foreach (String filePath in Directory.GetFiles(FolderPath, "*.jpg"))
            {

                OriginalFileName = Path.GetFileName(filePath);
                NewFileName = i + ".jpg";
                mItemsList.Add(new MyFileItem(OriginalFileName, filePath, NewFileName));
                
                mImageList.Images.Add(Image.FromFile(filePath));
                FlowImagePanel.Controls.Add(new ImageHolder(mImageList.Images[i],OriginalFileName));
                imagesCheckBoxList.Items.Add(NewFileName + "(" + OriginalFileName + ")");
                
                i++;
            }

            //Hide wait form.
            //WaitForm.HideWaitForm();
            // WaitForm does not hide properly.. need more work.
        }

        //private void PopulateItemList(string FolderPath)
        //{

        //    String OriginalFileName,NewFileName;
        //    int i = 0;
        //    // Clear previously saved list
        //    mItemsList.Clear();
        //    foreach (String filePath in Directory.GetFiles(FolderPath, "*.jpg"))
        //    {
        //        Image mImage = Image.FromFile(filePath);
        //        mImageList.Images.Add(mImage);

        //        OriginalFileName = Path.GetFileName(filePath);
        //        NewFileName = i + ".jpg";
        //        mItemsList.Add(new MyFileItem(OriginalFileName, filePath, NewFileName));
        //        i++;
                
        //    }
        //}

        //private void PopulateCheckBoxList()
        //{
        //    if (mItemsList.Count > 0)
        //    {
        //        imagesCheckBoxList.Items.Clear();
        //        foreach (MyFileItem temp in mItemsList) 
        //            imagesCheckBoxList.Items.Add(temp.NewName);
        //    }
           
        //}

        //private void LoadThumbnails(String FolderName)
        //{
        //    foreach (Image mImage in mImageList.Images)
        //        FlowImagePanel.Controls.Add(new ImageHolder(mImage));
        //}

        public int PerformRename()
        {
            int counter = 0;
            String OriginalPath, NewPath;
            foreach (MyFileItem mItem in mItemsList)
            {
                // skip identical items
                if (mItem.OriginalName.Equals(mItem.NewName)) continue;
                // rename others
                OriginalPath = Directory.GetParent(mItem.Path) + "\\" + mItem.OriginalName;
                NewPath = Directory.GetParent(mItem.Path) + "\\" + mItem.NewName;
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
