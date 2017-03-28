using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Turta;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace IoTDevice
{
    public sealed partial class MainPage : Page
    {
        // Röle denetleyicisini oluştur.
        static RelayController4Ch relayController = new RelayController4Ch();
        DispatcherTimer tmr = new DispatcherTimer();
        
        public MainPage()
        {
            this.InitializeComponent();

            tmr.Interval = TimeSpan.FromMilliseconds(2000);
            tmr.Tick += Tmr_Tick;
            tmr.Start();

            // Uygulama kapanırken tetiklenecek event handler'ı oluştur.
            Unloaded += MainPage_Unloaded;
        }

        private async void Tmr_Tick(object sender, object e)
        {
            bool state = await Control();

            relayController.
                SetRelay(RelayController4Ch.RelayCh.Ch1, 
                    state ? RelayController4Ch.RelayState.On : 
                        RelayController4Ch.RelayState.Off);

            text.Text = state.ToString();
        }

        public async Task<bool> Control()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("{Web_Api_Url}");
                var model = JsonConvert.DeserializeObject<Switch>(response.Content.ReadAsStringAsync().Result);
                return model.IsOpen;
            }
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            // Uygulama kapanırken röleleri pasifleştir ve pinleri serbest bırak.
            relayController.Dispose();
        }
    }
}
