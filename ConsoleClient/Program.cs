using DeviceId;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var deviceKey = new DeviceIdBuilder()
                .AddProcessorId()
                .AddMotherboardSerialNumber()
                .ToString();

            if(!await CheckLicense(deviceKey))
            {
                Console.WriteLine($"Данный ключ не зарегистрирован: {deviceKey}");
                Console.WriteLine($"Необходимо зарегистрировать ключ");
                return;
            }

            Console.WriteLine($"Лицензия активна: {deviceKey}");
            Console.WriteLine($"Проверка ключа успешна!");
        }

        private static async Task<bool> CheckLicense(string key)
        {
            using var hhtp = new HttpClient();
            var url = $"https://" + $"localhost:44363/api/license/is-active/{key}";
            var json = await hhtp.GetStringAsync(url);
            return JsonConvert.DeserializeObject<bool>(json);
        }
    }
}
