using System;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace DustInTheWind.Lisimba.Forms
{
    static class ControlExtensions
    {
        public static void Bind<TControl, TData, TProp1, TProp2>(this TControl control, Expression<Func<TControl, TProp1>> property, TData dataSource, Expression<Func<TData, TProp2>> dataSourceProperty, bool formattingEnabled)
            where TControl : Control
        {
            if (control == null) throw new ArgumentNullException("control");

            string controlPropertyName = GetControlPropertyName(property);
            string dataSourcePropertyName = GetControlPropertyName(dataSourceProperty);

            control.DataBindings.Add(controlPropertyName, dataSource, dataSourcePropertyName, formattingEnabled);
        }

        public static void Bind<TControl, TData, TProp1, TProp2>(this TControl control, Expression<Func<TControl, TProp1>> property, TData dataSource, Expression<Func<TData, TProp2>> dataSourceProperty, bool formattingEnabled, DataSourceUpdateMode updateMode)
            where TControl : Control
        {
            if (control == null) throw new ArgumentNullException("control");

            string controlPropertyName = GetControlPropertyName(property);
            string dataSourcePropertyName = GetControlPropertyName(dataSourceProperty);

            control.DataBindings.Add(controlPropertyName, dataSource, dataSourcePropertyName, formattingEnabled, updateMode);
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