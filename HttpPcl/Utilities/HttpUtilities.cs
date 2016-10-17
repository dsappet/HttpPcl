using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HttpPcl.Utilities
{
    public class HttpUtilities
    {
        public static Dictionary<string, string> BuildHeader(string token, bool isMetric = false)
        {
            var headers = new Dictionary<string, string>()
            {
                {"Token", token},
                {"IsMetric", isMetric.ToString() },
            };
            return headers;
        }
        public static Dictionary<string, string> BuildHeader(string token, int priceRegion, bool isMetric = false)
        {
            var headers = new Dictionary<string, string>()
            {
                {"Token", token},
                {"PriceRegionID", priceRegion.ToString() },
                {"IsMetric", isMetric.ToString() }
            };
            return headers;
        }

        public static Dictionary<string, string> BuildHeader(string token, string culture, int priceRegion =0, bool isMetric = false )
        {
            var headers = new Dictionary<string,string>()
            {
                {"IsMetric", isMetric.ToString() }
            };
            if(token !=null) headers.Add("Token",token);
            if(culture != null) headers.Add("Culture",culture);
            if(priceRegion !=0) headers.Add("PriceRegionID",priceRegion.ToString());
            return headers;
        }

        public static string BuildArguments<T>(T inputClass)
        {
            if (inputClass == null) return "";
            var builder = new StringBuilder();
            
            builder = builder.AppendFormat("?");
            var type = inputClass.GetType();
            foreach (var runtimeProperty in type.GetRuntimeProperties())
            {
                if (runtimeProperty.GetValue(inputClass, null) == null) continue; // Don't process null properties
                string name = runtimeProperty.Name;
                // Don't really need the null check below because they should be skipped above, but ... just because good code
                string value = runtimeProperty.GetValue(inputClass, null) != null ? runtimeProperty.GetValue(inputClass, null).ToString(): "null";
                builder = builder.AppendFormat("{0}={1}&", Uri.EscapeDataString(name), Uri.EscapeDataString(value));
            }
            if (builder.ToString().EndsWith("&")) builder = builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

    }
}
