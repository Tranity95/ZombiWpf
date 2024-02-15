using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using ZombiWpf.DTO;

namespace ZombiWpf.API
{
    public class Client
    {
        HttpClient httpClient = new HttpClient();
        public Client()
        {
            httpClient.BaseAddress = new Uri(@"https://localhost:7224/");
        }

        public async Task<List<ZombieDTO>> GetZombies()
        {
            try
            {
                var response = await httpClient.GetAsync("Zombie/GetZondri");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ZombieDTO>>(content);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                // Обработка исключений
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<ZombieTypeDTO>> GetZTypes()
        {
            try
            {
                var response = await httpClient.GetAsync("ZType/GetZTypes");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ZombieTypeDTO>>(content);
                }
                else
                {
                    throw new Exception($"Error: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                // Обработка исключений
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<ZombieDTO> EditZombie(ZombieDTO zombie, int id)
        {
            using StringContent jsonContent = new(
                   System.Text.Json.JsonSerializer.Serialize(zombie),
                   Encoding.UTF8,
                   "application/json");
            using HttpResponseMessage response = await httpClient.PutAsync("Zombie/" + zombie.Id, jsonContent);
            response.EnsureSuccessStatusCode();
           // MessageBox.Show(response.StatusCode.ToString());
            return zombie;
        }

        public async Task DeleteZombie(int id)
        {
            using HttpResponseMessage response = await httpClient.DeleteAsync("Zombie/" + id);
            response.EnsureSuccessStatusCode();
            
        }

        public async Task AddZombieAsync(ZombieDTO zombie)
        {
            using (var client = new HttpClient())
            {
                var jsonContent = JsonConvert.SerializeObject(zombie);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("Zombie/AddZondri", httpContent);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Не удалось добавить зомби.");
                }
            }
        }
        /*
        public async void DeleteZombie(ZombieDTO zombie)
        {
            using StringContent jsonContent = new(
                   JsonSerializer.Serialize(zombie),
                   Encoding.UTF8,
                   "application/json");
            using HttpResponseMessage response = await httpClient.PutAsync("Zombie/" + zombie.Id, jsonContent);
            response.EnsureSuccessStatusCode();
            MessageBox.Show(response.StatusCode.ToString());
        }
        */

        static Client instance = new ();
        public static Client Instance 
        {
            get 
            {
                if(instance == null)
                    instance = new Client();
                return instance;
            } 
        }
    }
}
