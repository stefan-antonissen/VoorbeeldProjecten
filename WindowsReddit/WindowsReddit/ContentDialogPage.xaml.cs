using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsReddit
{
    public sealed partial class ContentDialogPage : ContentDialog
    {
        private string page = "hot";

        public ContentDialogPage(string page)
        {
            this.page = page;
            this.InitializeComponent();
            switch (page)
            {
                case "hot":
                    HotCheckBox.IsChecked = true;
                    break;
                case "new":
                    NewCheckBox.IsChecked = true;
                    break;
                case "rising":
                    RisingCheckBox.IsChecked = true;
                    break;
                case "controversial":
                    ControversialCheckBox.IsChecked = true;
                    break;
                case "top":
                    TopCheckBox.IsChecked = true;
                    break;
                case "gilded":
                    GildedCheckBox.IsChecked = true;
                    break;
                default:
                    HotCheckBox.IsChecked = true;
                    break;
            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        public string getPage()
        {
            return page;
        }

        private void HotCheckBox_Click(object sender, RoutedEventArgs e)
        {
            page = "hot";
            NewCheckBox.IsChecked = false;
            RisingCheckBox.IsChecked = false;
            ControversialCheckBox.IsChecked = false;
            TopCheckBox.IsChecked = false;
            GildedCheckBox.IsChecked = false;
        }

        private void NewCheckBox_Click(object sender, RoutedEventArgs e)
        {
            page = "new";
            HotCheckBox.IsChecked = false;
            RisingCheckBox.IsChecked = false;
            ControversialCheckBox.IsChecked = false;
            TopCheckBox.IsChecked = false;
            GildedCheckBox.IsChecked = false;
        }

        private void RisingCheckBox_Click(object sender, RoutedEventArgs e)
        {
            page = "rising";
            HotCheckBox.IsChecked = false;
            NewCheckBox.IsChecked = false;
            ControversialCheckBox.IsChecked = false;
            TopCheckBox.IsChecked = false;
            GildedCheckBox.IsChecked = false;
        }

        private void ControversialCheckBox_Click(object sender, RoutedEventArgs e)
        {
            page = "controversial";
            HotCheckBox.IsChecked = false;
            NewCheckBox.IsChecked = false;
            RisingCheckBox.IsChecked = false;
            TopCheckBox.IsChecked = false;
            GildedCheckBox.IsChecked = false;
        }

        private void TopCheckBox_Click(object sender, RoutedEventArgs e)
        {
            page = "top";
            HotCheckBox.IsChecked = false;
            NewCheckBox.IsChecked = false;
            RisingCheckBox.IsChecked = false;
            ControversialCheckBox.IsChecked = false;
            GildedCheckBox.IsChecked = false;
        }

        private void GildedCheckBox_Click(object sender, RoutedEventArgs e)
        {
            page = "gilded";
            HotCheckBox.IsChecked = false;
            NewCheckBox.IsChecked = false;
            RisingCheckBox.IsChecked = false;
            ControversialCheckBox.IsChecked = false;
            TopCheckBox.IsChecked = false;
        }
    }
}
