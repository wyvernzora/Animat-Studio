using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Animat.UI
{
    public partial class ErrorWindow : Form
    {
        public ErrorWindow(Exception x)
        {
            InitializeComponent();

            // Construct Exception Info
            var xInfo = new StringBuilder();
            xInfo.AppendFormat("Exception: {0};", x.GetType().FullName);
            xInfo.AppendLine();
            xInfo.AppendFormat("Message: {0};", x.Message);
            xInfo.AppendLine();
            xInfo.AppendLine("===============STACK TRACE===============");
            xInfo.AppendLine(x.StackTrace);

            txtExceptionInfo.Text = xInfo.ToString();

            // Attach Event Handlers
            btnCopy.Click += (@s, e) => Clipboard.SetText(xInfo.ToString());
            btnQuit.Click += (@s, e) => Application.ExitThread();
        }

        private void ErrorWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
