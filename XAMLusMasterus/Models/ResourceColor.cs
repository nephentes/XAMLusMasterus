using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace XAMLusMasterus.Models
{

    public class ResourceColor : INotifyPropertyChanged
    {

        public object ResourceKey { get; set; }

        public Color OriginalColor { get; set; }

        public Color CurrentColor { get; set; }

        public string CurrentColorText
        {
            get
            {
                return CurrentColor.ToString();
            }
            set
            {
                var color = ColorConverter.ConvertFromString(value) as Color?;
                if (color.HasValue)
                {
                    CurrentColor = color.Value;
                    Locked = true;
                    OnPropertyChanged("Locked");
                }
                OnPropertyChanged("CurrentColorText");
                OnPropertyChanged("CurrentColor");
            }
        }

        public bool Locked { get; set; }

        public ResourceColor()
        {

        }

        public ResourceColor(object key, Color color) : this()
        {
            ResourceKey = key;
            OriginalColor = color;
            CurrentColor = color;
            Locked = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
