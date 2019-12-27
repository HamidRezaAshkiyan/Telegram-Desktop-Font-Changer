using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static System.Windows.Media.Fonts;

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
            var myKey = Registry.LocalMachine.OpenSubKey(
                "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\FontSubstitutes", true);
            var fontName = SystemFontCombobox.Text;

            if ( myKey == null ) return;
            myKey.SetValue("MS Shell Dlg 2", fontName, RegistryValueKind.String);
            MessageBox.Show(myKey.GetValue("MS Shell Dlg 2").ToString());
            myKey.Close();

            
        }
    }
}
