﻿using System;
using System.Collections.Generic;
using System.Windows;

namespace DynamicControlCreationExample.WPFMVVM;

public static class ExtensionMethods
{
    public static void AddOnUI<T>(this ICollection<T> collection, T item)
    {
        Action<T> addMethod = collection.Add;
        Application.Current.Dispatcher.BeginInvoke(addMethod, item);
    }
}