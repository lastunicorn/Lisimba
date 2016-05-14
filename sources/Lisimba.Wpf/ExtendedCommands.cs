// Lisimba
// Copyright (C) 2007-2016 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Windows;
using System.Windows.Input;

namespace DustInTheWind.Lisimba.Wpf
{
    public static class ExtendedCommands
    {
        public static readonly DependencyProperty DoubleClickCommandProperty;
        public static readonly DependencyProperty DoubleClickCommandParameterProperty;

        static ExtendedCommands()
        {
            DoubleClickCommandProperty = DependencyProperty.RegisterAttached("DoubleClickCommand", typeof(ICommand), typeof(ExtendedCommands), new UIPropertyMetadata(null, OnDoubleClickCommandPropertyChanged));
            DoubleClickCommandParameterProperty = DependencyProperty.RegisterAttached("DoubleClickCommandParameter", typeof(object), typeof(ExtendedCommands), new UIPropertyMetadata(null));
        }

        public static ICommand GetDoubleClickCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(DoubleClickCommandProperty);
        }

        public static void SetDoubleClickCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DoubleClickCommandProperty, value);
        }

        public static object GetDoubleClickCommandParameter(DependencyObject obj)
        {
            return obj.GetValue(DoubleClickCommandParameterProperty);
        }

        public static void SetDoubleClickCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(DoubleClickCommandParameterProperty, value);
        }

        private static void OnDoubleClickCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as UIElement;

            if (element != null)
            {
                if (e.OldValue == null && e.NewValue != null)
                    element.MouseDown += Control_MouseDown;
                else if (e.OldValue != null && e.NewValue == null)
                    element.MouseDown -= Control_MouseDown;
            }
        }

        private static void Control_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                var element = sender as UIElement;

                if (element != null)
                {
                    var command = GetDoubleClickCommand(element);
                    var parameter = GetDoubleClickCommandParameter(element);

                    if (command != null && command.CanExecute(parameter))
                    {
                        e.Handled = true;
                        command.Execute(parameter);
                    }
                }
            }
        }
    }
}
