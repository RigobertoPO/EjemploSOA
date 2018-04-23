using ClienteWPF.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClienteWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObtenerEmpleados();
        }
        private void EliminarBoton_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://201.134.173.232/pruebaapi/");
            client.BaseAddress = new Uri("http://localhost:14082/");
            var id = IdTextBox.Text.Trim();
            var url = "api/Empleado/DeleteEmpleado?Id=" + id;
            HttpResponseMessage response = client.DeleteAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Empleado eliminado");
                ObtenerEmpleados();
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://201.134.173.232/pruebaapi/");
            client.BaseAddress = new Uri("http://localhost:14082/");
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            var nuevoEmpleado = new Empleado();
            nuevoEmpleado.Id = Guid.NewGuid();
            nuevoEmpleado.Nombre = NombreTextBox.Text;
            nuevoEmpleado.Apellidos = ApellidosTextBox.Text;
            nuevoEmpleado.Edad = int.Parse(EdadTextBox.Text);
            nuevoEmpleado.Login = LoginTextBox.Text;
            nuevoEmpleado.Password = ContraseñaPasswordBox.Password;

            var response = client.PostAsJsonAsync("api/Empleado/AddEmpleado", nuevoEmpleado).Result;

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Empleado agregado");
                IdTextBox.Text = "";
                NombreTextBox.Text = "";
                ApellidosTextBox.Text = "";
                EdadTextBox.Text = "";
                ObtenerEmpleados();
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
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
                EmpleadoDataGrid.ItemsSource = listaEmpleados;
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }

        }

        #endregion

    
    }
}
