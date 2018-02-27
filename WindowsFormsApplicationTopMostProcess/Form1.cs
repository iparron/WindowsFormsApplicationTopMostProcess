using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplicationTopMostProcess
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        public static int GWL_STYLE = -16;
        public static int WS_CHILD = 0x40000000;

        public Form1()
        {
            InitializeComponent();
        }              

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hecho");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Process.Start("calc.exe");
            Process calc = Process.GetProcessesByName("calc")[0];
            if (calc != null)
            {
                Hide();
                FormBorderStyle = FormBorderStyle.None;
                SetBounds(0, 0, 0, 0, BoundsSpecified.Location);

                IntPtr hostHandle = calc.MainWindowHandle;
                IntPtr guestHandle = this.Handle;
                SetWindowLong(guestHandle, GWL_STYLE, GetWindowLong(guestHandle, GWL_STYLE) | WS_CHILD);
                SetParent(guestHandle, hostHandle);

                Show();
            }
        }
    }
}
