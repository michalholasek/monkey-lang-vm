using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

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

        public static void AreDeeplyEqual(object a, object b)
        {
            A.AreEqual(JsonConvert.SerializeObject(a), JsonConvert.SerializeObject(b));
        }
    }
}
