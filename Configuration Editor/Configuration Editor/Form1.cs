/*
* FILE			: Form1.cs
* PROJECT		: PROG3070 - Milestone-2
* PROGRAMMER	: Enes Demirsoz, Jessica Sim, Hoda Akrami
* FIRST VERSION : 2022-11-26
* DESCRIPTION	: This is the configuration editor of the eKanban system
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Configuration_Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.configurationTableTableAdapter.Fill(this.eKanbanDataSet.ConfigurationTable);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Update the database with the user's changes.
            this.configurationTableTableAdapter.Update(this.eKanbanDataSet.ConfigurationTable);
            MessageBox.Show("Changes has been saved!!");
        }
    }
}
