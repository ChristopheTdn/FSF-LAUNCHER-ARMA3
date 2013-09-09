using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FSFLauncherA3
{
    public partial class Dial_LanceSynchroOfficielle : Form
    {
        public Dial_LanceSynchroOfficielle()
        {
            InitializeComponent();
            FSFLauncherCore.dialogueReponse = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FSFLauncherCore.dialogueReponse = true;
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Dial_LanceSynchroOfficielle_Load(object sender, EventArgs e)
        {

        }
    }
}
