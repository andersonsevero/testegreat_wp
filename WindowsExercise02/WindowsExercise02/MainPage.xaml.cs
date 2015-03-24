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

        private const string TXT_CREATE_NEW = "Create New";
        private const string TXT_CANCEL = "Cancel";
        private bool waitingNewNumber = false;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ButtonCreateCancel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            BlockControls();
            CreateNewMagicNumber();
        }

        private async Task CreateNewMagicNumber()
        {

            waitingNewNumber = true;
            MagicNumberCreator mnc = new MagicNumberCreator();
            int newMagicNumber = mnc.CreateMagicNumber();
            MagicNumberBox.Text = "" + newMagicNumber;
            ReleaseControls();

        }

        private void BlockControls()
        {
            ProgressBar.Visibility = Visibility.Visible;
            ButtonCreateCancel.IsHitTestVisible = false;
            ButtonCreateCancel.Content = TXT_CANCEL;
        }

        private void ReleaseControls()
        {
            ProgressBar.Visibility = Visibility.Collapsed;
            ButtonCreateCancel.IsHitTestVisible = true;
            ButtonCreateCancel.Content = TXT_CREATE_NEW;
        }

    }
}
