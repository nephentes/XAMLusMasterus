using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XAMLusMasterus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void loadXaml_Clicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "XAML files (*.xaml)|*.xaml|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            if (openFileDialog.ShowDialog() == true)
            {
                var xamlFileContent = XamlReader.Load(File.OpenRead(openFileDialog.FileName));
                var loadedResourceDictionary = xamlFileContent as ResourceDictionary;
                if (loadedResourceDictionary != null)
                {
                    resourceDictEditor.Init(loadedResourceDictionary);
                    txtFileName.Text = openFileDialog.FileName;
                }
                else
                {
                    await this.ShowMessageAsync("Problem", "This editor supports ResourceDictionary files only.");
                }

            }

        }
    }
}
