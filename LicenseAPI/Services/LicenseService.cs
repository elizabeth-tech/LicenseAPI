using System;
using System.Collections.Generic;

namespace LicenseAPI.Services
{
    // Класс, создающий лицензии ПО и проверяющий их актуальность
    public class LicenseService
    {
        private readonly Dictionary<string, DateTime> store;
        private readonly TimeSpan keyLifetime = TimeSpan.FromDays(30);

        public LicenseService() =>
            // Вместо БД берем обычный словарь
            store = new Dictionary<string, DateTime>();

        public void RegisterKey(string key)
        {
            var time = DateTime.Now.Add(keyLifetime);
            store[key] = time;
        }

        public bool IsActive(string key)
        {
            if (!store.TryGetValue(key, out var time)) return false;

            return DateTime.Now < time; // Если время жизни еще не истекло, то ключ валидный
        }
    }
}
