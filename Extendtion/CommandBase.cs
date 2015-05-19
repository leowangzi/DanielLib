using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;

namespace Command
{
    /// <summary>
    /// internal void RegisterCommand()
    /// {
    ///      CommandBindings.Add(new CommandBinding(CommandBase.SampleCommand, 
    ///      new ExecutedRoutedEventHandler(CommandBase.SampleCommand_Executed), 
    ///      new CanExecuteRoutedEventHandler(CommandBase.SamleCommand_CanExecute)));
    /// }
    /// CommandBase.SampleCommand.Execute("Message", this);
    /// </summary>
    public static class CommandBase
    {
        static CommandBase()
        {
            SampleCommand = new RoutedCommand();
        }

        public static RoutedCommand SampleCommand { get; private set; }

        public static void SampleCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var val = e.Parameter as String;
            MessageBox.Show(val.ToString());
        }

        public static void SamleCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }
    }
}
