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
using System.Linq.Expressions;
using System.Windows.Forms;

namespace DustInTheWind.Lisimba.Forms
{
    internal static class BindingExtensions
    {
        public static Binding Bind<TControl, TData, TProp1, TProp2>(this TControl control, Expression<Func<TControl, TProp1>> property, TData dataSource, Expression<Func<TData, TProp2>> dataSourceProperty, bool formattingEnabled)
            where TControl : IBindableComponent
        {
            if (control == null) throw new ArgumentNullException("control");

            string controlPropertyName = GetControlPropertyName(property);
            string dataSourcePropertyName = GetControlPropertyName(dataSourceProperty);

            Binding binding = new Binding(controlPropertyName, dataSource, dataSourcePropertyName, formattingEnabled);
            control.DataBindings.Add(binding);

            return binding;
        }

        public static Binding Bind<TControl, TData, TProp1, TProp2>(this TControl control, Expression<Func<TControl, TProp1>> property, TData dataSource, Expression<Func<TData, TProp2>> dataSourceProperty, bool formattingEnabled, DataSourceUpdateMode updateMode)
            where TControl : IBindableComponent
        {
            if (control == null) throw new ArgumentNullException("control");

            string controlPropertyName = GetControlPropertyName(property);
            string dataSourcePropertyName = GetControlPropertyName(dataSourceProperty);

            Binding binding = new Binding(controlPropertyName, dataSource, dataSourcePropertyName, formattingEnabled, updateMode);
            control.DataBindings.Add(binding);

            return binding;
        }

        private static string GetControlPropertyName<TObject, TProperty>(Expression<Func<TObject, TProperty>> property)
        {
            MemberExpression me = property.Body as MemberExpression;

            if (me == null)
                throw new ArgumentException("Invalid expression. You must pass a lambda of the form: 'x => x.Property'.");

            return me.Member.Name;
        }
    }
}