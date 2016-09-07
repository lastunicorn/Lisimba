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
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Wpf.Sections.AddressBookSection.Views
{
    /// <summary>
    /// Interaction logic for BirthdayViewer2.xaml
    /// </summary>
    public partial class BirthdayViewer2 : UserControl
    {
        public Date Date
        {
            get { return (Date)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public static readonly DependencyProperty DateProperty = DependencyProperty.Register("BirthdayViewer2.Date", typeof(Date), typeof(BirthdayViewer2), new PropertyMetadata(new Date(0, 0, 0)));

        public BirthdayViewer2()
        {
            InitializeComponent();
        }
    }

    internal class BirthDayViewModel : ViewModelBase
    {
        private int days;
        private int month;
        private int year;

        public int Days
        {
            get { return days; }
            set
            {
                days = value;
                OnPropertyChanged();
            }
        }

        public int Month
        {
            get { return month; }
            set
            {
                month = value;
                OnPropertyChanged();
            }
        }

        public int Year
        {
            get { return year; }
            set
            {
                year = value;
                OnPropertyChanged();
            }
        }
    }
}
