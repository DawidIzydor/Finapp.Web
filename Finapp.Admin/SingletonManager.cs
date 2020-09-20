using System;
using System.Collections.Generic;
using System.Windows;

namespace Finapp.Admin
{
    public static class SingletonManager
    {
        private static readonly Dictionary<string, object> Cache = new Dictionary<string, object>();

        public static T Get<T>()
        {
            var fullName = typeof(T).FullName;
            if (fullName == null)
            {
                return default;
            }

            lock (Cache)
            {
                if (Cache.ContainsKey(fullName))
                {
                    if ((T) Cache[fullName] == null)
                    {
                        Cache.Remove(fullName);
                    }
                    else
                    {
                        return (T) Cache[fullName];
                    }
                }

                var instance = Activator.CreateInstance(typeof(T));
                Cache.Add(fullName, instance);
                if (instance is Window window)
                {
                    window.Closed += (sender, args) => Cache[fullName] = null;
                }

                return (T) Cache[fullName];
            }
        }
    }
}