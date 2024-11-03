using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Masterclass.Revit.SecondButton
{
    public class SecondButtonViewModel : ViewModelBase
    {
        public SecondButtonModel Model { get; set; }
        public RelayCommand<Window> Close { get; set; }
        public RelayCommand<Window> Delete { get; set; }
        private ObservableCollection<SpatialObjectWrapper> _spatialObjects;
        public ObservableCollection<SpatialObjectWrapper> SpatialObjects
        {

            get
            {
                return _spatialObjects;
            }

            set
            {
                _spatialObjects = value;
                RaisePropertyChanged(() => SpatialObjects);
            }
        }

        public SecondButtonViewModel(SecondButtonModel model)
        {
            Model = model;
            SpatialObjects = Model.CollectSpatialObjects();
            Close = new RelayCommand<Window>(OnClose);
            Delete = new RelayCommand<Window>(OnDelete);

        }

        private void OnDelete(Window win)
        {
            var selected = SpatialObjects.Where(x => x.IsSelected).ToList();
            Model.Delete(selected);
        }

        private void OnClose(Window win)
        {
            win.Close();
        }
    }
}
