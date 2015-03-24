using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AsyncAwaitDojo
{
    public sealed partial class MainPage : Page
    {

        private string requestURL;
        private const double ALPHA_ENABLED = 1, ALPHA_DISABLED = 0.5;

        public MainPage()
        {
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            // Colecting the request form the UI
            requestURL = UrlTextBox.Text;
            // Creating and running the web request 
            SendRequest(requestURL);
            // Locking controls
            BlockControls();
        }

        private void BlockControls()
        {
            ProgressBar.Visibility = Visibility.Visible;
            ContentGrid.Opacity = ALPHA_DISABLED;
            UrlTextBox.IsHitTestVisible = false;
            Confirm.IsHitTestVisible = false;
        }

        private void ReleaseControls()
        {
            ProgressBar.Visibility = Visibility.Collapsed;
            ContentGrid.Opacity = ALPHA_ENABLED;
            UrlTextBox.IsHitTestVisible = true;
            Confirm.IsHitTestVisible = true;
        }

        private async Task SendRequest(string URL)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(URL));

            try
            {
                /* Asynchronous request :: Locks here until response is ready */
                var response = await client.SendAsync(request);

                /* Flush the content to the UI and unlock it */
                BodyTextBlock.Text = response.Content.ReadAsStringAsync().Result;
            }
            catch (HttpRequestException e)
            {
                BodyTextBlock.Text = String.Format("{0} (HResult: {1})", e.Message, e.HResult);
            }
            finally
            {
                ReleaseControls();
            }
        }
        
    }
}