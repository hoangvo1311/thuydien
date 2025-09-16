using IndusG.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IndusG.FormNotification
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            while (true)
            {
                var service = new PlcService();
                bool plcRunning;
                try
                {
                    plcRunning = service.GetCurrentPLCStatus();
                } catch
                {
                    plcRunning = service.CheckPLCAvailable();
                }


                if (plcRunning)
                {
                    this.notifyIcon1.Text = "IndusG Solutions: Kết nối PLC thành công";
                    this.notifyIcon1.Icon = new Icon(this.GetType(), "GreenAlert.ico");
                } else
                {
                    this.notifyIcon1.Text = "IndusG Solutions: Kết nối PLC gặp vấn đề!";
                    this.notifyIcon1.Icon = new Icon(this.GetType(), "RedAlert.ico");
                }

                System.Threading.Thread.Sleep(5000);
            }
        }


        /// <summary>
        /// This is the form move event.  We are causing the form to not show up on the
        /// task bar when minimized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Move(object sender, EventArgs e)
        {
            //This code causes the form to not show up on the task bar only in the tray.
            //NOTE there is now a form property that will allow you to keep the application
            //from every showing up in the task bar.
            if (this == null)
            { //This happen on create.
                return;
            }
            //If we are minimizing the form then hide it so it doesn't show up on the task bar
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.ShowBalloonTip(3000, "IndusG Solutions",
                    "IndusG Solutions Notification has be moved to the tray.",
                    ToolTipIcon.Info);
            }
            else
            {//any other windows state show it.
                this.Show();
            }

        }

        /// <summary>
        /// This is the form closing event.  You have the opportunity to keep the form
        /// from closing here.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //There are several ways to close an application.
            //We are trying to find the click of the X in the upper right hand corner
            //We will only allow the closing of this app if it is minized.
            if (this.WindowState != FormWindowState.Minimized)
            {
                //we don't close the app...
                e.Cancel = true;
                //minimize the app and then display a message to the user so
                //they understand they didn't close the app they just sent it to the tray.
                this.WindowState = FormWindowState.Minimized;
                //Show the message.
                notifyIcon1.ShowBalloonTip(3000, "IndusG Solutions",
                    "You have not closed this appliation." +
                    (Char)(13) + "It has be moved to the tray." +
                    (Char)(13) + "Right click the Icon to exit.",
                    ToolTipIcon.Info);
            }
        }

        /// <summary>
        /// This is the context menu for the notify icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //If we don't minimize the window first we won't be able to close the window.
        }

        /// <summary>
        /// This is the form menu to close the app.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //If we don't minimize the window first we won't be able to close the window.
        }

        /// <summary>
        /// This is the notify Icon double click event when you click the tray icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }

            // Activate the form.
            this.Activate();
            this.Focus();
        }

    }
}
