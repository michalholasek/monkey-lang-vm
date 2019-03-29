using System.Collections.Generic;
using System.Linq;

using A = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Monkey.Tests.Utilities
{
    public static class Assert
    {
        public static void AreDeeplyEqual<T>(List<T> a, List<T> b)
        {
            A.AreEqual(a.Count, b.Count);

            for (var i = 0; i < a.Count; i++)
            {
                var props = a[i].GetType().GetProperties();

                foreach (var prop in props)
                {
                    A.AreEqual(
                        a[i].GetType().GetProperty(prop.Name).GetValue(a[i]),
                        b[i].GetType().GetProperty(prop.Name).GetValue(b[i])
                    );
                }   
            }
        }

        public static void AreDeeplyEqual<T, U>(Dictionary<T, U> a, Dictionary<T, U> b)
        {
            A.AreEqual(a.Count, b.Count);
            a.Keys.ToList().ForEach(key => { A.AreEqual(a[key], b[key]); });
        }
    }
}
