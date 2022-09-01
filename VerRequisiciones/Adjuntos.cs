using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VerRequisiciones
{
    public partial class Adjuntos : Form
    {
        public Adjuntos()
        {
            InitializeComponent();
        }

        private void Adjuntos_Load(object sender, EventArgs e)
        {
            if(Form1.archivos.Any())
            {
                foreach(String[] archivo in Form1.archivos)
                {
                    dataGridView1.Rows.Add("http://148.223.153.43/SiTAFA/" + archivo[0] + "-" + archivo[1], archivo[1]);
                }
            }
        }

        public static String navegador_link;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            String link = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            navegador_link = link;
            Navegador nav = new Navegador();
            nav.ShowDialog();
            //Process.Start("iexplore.exe", link);
        }
    }
}
