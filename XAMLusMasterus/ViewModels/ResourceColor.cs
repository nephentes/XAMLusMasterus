using ColorPickerWPF;
using GalaSoft.MvvmLight.Command;
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
        private Color _currentColor;

        public object ResourceKey { get; set; }

        public Color OriginalColor { get; set; }

        public Color CurrentColor
        {
            get
            {
                return _currentColor;
            }
            set
            {
                _currentColor = value;
                OnPropertyChanged("CurrentColorText");
                OnPropertyChanged("CurrentColor");
            }
        }

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

        public RelayCommand ChangeColorCommand { get; set; }


        public ResourceColor()
        {
            ChangeColorCommand = new RelayCommand(ExecuteChangeColorCommand);
        }

        public ResourceColor(object key, Color color) : this()
        {
            ResourceKey = key;
            OriginalColor = color;
            CurrentColor = color;
            Locked = false;
        }


        public void ExecuteChangeColorCommand()
        {
            Color changedColor;
            if (ColorPickerWindow.ShowDialog(out changedColor))
            {
                CurrentColor = changedColor;
                Locked = true;
                OnPropertyChanged("Locked");
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