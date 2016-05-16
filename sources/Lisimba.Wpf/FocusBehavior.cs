//// Lisimba
//// Copyright (C) 2007-2016 Dust in the Wind
//// 
//// This program is free software: you can redistribute it and/or modify
//// it under the terms of the GNU General Public License as published by
//// the Free Software Foundation, either version 3 of the License, or
//// (at your option) any later version.
//// 
//// This program is distributed in the hope that it will be useful,
//// but WITHOUT ANY WARRANTY; without even the implied warranty of
//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//// GNU General Public License for more details.
//// 
//// You should have received a copy of the GNU General Public License
//// along with this program.  If not, see <http://www.gnu.org/licenses/>.

//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Input;

//namespace DustInTheWind.Lisimba.Wpf
//{
//    public static class FocusBehavior
//    {
//        public static readonly DependencyProperty FocusFirstProperty = DependencyProperty.RegisterAttached(
//                "FocusFirst",
//                typeof(bool),
//                typeof(FocusBehavior),
//                new PropertyMetadata(false, OnFocusFirstPropertyChanged));

//        public static bool GetFocusFirst(Control control)
//        {
//            return (bool)control.GetValue(FocusFirstProperty);
//        }

//        public static void SetFocusFirst(Control control, bool value)
//        {
//            control.SetValue(FocusFirstProperty, value);
//        }

//        private static void OnFocusFirstPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
//        {
//            Control control = obj as Control;

//            if (control == null)
//                return;

//            if (!(e.NewValue is bool))
//                return;

//            if ((bool)e.NewValue)
//                control.Loaded += (sender, args) => control.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
//        }
//    }
//}
