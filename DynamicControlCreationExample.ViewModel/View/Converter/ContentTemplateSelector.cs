using System;
using System.Windows;
using System.Windows.Controls;
using DynamicControlCreationExample.WPFMVVM.ViewModel;

namespace DynamicControlCreationExample.WPFMVVM.View.Converter
{
    internal class ContentTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = (container as FrameworkElement)!;
            TabViewModel tab = (item as TabViewModel)!;
            return tab.Index switch
                {
                    0 when element.FindResource("ShowAllTabs") is DataTemplate dt => dt,
                    1 when element.FindResource("ShowOnlyId") is DataTemplate dt2 => dt2,
                    2 when element.FindResource("ShowOnlyName") is DataTemplate dt3 => dt3,
                    _ when element.FindResource("FallbackTemplate") is DataTemplate fallbackDataTemplate => fallbackDataTemplate,
                    _ => throw new ArgumentOutOfRangeException()
                };

            
        }
    }
}
