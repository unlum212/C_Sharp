using System;
using System.Collections.Generic;

namespace App
{

    /*
    public enum ProviderDirection
    {
        Kafka,
        Rabbit,
        Redis
    }
    public class Program
    {
        static void Main()
        {
            #region First Case classic switch-case usage

            RoleProvider.Ping(ProviderDirection.Kafka, "random text... bla.. bla...");

            #endregion 
        }
    }
    public static class RoleProvider
    {
        public static void Ping(ProviderDirection target, string message)
        {
            switch (target)
            {
                case ProviderDirection.Kafka:
                    Console.WriteLine("Kafka->{0}", message);
                    break;
                case ProviderDirection.Rabbit:
                    Console.WriteLine("Rabbit MQ->{0}", message);
                    break;
                case ProviderDirection.Redis:
                    Console.WriteLine("Redis->{0}", message);
                    break;
                default:
                    break;
            }
        }
    }
    */

    public enum ProviderDirection
    {
        Kafka,
        Rabbit,
        Redis
    }
    class Program
    {
        static void Main()
        {
            #region Strategy Pattern applied solution
            ProviderContext strategyContext = new ProviderContext();
            string message = "We can say that our code has become more readable thanks to this use of the strategy pattern.";
            strategyContext.Ping(ProviderDirection.Kafka, message);
            
            #endregion
        }
    }
    
    #region Solution with strategy pattern
    interface IProviderStrategy
    {
        void Send(string message);
    }
    class KafkaProvider: IProviderStrategy
    {
        public void Send(string message)
        {
            Console.WriteLine("The message is sent to Kafka. {0}", message);
        }
    }
    class RabbitMqProvider: IProviderStrategy
    {
        public void Send(string message)
        {
            Console.WriteLine("The message is sent to RabbitMQ. {0}", message);
        }
    }
    class RedisProvider: IProviderStrategy
    {
        public void Send(string message)
        {
            Console.WriteLine("The message is sent to Redis. {0}", message);
        }
    }
    class ProviderContext
    {
        private static Dictionary<ProviderDirection, IProviderStrategy> _providers = new Dictionary<ProviderDirection, IProviderStrategy>();

        //// In addition, providers can be imported with a simple injection fiction.
        //public void AddProvider(ProviderDirection direction, IProviderStrategy provider)
        //{
        //    _providers.Add(direction, provider);
        //}
        static ProviderContext()
        {
            _providers.Add(ProviderDirection.Kafka, new KafkaProvider());
            _providers.Add(ProviderDirection.Rabbit, new RabbitMqProvider());
            _providers.Add(ProviderDirection.Redis, new RedisProvider());
        }
        public void Ping(ProviderDirection direction, string message)
        {
            _providers[direction].Send(message);
        }
    }

    #endregion
}