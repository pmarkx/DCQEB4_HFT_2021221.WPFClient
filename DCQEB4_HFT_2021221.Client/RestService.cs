﻿using System;
using System.Collections.Generic;
using System.Net.Http;

namespace DCQEB4_HFT_2021221.Client
{
    class RestService
    {
        HttpClient client;

        public RestService(string baseurl)
        {
            Init(baseurl);
        }

        private void Init(string baseurl)
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(baseurl)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue
            ("application/json"));
            try
            {
                client.GetAsync("").GetAwaiter().GetResult();
            }
            catch (HttpRequestException)
            {
                throw new ArgumentException("Endpoint is not available!");
            }

        }

        public IEnumerable<T> Get<T>(string endpoint)
        {
            List<T> items = new();
            HttpResponseMessage response = client.GetAsync(endpoint).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                items = response.Content.ReadAsAsync<List<T>>().GetAwaiter().GetResult();
            }
            return items;
        }

        public T GetSingle<T>(string endpoint)
        {
            T item = default;
            HttpResponseMessage response = client.GetAsync(endpoint).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            }
            return item;
        }

        public T Get<T>(int id, string endpoint)
        {
            T item = default;
            HttpResponseMessage response = client.GetAsync(endpoint + "/" + id.ToString()).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            }
            return item;
        }

        public void Post<T>(T item, string endpoint)
        {
            HttpResponseMessage response =
            client.PostAsJsonAsync(endpoint, item).GetAwaiter().GetResult();

            response.EnsureSuccessStatusCode();
        }

        public void Delete(int id, string endpoint)
        {
            HttpResponseMessage response =
            client.DeleteAsync(endpoint + "/" + id.ToString()).GetAwaiter().GetResult();

            response.EnsureSuccessStatusCode();
        }

        public void Put<T>(T item, string endpoint)
        {
            HttpResponseMessage response =
            client.PutAsJsonAsync(endpoint, item).GetAwaiter().GetResult();

            response.EnsureSuccessStatusCode();
        }

    }
}