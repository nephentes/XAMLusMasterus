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
using System.Xml;

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
                    resourceDictEditor.Init(loadedResourceDictionary, File.ReadAllText(openFileDialog.FileName));
                    txtFileName.Text = openFileDialog.FileName;
                }
                else
                {
                    await this.ShowMessageAsync("Problem", "This editor supports ResourceDictionary files only.");
                }

            }

        }

        private void saveXaml_Clicked(object sender, RoutedEventArgs e)
        {
            resourceDictEditor.viewModel.UpdateResourceDictionary();

            /* This bad boy serilized too much :(
            using (var fileWriter = File.OpenWrite(txtFileName.Text))
            {
              
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                XmlWriter writer = XmlWriter.Create(fileWriter, settings);
                XamlWriter.Save(resourceDictEditor.viewModel.EditedObject, writer);
            }
            */

            // for now ugly solution
            var fileContent = resourceDictEditor.viewModel.FileContent;
            var replacements = resourceDictEditor.viewModel.GetReplaceList();

            foreach (var stage1 in replacements)
                fileContent = fileContent.Replace(stage1.Item1, stage1.Item2);

            foreach (var stage2 in replacements)
                fileContent = fileContent.Replace(stage2.Item2, stage2.Item3);

            File.WriteAllText(txtFileName.Text, fileContent);

        }
    }
}
