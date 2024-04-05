using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

internal class Program
{
    static IFirebaseConfig config = new FirebaseConfig
    {
        AuthSecret = "Key1",
        BasePath = "Key2"
    };

    private static async Task Main(string[] args)
    {
       
        // Создание клиента Firebase
        IFirebaseClient client = new FireSharp.FirebaseClient(config);

        // Данные для записи
        var data = new
        {
            name = "John",
            age = 30
        };

        // Путь к месту, где вы хотите сохранить данные
        string path = "users/user2";

        // Запись данных в базу данных Firebase
        SetResponse response = await client.SetAsync(path, data);

        // Вывод результата операции
        Console.WriteLine("Data saved successfully!");
    

        // Получение данных из узла "users"
        FirebaseResponse response2 = await client.GetAsync(path);

        // Проверка наличия данных
        if (response.Body != "null")
        {
            // Преобразование в объект JObject
            JObject data2 = JObject.Parse(response.Body);

            // Преобразование JObject в IDictionary<string, JToken>
            IDictionary<string, JToken> userData = data2;

            // Вывод данных
            foreach (var user in userData)
            {
                Console.WriteLine($"User ID: {user.Key}");

                // Преобразование значения JToken в Dictionary<string, object>
                Dictionary<string, object> userProperties = user.Value.ToObject<Dictionary<string, object>>();

                foreach (var property in userProperties)
                {
                    Console.WriteLine($"{property.Key}: {property.Value}");
                }
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Узел 'users' не содержит данных.");
        }
        Console.ReadLine();

     
    }
}
