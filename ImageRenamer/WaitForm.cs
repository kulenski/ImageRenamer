using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ImageRenamer
{
    public partial class WaitForm : Form
    {
        static Thread mThread = null;
        static WaitForm mWaitForm = null;

        public WaitForm()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        static private void ShowWaitForm() {
            mWaitForm = new WaitForm();
            Application.Run(mWaitForm);
        }

        static public void StartWaitForm()
        {
            if (mWaitForm != null) return;
            mThread = new Thread(new ThreadStart(WaitForm.ShowWaitForm));
            mThread.IsBackground = true;
            mThread.SetApartmentState(ApartmentState.STA);
            mThread.Start();

            while (mWaitForm == null || mWaitForm.IsHandleCreated == false)
            {
                System.Threading.Thread.Sleep(50);
            }
        }

        static public void HideWaitForm()
        {
            mWaitForm.Close();
            mThread = null;
            mWaitForm = null;
        }

    }
}
