using JailersApp.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyPrisoners.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllPage : ContentPage
    {
        string Uri = "http://jailing.azurewebsites.net/api/PrisonersApi";
        public AllPage()
        {
            InitializeComponent();

            DownloadPrisonersAsync();
        }

        private async Task DownloadPrisonersAsync()
        {
            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(Uri);

            var prisoners = JsonConvert.DeserializeObject<List<Prisoner>>(json);

            EmployeesListView.ItemsSource = prisoners;
        }
    }
}
