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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Egg.GateModel;

namespace DustInTheWind.Lisimba.WinForms.Forms
{
    public partial class GateSelector : UserControl
    {
        private ObservableCollection<IGate> gates;

        public ObservableCollection<IGate> Gates
        {
            get { return gates; }
            set
            {
                if (gates != null)
                    gates.CollectionChanged -= HandleGatesCollectionChanged;

                gates = value;

                if (gates != null)
                    gates.CollectionChanged += HandleGatesCollectionChanged;

                UpdateGates();
            }
        }

        public AvailableGates AvailableGates { get; set; }

        public event EventHandler GateSelected;

        public GateSelector()
        {
            InitializeComponent();
        }

        private void HandleGatesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateGates();
        }

        private void UpdateGates()
        {
            ClearItems();

            if (gates == null)
                return;

            AddItems();
        }

        private void ClearItems()
        {
            Controls.Clear();
        }

        private void AddItems()
        {
            IEnumerable<Control> controls = gates
                .Select(x =>
                {
                    Button button = new Button
                    {
                        Dock = DockStyle.Top,
                        Text = x.Name,
                        Tag = x.Id,
                        AutoEllipsis = true
                    };

                    button.Click += HandleButtonClick;

                    return button;
                })
                .Reverse();

            Controls.AddRange(controls.ToArray());
        }

        private void HandleButtonClick(object sender, EventArgs e)
        {
            Control control = sender as Control;

            if (control != null && AvailableGates != null)
                AvailableGates.SetDefaultGate(control.Tag as string);

            OnGateSelected();
        }

        protected virtual void OnGateSelected()
        {
            EventHandler handler = GateSelected;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}
