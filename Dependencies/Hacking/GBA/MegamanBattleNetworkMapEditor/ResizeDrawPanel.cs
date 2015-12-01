using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Nintenlord.MegamanBattleNetworkMapEditor
{
    public partial class ResizeDrawPanel : Panel
    {
        public ResizeDrawPanel()
        {
            InitializeComponent();
            this.ResizeRedraw = true;
        }
    }
}
