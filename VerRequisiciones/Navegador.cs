using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VerRequisiciones
{
    public partial class Navegador : Form
    {
        public Navegador()
        {
            InitializeComponent();
        }

        private void Navegador_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate(Adjuntos.navegador_link);
        }
    }
}
