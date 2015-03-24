using DojoLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsExercise03
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        /*
         * TODO: Probably it's better moving those for a resource file
         */
        private const string TXT_CREATE_NEW = "Create New";
        private const string TXT_CANCEL = "Cancel";

        /* Running task flag */
        private bool cancelationRequested = false;


        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ButtonCreateCancel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (cancelationRequested)
            {
                RestoreControls();
                cancelationRequested = false;
            }
            else
            {
                EnableCancelation();
                CreateNewMagicNumber();
            }
        }

        private async Task CreateNewMagicNumber()
        {

            cancelationRequested = true;
            MagicNumberCreator mnc = new MagicNumberCreator();
            int newMagicNumber = await RequestMagicNumber();

            /*
             * This section runs only after the MagicNumber creation
             * process is finished. In the meanwhile, the 'cancelationRequested'
             * flag might be changed from the User request (via UI). In this
             * case the process of showing a new Magic Number is dropped.
             */
            if (cancelationRequested)
            {
                cancelationRequested = false;
                MagicNumberBox.Text = newMagicNumber.ToString();
                RestoreControls();
            }
            
        }

        /**
         * This method uses a Task approach to request a new Magic Number
         * asynchronously, by using await keyword. The caller method does
         * so as weel, so it waits for a Task<int> which actually comes out
         * as a plain int.
         * */
        private async Task<int> RequestMagicNumber()
        {
            MagicNumberCreator mnc = new MagicNumberCreator();
            int newMagicNumber = await Task.Run(() => mnc.CreateMagicNumber());
            return newMagicNumber;
        }

        private void EnableCancelation()
        {
            cancelationRequested = true;
            ProgressBar.Visibility = Visibility.Visible;
            ButtonCreateCancel.Content = TXT_CANCEL;
        }

        private void RestoreControls()
        {
            ProgressBar.Visibility = Visibility.Collapsed;
            ButtonCreateCancel.Content = TXT_CREATE_NEW;
        }

    }
}
