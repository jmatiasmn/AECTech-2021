using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Windows;
using System.Reflection;
using Masterclass.Revit.Utilities;
using Masterclass.Revit.SecondButton;
using System.Windows.Interop;

namespace Masterclass.Revit.SecondButton
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class SecondButtonCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIApplication uiApp = commandData.Application;
                SecondButtonModel m = new SecondButtonModel(uiApp);
                SecondButtonViewModel vm = new SecondButtonViewModel(m);
                SecondButtonView v = new SecondButtonView()
                {
                    DataContext = vm,

                };

               // WindowInteropHelper modal = new WindowInteropHelper(v)
               //{
               //     Owner = uiApp.MainWindowHandle
               // };
                

                v.ShowDialog();

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
                "Second \nButton",
                assembly.Location,
                MethodBase.GetCurrentMethod().DeclaringType?.FullName
                )
            {
                ToolTip= "SecondButton button command",
                LargeImage = ImageUtils.LoadImage(assembly
                , "_32x32.secondButton.png")
            });
        }
    }
}
