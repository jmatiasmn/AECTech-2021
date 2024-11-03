using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Masterclass.Revit.SecondButton
{
    public class SecondButtonModel
    {
        public UIApplication uiApp { get;}
        public Document doc { get; }
        public SecondButtonModel(UIApplication uiapp)
        {
            uiApp = uiapp;
            doc = uiApp.ActiveUIDocument.Document;
        }

        public ObservableCollection<SpatialObjectWrapper> CollectSpatialObjects()
        {
            var spatialObjects = new FilteredElementCollector(doc)
                .OfClass(typeof(SpatialElement))
                .WhereElementIsNotElementType()
                .Cast<SpatialElement>()
                .Where(x => x is Room)
                .Select( r => new SpatialObjectWrapper(r));

            return new ObservableCollection<SpatialObjectWrapper>(spatialObjects);
        }

        public void Delete(List<SpatialObjectWrapper> selected)
        {
            List<ElementId> ids = selected.Select(x => x.Id).ToList();

            using (Transaction t = new Transaction(doc,"Delete Rooms"))
            {
                t.Start();
                doc.Delete(ids);
                t.Commit();
            }
            
        }
    }
}
