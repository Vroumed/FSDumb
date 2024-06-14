﻿using System;
using System.Collections;

namespace Vroumed.FSDumb.Extensions
{
    public static class Extensions
    {
        public static bool Any(this ArrayList list)
        {
            return list.Count > 0;
        }

        public static bool Any(this ArrayList list, Func<object, bool> predicate)
        {
            return list.ToArray().Any(predicate);
        }

        public static bool Any<T>(this T[] list, Func<T, bool> predicate)
        {
            if (list == null)
            {
                return false;
            }
            foreach (var item in list)
            {
                if (predicate(item))
                {
                    return true;
                }
            }

            return false;
        }
    }
}