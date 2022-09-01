using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VerRequisiciones
{
    public partial class Form1 : Form
    {
        private String versiontext = "0.0.2";
        private String version = "8deabc7e901af6f38f0e51f187dace5779298897";
        public static String conexionsqllast = "server=148.223.153.37,5314; database=InfEq;User ID=eordazs;Password=Corpame*2013; integrated security = false ; MultipleActiveResultSets=True";


        public static String servivor = "148.223.153.43\\MSSQLSERVER1";
        public static String basededatos = "bd_SiTAF";
        public static String usuariobd = "sa";
        public static String passbd = "At3n4";
        public static string nsql = "server=" + servivor + "; database=" + basededatos + " ;User ID=" + usuariobd + ";Password=" + passbd + "; integrated security = false ; MultipleActiveResultSets=True";

        public static List<String[]> archivos = new List<String[]>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion2 = new SqlConnection(conexionsqllast))
                {
                    conexion2.Open();
                    String sql2 = "SELECT (SELECT valor FROM Configuracion WHERE nombre='VerRequisiciones_Version') as version,valor FROM Configuracion WHERE nombre='VerRequisiciones_hash'";
                    SqlCommand comm2 = new SqlCommand(sql2, conexion2);
                    SqlDataReader nwReader2 = comm2.ExecuteReader();
                    if (nwReader2.Read())
                    {
                        if (nwReader2["valor"].ToString() != version)
                        {
                            MessageBox.Show("No se puede inciar sesion porque hay una nueva version.\n\nNueva Version: " + nwReader2["version"].ToString() + "\nVersion actual: " + versiontext + "\n\nEl programa se cerrara.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            System.Windows.Forms.Application.Exit();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se puedo verificar la version del programa.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        System.Windows.Forms.Application.Exit();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en validar la version del programa\n\nMensaje: " + ex.Message, "Ver Requisiciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Forms.Application.Exit();
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            archivos.Clear();
            try
            {
                using (SqlConnection conexion2 = new SqlConnection(nsql))
                {
                    conexion2.Open();
                    String sql2 = $"SELECT TOP(1000) a.* FROM [bd_SiTAF].[dbo].[ms_ticket] m INNER JOIN [bd_SiTAF].[dbo].[ms_archivo] a ON a.id_ms_ticket=m.id_ms_ticket where m.descripcion LIKE '%{textBox1.Text}%' order by m.id_ms_ticket DESC";
                    SqlCommand comm2 = new SqlCommand(sql2, conexion2);
                    SqlDataReader nwReader2 = comm2.ExecuteReader();

                    while(nwReader2.Read())
                    {
                        String[] n = new String[2];
                        n[0] = nwReader2["id_archivo"].ToString();
                        n[1] = nwReader2["nombre"].ToString();
                        archivos.Add(n);  
                    }
                    Adjuntos adjunto = new Adjuntos();
                    adjunto.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en validar la version del programa\n\nMensaje: " + ex.Message, "Información del Equipo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

    }
}
