using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Masterclass.Revit.Utilities;

namespace Masterclass.Revit.FirstButton
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class FirstButtonCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                MessageBox.Show("Hello world!","AEC Tech", MessageBoxButton.OK);
                return Result.Succeeded;
            }
            catch (Exception)
            {

                return Result.Failed;
            }
            
        }

        public static void CreateButton (RibbonPanel panel)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            panel.AddItem(new PushButtonData(

                MethodBase.GetCurrentMethod().DeclaringType?.Name,
                "First \nButton",
                assembly.Location,
                MethodBase.GetCurrentMethod().DeclaringType?.FullName
                )
            {
                ToolTip="FirstButton button command",
                LargeImage = ImageUtils.LoadImage(assembly
                , "_32x32.firstButton.png")
            }).Enabled = false;
        }
    }
}
