using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DXWindowsApplication2
{
    public partial class splashForm : Form
    {
        private static Thread _splashThread;
        private static splashForm _splashForm;
        //
        public splashForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Show the Splash Screen (Loading...)
        /// </summary>
        public static void ShowSplash()
        {
            if (_splashThread == null)
            {
                // show the form in a new thread
                _splashThread = new Thread(new ThreadStart(DoShowSplash));
                _splashThread.IsBackground = true;
                _splashThread.Start();
            }
        }

        // called by the thread
        private static void DoShowSplash()
        {
            if (_splashForm == null)
                _splashForm = new splashForm();

            // create a new message pump on this thread (started from ShowSplash)
            Application.Run(_splashForm);
        }

        /// <summary>
        /// Close the splash (Loading...) screen
        /// </summary>
        public static void CloseSplash()
        {
            // need to call on the thread that launched this splash
            if (_splashForm.InvokeRequired)
                _splashForm.Invoke(new MethodInvoker(CloseSplash));

            else
                Application.ExitThread();
        }
    }
}
