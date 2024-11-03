using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Masterclass.Revit.FirstButton;
using Masterclass.Revit.SecondButton;
using Masterclass.Revit.ThirdButton;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;

namespace Masterclass.Revit
{
    public class AppCommand : IExternalApplication
    {

        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                application.CreateRibbonTab("Masterclass");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            var ribbonPanel = application.GetRibbonPanels("Masterclass")
                .FirstOrDefault(x => x.Name == "AEC Tech")
                ?? application.CreateRibbonPanel("Masterclass", "AEC Tech");

            FirstButtonCommand.CreateButton(ribbonPanel);
            ribbonPanel.AddSeparator();
            SecondButtonCommand.CreateButton(ribbonPanel);
            ribbonPanel.AddSeparator();
            ThirdButtonCommand.CreateButton(ribbonPanel);

            // Adicionar a ComboBox ao Ribbon Panel
            ComboBoxData comboBoxData = new ComboBoxData("MyComboBox");
            ComboBox comboBox = ribbonPanel.AddItem(comboBoxData) as ComboBox;

            // Adicionar itens à ComboBox
            ComboBoxMemberData item1 = new ComboBoxMemberData("Item1", "Item 1");
            ComboBoxMemberData item2 = new ComboBoxMemberData("Item2", "Item 2");
            ComboBoxMemberData item3 = new ComboBoxMemberData("Item3", "Item 3");

            comboBox.AddItem(item1);
            comboBox.AddItem(item2);
            comboBox.AddItem(item3);

            // Evento quando a seleção da ComboBox muda
            comboBox.CurrentChanged += OnComboBoxSelectionChanged;

            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        private void OnComboBoxSelectionChanged(object sender, ComboBoxCurrentChangedEventArgs e)
        {
            TaskDialog.Show("Selection Changed", $"Selected item: {e.NewValue}");
        }
    }
}
