using edok_kiosk.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace edok_kiosk.Utility
{
    public static class ApiClient
    {
        public static readonly string _apiPath = "http://localhost:5224/api/";
        public static HttpClient httpClient = new HttpClient();

        private static async Task<T?> SendRequest<T>(string url, HttpMethod httpMethod, object? body = null)
        {
            using var request = new HttpRequestMessage(httpMethod, _apiPath + url);
            if (body != null)
            {
                var jsonContent = JsonConvert.SerializeObject(body);
                request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            }

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode) return default;

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        private static async Task<bool> SendRequest(string url, HttpMethod httpMethod, object? body = null)
        {
            using var request = new HttpRequestMessage(httpMethod, _apiPath + url);
            if (body != null)
            {
                var jsonContent = JsonConvert.SerializeObject(body);
                request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            }

            var response = await httpClient.SendAsync(request);
            
            return response.IsSuccessStatusCode;
        }

        public static class _Manager
        {
            public static async Task<Manager?> Authenticate(string login, string password)
            {
                var dto = new
                {
                    Login = login,
                    Password = password
                };
                return await SendRequest<Manager?>("manager/login", HttpMethod.Post, dto);
            }

            public static async Task<List<Restaurant>?> GetRestaurantsById(uint id) => await SendRequest<List<Restaurant>>($"manager/{id}/restaurants", HttpMethod.Get);

            public static async Task<Restaurant?> GetLatestRestaurantById(uint id) => (await SendRequest<List<Restaurant>>($"manager/{id}/restaurants", HttpMethod.Get))!.FirstOrDefault();
        }

        public static class _Restaurant
        {
            public static async Task<List<Order>> GetActiveOrders(uint id)
            {
                return await SendRequest<List<Order>>($"restaurant/{id}/orders/active", HttpMethod.Get);
            }
        }

        public static class _Order
        {
            public static async Task<List<CartItem>> GetCartItems(Order order)
            {
                return await SendRequest<List<CartItem>>($"cart/{order.CartId}/items", HttpMethod.Get);
            }
        }

        public static class _Cart
        {
            public static async Task<List<CartItem>> GetCartItems(uint id)
            {
                return await SendRequest<List<CartItem>>($"cart/{id}/items", HttpMethod.Get);
            }
        }

        //public static class _Order
        //{
        //    public static async Task<List<Order>> GetActiveOrders(uint id)
        //    {
        //        return await SendRequest<List<Order>>("restaurant")
        //    }
        //}
    }
}
