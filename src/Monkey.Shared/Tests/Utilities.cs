using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

using A = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Monkey.Tests.Utilities
{
    public static class Assert
    {
        public static void AreDeeplyEqual(object a, object b)
        {
            A.AreEqual(JsonConvert.SerializeObject(a), JsonConvert.SerializeObject(b));
        }
    }
}
