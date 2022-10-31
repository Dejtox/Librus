using Blazored.SessionStorage;
using System.Text;
using System.Text.Json;

namespace GradeSystem.v1.Client.Extencion
{
    public static class SessionStorageServiceExtentions
    {
        public static async Task SaveItemEncryptedAsync<T>(this ISessionStorageService sessionStorageService,string key, T item)
        {
            var itemJson = JsonSerializer.Serialize(item);
            var itemJasonBytes = Encoding.UTF8.GetBytes(itemJson);
            var base64Json = Convert.ToBase64String(itemJasonBytes);
            await sessionStorageService.SetItemAsync(key, base64Json);
        }

        public static async Task<T> ReadEncryptedAsync<T>(this ISessionStorageService sessionStorageService, string key)
        {
            var base64Json = await sessionStorageService.GetItemAsync<string>(key);
            var itemJasonBytes = Convert.FromBase64String(base64Json);
            var itemJason = Encoding.UTF8.GetString(itemJasonBytes);
            var item = JsonSerializer.Deserialize<T>(itemJason);
            return item;

        }
    }
}
