using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace eu.kulenski.appkitchen.ImageRenamer
{
    public partial class WaitForm : Form
    {
        static WaitForm mInstance = null;

        public WaitForm()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            mInstance = this;
        }

        public void ShowWaitForm() {  mInstance.Show(); }

        public void HideWaitForm() {  mInstance.Close(); }

    }
}
