using BlockChain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockChain
{
    public partial class Form1 : Form
    {
        private Chain _chain = new Chain();

        public Form1()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            _chain.Add(textBox1.Text, "Admin");
            textBox1.Clear();
            _chain.Blocks.ForEach(x => listBox1.Items.Add(x));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _chain.Blocks.ForEach(x => listBox1.Items.Add(x));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            AddButton.Enabled = !string.IsNullOrWhiteSpace(textBox1.Text);
        }
    }
}
