using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Masterclass.Revit.SecondButton
{
    public class SpatialObjectWrapper : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public double Area { get; set; }
        public ElementId Id { get; set; }

        private bool _Isselected;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsSelected
        {
            get { 
                return _Isselected; 
            }
            set { 
                _Isselected = value; 
                RaisePropertyChanged(nameof(IsSelected)); 
            }
        }

        public SpatialObjectWrapper(SpatialElement spatialElement)
        {
            Name = spatialElement.Name;
            Area = spatialElement.Area;
            Id = spatialElement.Id;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
