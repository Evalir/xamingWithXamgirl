using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MVMMLogin.Helpers
{
    public static class ExtensionMethods
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            return new ObservableCollection<T>(enumerable);
        }
    }
}
