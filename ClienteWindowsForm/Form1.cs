using ClienteWindowsForm.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteWindowsForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ObtenerEmpleados();
        }

    

        private void Agregarbutton_Click(object sender, EventArgs e)
        {

        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {

        }

        #region METODOS
        private void ObtenerEmpleados()
        {
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://201.134.173.232/pruebaapi/");
            client.BaseAddress = new Uri("http://localhost:14082/");
            //client.DefaultRequestHeaders.Add("appkey", "myapp_key");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/Empleado/GetEmpleado").Result;
            if (response.IsSuccessStatusCode)
            {
                var listaEmpleados = response.Content.ReadAsAsync<IEnumerable<Empleado>>().Result;
                EmpleadosdataGridView.DataSource = listaEmpleados;
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }

        }

        #endregion
    }
}
