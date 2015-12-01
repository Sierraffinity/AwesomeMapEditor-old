using System;
using System.Collections.Generic;

using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FETextEditor
{
    public partial class IntegerRequester : Form
    {
        public int Value
        {
            get;
            private set;
        }

        public IntegerRequester() : this(0)
        {
        }

        public IntegerRequester(int defaultValue)
        {
            InitializeComponent();
            numericUpDown1.Value = defaultValue;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Value = (int)numericUpDown1.Value;
        }
    }
}
