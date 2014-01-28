using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using libWyvernzora.Utilities;
using libWyvernzora.Core;

namespace Animat.CacheViewer
{
    public partial class MainForm : Form
    {
        private ObservableStudioCacheManager cacheManager;

        public MainForm()
        {
            InitializeComponent();

            AttachEventHandlers();
        }

        private void AttachEventHandlers()
        {
            tsmLoad.Click += (@s, e) =>
            {
                var dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    cacheManager = new ObservableStudioCacheManager(dialog.SelectedPath);
                    UpdateList();
                }
            };

            tsmAddEntry.Click += (@s, e) =>
            {
                var dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.png; *.jpg; *.bmp)|*.png;*.jpg;*.bmp";
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    

                }
            };
        }

        private void UpdateList()
        {
            using (new ActionLock(treeView1.BeginUpdate, treeView1.EndUpdate))
            {
                treeView1.Nodes.Clear();

                foreach (var id in cacheManager.GetEntryIDs())
                {
                    var node = new TreeNode("0x" + DirectIntConv.ToHexString(id, 8));

                    treeView1.Nodes.Add(node);
                }
            }
        }
    }
}
