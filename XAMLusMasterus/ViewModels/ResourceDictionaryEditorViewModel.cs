using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using XAMLusMasterus.Models;

namespace XAMLusMasterus.ViewModels
{
    public class ResourceDictionaryEditorViewModel : INotifyPropertyChanged
    {
        private double _luminosity;
        private double _hue;
        private double _saturation;

        public string FileContent { get; set; }

        public ResourceDictionary EditedObject { get; set; }

        public ObservableCollection<ResourceColor> ResourceColors { get; set; }


        public double Luminosity
        {
            get => _luminosity;
            set
            {
                _luminosity = value;
                OnPropertyChanged("Luminosity");
                ApplyFilters();
            }
        }

        public double Hue
        {
            get => _hue;
            set
            {
                _hue = value;
                OnPropertyChanged("Hue");
                ApplyFilters();
            }
        }

        public double Saturation
        {
            get => _saturation;
            set
            {
                _saturation = value;
                OnPropertyChanged("Saturation");
                ApplyFilters();
            }
        }



        public ResourceDictionaryEditorViewModel()
        {
            ResourceColors = new ObservableCollection<ResourceColor>();
            Luminosity = 0.5;
            Hue = 0.5;
            Saturation = 0.5;
        }

        public void Init(ResourceDictionary resourceDictionary)
        {
            EditedObject = resourceDictionary;
            foreach (var oneKey in EditedObject.Keys)
            {
                var colorValue = EditedObject[oneKey] as Color?;
                if (colorValue != null && colorValue.HasValue)
                {
                    ResourceColors.Add(new ResourceColor(oneKey, colorValue.Value));
                }
            }

        }

        public void UpdateResourceDictionary()
        {
            foreach (var oneKey in EditedObject.Keys)
            {
                var colorValue = EditedObject[oneKey] as Color?;
                if (colorValue != null && colorValue.HasValue)
                {
                    // is there something in editor ?
                    var editorPart = ResourceColors.ToList().FindLast(p => p.ResourceKey == oneKey);
                    if (editorPart!=null)
                    {
                        EditedObject[oneKey] = editorPart.CurrentColor;
                    }
                }
            }
        }

        public List<Tuple<string, string, string>> GetReplaceList()
        {
            List<Tuple<string, string, string>> retVal = new List<Tuple<string, string, string>>();
            int i = 1;
            foreach (var oneColorToReplace in ResourceColors)
            {
                if (oneColorToReplace.CurrentColor.ToString() != oneColorToReplace.OriginalColor.ToString())
                {
                    var replacable = new Tuple<string, string, string>(oneColorToReplace.OriginalColor.ToString(), string.Format("ReplacePlaceholder_{0}_", i), oneColorToReplace.CurrentColor.ToString());
                    retVal.Add(replacable);
                }
                i += 1;
            }
            return retVal;
        }

        public void ApplyFilters()
        {
            foreach (var oneResourceColor in ResourceColors)
            {
                if (!oneResourceColor.Locked)
                {
                    var tmpColor = new HSLColor(oneResourceColor.OriginalColor);
                    tmpColor.Luminosity = tmpColor.Luminosity * Luminosity;
                    tmpColor.Hue = tmpColor.Hue * Hue;
                    tmpColor.Saturation = tmpColor.Saturation * Saturation;
                    oneResourceColor.CurrentColor = tmpColor;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
