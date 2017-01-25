using System;
using StackExchange.Redis;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to RedisChat 1.0");

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("redis-16112.c11.us-east-1-3.ec2.cloud.redislabs.com:16112");
            ISubscriber sub = redis.GetSubscriber();

            sub.Subscribe("messages", (channel, message) => {
                Console.WriteLine((string)message);
                Console.Write("> ");
            });

            Console.Write("Enter your name : ");
            var name = Console.ReadLine();

            sub.Publish("messages", "["+ DateTime.Now +"] " + name + " entered the channel");
            
            while(true) {
                Console.Write("> ");
                var input = Console.ReadLine();
                sub.Publish("messages", "["+ DateTime.Now +"] " + name + " : " + input);
            }
        }
    }
}
