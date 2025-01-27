using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace edok_kiosk.Utility
{
    public static class ApiClient
    {
        public static readonly string _apiPath = "http://localhost:5224/api/";
        public static HttpClient httpClient = new HttpClient();

        
    }
}
