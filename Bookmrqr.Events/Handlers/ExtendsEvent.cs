using Newtonsoft.Json;
using Quintor.CQRS.Events;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bookmrqr.Events.Handlers
{
    public static class ExtendsEvent
    {
        private static readonly JsonSerializerSettings SerializerSettings =
            new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None };

        public static string ToJson(this IEvent @event)
        {
            return JsonConvert.SerializeObject(@event, SerializerSettings);
        }

        public static IEvent ToEvent(this string json, string eventType)
        {
            Type type = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(IEvent).IsAssignableFrom(t))
                .Where(t => t.Name.Equals(eventType, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            return (IEvent)JsonConvert.DeserializeObject(json, type);
        }

        public static byte[] ToJsonBytes<TEvent>(this TEvent @event) where TEvent : IEvent
        {
            string jsonString = JsonConvert.SerializeObject(@event, SerializerSettings);
            return Encoding.ASCII.GetBytes(jsonString);
        }

        public static TEvent ToEvent<TEvent>(this byte[] jsonBytes) where TEvent : IEvent
        {
            string jsonString = Encoding.ASCII.GetString(jsonBytes);
            return JsonConvert.DeserializeObject<TEvent>(jsonString);
        }

        public static IEvent ToEvent(this byte[] jsonBytes, string eventType)
        {
            string jsonString = Encoding.ASCII.GetString(jsonBytes);
            return jsonString.ToEvent(eventType);
        }

    }
}
