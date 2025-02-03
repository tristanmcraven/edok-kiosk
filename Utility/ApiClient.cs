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

            public static async Task<List<Order>> GetOrders(uint id)
            {
                return await SendRequest<List<Order>>($"restaurant/{id}/orders", HttpMethod.Get);
            }
        }

        public static class _Order
        {
            public static async Task<List<CartItem>> GetCartItems(Order order)
            {
                return await SendRequest<List<CartItem>>($"cart/{order.CartId}/items", HttpMethod.Get);
            }

            public static async Task<Order> ApplyNextStatus(uint id)
            {
                return await SendRequest<Order>($"order/{id}/next_status", HttpMethod.Put);
            }

            public static async Task<Order> GetById(uint id)
            {
                return await SendRequest<Order>($"order/{id}", HttpMethod.Get);
            }

            public static async Task<Order> CancelOrder(uint id)
            {
                return await SendRequest<Order>($"order/{id}/cancel", HttpMethod.Put);
            }
        }

        public static class _Cart
        {
            public static async Task<List<CartItem>> GetCartItems(uint id)
            {
                return await SendRequest<List<CartItem>>($"cart/{id}/items", HttpMethod.Get);
            }
        }

        public static class _User
        {
            public static async Task<User> GetUserById(uint id)
            {
                return await SendRequest<User>($"user/{id}", HttpMethod.Get);
            }

            public static async Task<List<Order>> GetOrders(uint id)
            {
                return await SendRequest<List<Order>>($"user/{id}/orders", HttpMethod.Get);
            }
        }

        public static class _Food
        {
            public static async Task<Food> GetById(uint id)
            {
                return await SendRequest<Food>($"food/{id}", HttpMethod.Get);
            }
        }

        public static class _KitchenStatus
        {
            public static async Task<List<KitchenStatus>> Get()
            {
                return await SendRequest<List<KitchenStatus>>("kitchenstatus", HttpMethod.Get);
            }
        }

        public static class _FoodCategory
        {
            public static async Task<FoodCategory> GetById(uint id)
            {
                return await SendRequest<FoodCategory>($"foodcategory/{id}", HttpMethod.Get);
            }
        }
    }
}
