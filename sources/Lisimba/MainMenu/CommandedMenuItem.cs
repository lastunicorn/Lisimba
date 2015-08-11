// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
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
using System.ComponentModel;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Operations;
using DustInTheWind.Lisimba.Utils;

namespace DustInTheWind.Lisimba.MainMenu
{
    internal class CommandedMenuItem : ToolStripMenuItem, IBindableComponent
    {
        private BindingContext bindingContext;
        private ControlBindingsCollection dataBindings;

        [Browsable(false)]
        public BindingContext BindingContext
        {
            get
            {
                if (bindingContext == null)
                    bindingContext = new BindingContext();

                return bindingContext;
            }
            set { bindingContext = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ControlBindingsCollection DataBindings
        {
            get
            {
                if (dataBindings == null)
                    dataBindings = new ControlBindingsCollection(this);

                return dataBindings;
            }
        }

        //private IOpertion opertion;

        //[Browsable(false)]
        //public ApplicationStatus ApplicationStatus { get; set; }

        public string ShortDescription { get; set; }

        //[Browsable(false)]
        //public IOpertion Opertion
        //{
        //    get { return opertion; }
        //    set
        //    {
        //        if (opertion != null)
        //            opertion.IsEnabledChanged -= HandleOpertionEnabledChanged;

        //        opertion = value;

        //        if (opertion != null)
        //            opertion.IsEnabledChanged += HandleOpertionEnabledChanged;

        //        Enabled = opertion == null || opertion.IsEnabled;
        //    }
        //}

        private IExecutableViewModel viewModel;

        public IExecutableViewModel ViewModel
        {
            get { return viewModel; }
            set
            {
                DataBindings.Clear();

                viewModel = value;

                if (viewModel != null)
                    this.Bind(x => x.Enabled, viewModel, x => x.IsEnabled, false, DataSourceUpdateMode.Never);
            }
        }

        [Browsable(false)]
        public object CommandParameter { get; set; }

        [Browsable(false)]
        public Func<object> CommandParameterProvider { get; set; }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (ViewModel != null)
                ViewModel.MouseEnter();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (ViewModel != null)
                ViewModel.MouseLeave();

            base.OnMouseLeave(e);
        }

        protected override void OnClick(EventArgs e)
        {
            if (ViewModel != null)
            {
                object commandParameter = CalculateParameterToUseWithCommand();

                if (commandParameter == null)
                    ViewModel.Execute();
                else
                    ViewModel.Execute(commandParameter);
            }

            base.OnClick(e);
        }

        //private void HandleOpertionEnabledChanged(object sender, EventArgs eventArgs)
        //{
        //    Enabled = ViewModel.IsEnabled;
        //}

        //private string CalculateTextToDisplayAsStatus()
        //{
        //    if (ShortDescription != null)
        //        return ShortDescription;

        //    if (Opertion != null && Opertion.ShortDescription != null)
        //        return Opertion.ShortDescription;

        //    return null;
        //}

        private object CalculateParameterToUseWithCommand()
        {
            return CommandParameterProvider != null
                ? CommandParameterProvider()
                : CommandParameter;
        }
    }
}