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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DustInTheWind.Lisimba.Wpf.MainWindows
{
    // todo: Very ugly code. Is there a better approach?

    /// <summary>
    /// Interaction logic for ContactEditor_ItemsSection.xaml
    /// </summary>
    public partial class ContactEditor_ItemsSection : UserControl
    {
        public ContactEditor_ItemsSection()
        {
            InitializeComponent();
        }

        private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
        {
            ListBox listBox = sender as ListBox;

            if (listBox == null)
                return;

            listBox.SelectedItem = null;
        }

        private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Handled)
                return;

            e.Handled = true;

            MouseWheelEventArgs eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
            {
                RoutedEvent = MouseWheelEvent,
                Source = sender
            };

            UIElement parent = ((Control)sender).Parent as UIElement;

            if (parent != null)
                parent.RaiseEvent(eventArg);
        }

        private void UIElement_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Handled)
                return;

            if (e.Key == Key.Up)
            {
                ListBox listBox = sender as ListBox;

                if (listBox != null && listBox.SelectedIndex == 0)
                {
                    DockPanel currentItem = FindAncestor<DockPanel>(listBox);

                    ItemsControl parent = FindAncestor<ItemsControl>(currentItem);

                    if (parent != null)
                    {
                        int currentIndex = GetIndexOfChild(parent, currentItem);

                        if (currentIndex > 0)
                        {
                            DockPanel previousItem = GetChildAt(parent, currentIndex - 1) as DockPanel;

                            if (previousItem != null)
                            {
                                ListBox previousListBox = VisualTreeHelper.GetChild(previousItem, 1) as ListBox;

                                if (previousListBox != null)
                                {
                                    previousListBox.Focus();
                                    previousListBox.SelectedIndex = previousListBox.Items.Count - 1;

                                    UIElement item = previousListBox.ItemContainerGenerator.ContainerFromItem(previousListBox.SelectedItem) as UIElement;

                                    if (item != null)
                                    {
                                        item.Focus();
                                        e.Handled = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (e.Key == Key.Down)
            {
                ListBox listBox = sender as ListBox;

                if (listBox != null && listBox.SelectedIndex == listBox.Items.Count - 1)
                {
                    DockPanel currentItem = FindAncestor<DockPanel>(listBox);

                    ItemsControl parent = FindAncestor<ItemsControl>(currentItem);

                    if (parent != null)
                    {
                        int currentIndex = GetIndexOfChild(parent, currentItem);

                        if (currentIndex < parent.Items.Count - 1)
                        {
                            DockPanel nextItem = GetChildAt(parent, currentIndex + 1) as DockPanel;

                            if (nextItem != null)
                            {
                                ListBox previousListBox = VisualTreeHelper.GetChild(nextItem, 1) as ListBox;

                                if (previousListBox != null)
                                {
                                    previousListBox.Focus();
                                    previousListBox.SelectedIndex = 0;

                                    UIElement item = previousListBox.ItemContainerGenerator.ContainerFromItem(previousListBox.SelectedItem) as UIElement;

                                    if (item != null)
                                    {
                                        item.Focus();
                                        e.Handled = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static DependencyObject GetChildAt(ItemsControl parent, int index)
        {
            object item = parent.Items[index];
            UIElement uiElement = (UIElement)parent.ItemContainerGenerator.ContainerFromItem(item);

            return VisualTreeHelper.GetChild(uiElement, 0);
        }

        private static int GetIndexOfChild(ItemsControl itemsControl, DependencyObject child)
        {
            for (int index = 0; index < itemsControl.Items.Count; index++)
            {
                object item = itemsControl.Items[index];
                UIElement uiElement = (UIElement)itemsControl.ItemContainerGenerator.ContainerFromItem(item);

                if (ReferenceEquals(uiElement, child) || IsChild(child, uiElement))
                    return index;
            }

            return -1;
        }

        private static bool IsChild(DependencyObject child, UIElement parent)
        {
            if (child == null || parent == null)
                return false;

            DependencyObject candidate = VisualTreeHelper.GetParent(child);

            if (candidate == null)
                return false;

            if (ReferenceEquals(candidate, parent))
                return true;

            return IsChild(candidate, parent);
        }

        private static T FindAncestor<T>(DependencyObject element)
            where T : class
        {
            if (element == null)
                return null;

            T candidate = element as T;

            if (candidate != null)
                return candidate;

            return FindAncestor<T>(VisualTreeHelper.GetParent(element));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button == null)
                return;

            button.ContextMenu.IsEnabled = true;
            button.ContextMenu.PlacementTarget = button;
            button.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            button.ContextMenu.IsOpen = true;
        }
    }
}
