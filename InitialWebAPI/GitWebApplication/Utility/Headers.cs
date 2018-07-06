using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Utility
{
    public static class Headers
    {
        public static string GetHeaderKeyValue(HttpHeaders headers, string key)
        {
            IEnumerable<string> keyList;
            if (headers.TryGetValues(key, out keyList))
            {
                return keyList.FirstOrDefault();
            }

            return string.Empty;
        }
    }
}
