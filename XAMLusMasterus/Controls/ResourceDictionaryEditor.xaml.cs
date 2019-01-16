using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XAMLusMasterus.Models;

namespace XAMLusMasterus.Controls
{
    /// <summary>
    /// Interaction logic for ResourceDictionaryEditor.xaml
    /// </summary>
    public partial class ResourceDictionaryEditor : UserControl
    {

        public ResourceDictionary EditedObject { get; set; }

        public ObservableCollection<ResourceColor> ResourceColors { get; set; }

        public ResourceDictionaryEditor()
        {
            InitializeComponent();
        }

        public void Init(ResourceDictionary resourceDictionary)
        {
            ResourceColors = new ObservableCollection<ResourceColor>();
            EditedObject = resourceDictionary;
            foreach (var oneKey in EditedObject.Keys)
            {
                var colorValue = EditedObject[oneKey] as Color?;
                if (colorValue != null && colorValue.HasValue)
                {
                    ResourceColors.Add(new ResourceColor(oneKey, colorValue.Value));
                }
            }
            this.DataContext = ResourceColors;
        }

    }
}
