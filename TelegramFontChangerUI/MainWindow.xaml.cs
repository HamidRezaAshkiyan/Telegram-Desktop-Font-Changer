using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static System.Windows.Media.Fonts;
using MessageBox = System.Windows.MessageBox;

namespace TelegramFontChangerUI
{
    public partial class MainWindow
    {
        private ObservableCollection<string> Fonts { get; } = new ObservableCollection<string>(SystemFontFamilies.Select(x => x.Source).ToList());

        public MainWindow()
        {
            InitializeComponent();

            DataContext = Fonts;
        }


        private void SystemFontCombobox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var myKey = Registry.LocalMachine.OpenSubKey(
                    "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\FontSubstitutes", true);
                var fontName = SystemFontCombobox.SelectedValue.ToString();

                if ( myKey == null ) return;
                myKey.SetValue("MS Shell Dlg 2", fontName, RegistryValueKind.String);
                //MessageBox.Show(myKey.GetValue("MS Shell Dlg 2").ToString());
                myKey.Close();

                //Dialog.Show("Font Has Changed successfully");
                Result.Text = "Font Has Changed successfully";
                Result.Visibility = Visibility.Visible;
            }
            catch ( Exception exception )
            {
                //Dialog.Show("You are not in Administrator mode. Run app as administrator.");
                Result.Text = "You are not in Administrator mode. Run app as administrator.";
                Result.Visibility = Visibility.Visible;
            }
        }
    }
}
