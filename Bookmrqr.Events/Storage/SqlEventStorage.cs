using Quintor.CQRS.Events;
using Quintor.CQRS.Events.Storage;
using Quintor.CQRS.Utilities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Bookmrqr.Events.Storage
{
    public class SqlEventStorage : IEventStorage
    {
        private readonly ISettings _settings;

        public SqlEventStorage(ISettings settings)
        {
            _settings = settings;
        }

        public void Save(IEvent @event)
        {
            const string sql =
                 @"INSERT INTO Events(Id, Created, AggregateId, Version, Event, EventType)  
                   VALUES(@Id, @Created, @AggregateId, @Version, @Event, @EventType)";

            using (SqlConnection conn = new SqlConnection(_settings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();

                cmd.Parameters.Add(new SqlParameter("@Id", @event.Id));
                cmd.Parameters.Add(new SqlParameter("@Created", @event.TimeStamp));
                cmd.Parameters.Add(new SqlParameter("@AggregateId", @event.AggregateId));
                cmd.Parameters.Add(new SqlParameter("@Version", @event.Version));
                cmd.Parameters.Add(new SqlParameter("@Event", @event.ToJson()));
                cmd.Parameters.Add(new SqlParameter("@EventType", @event.GetType().AssemblyQualifiedName));

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        //Violation of primary key. Handle Exception
                        //TODO throw ConcurrencyException
                    }
                    throw;
                }
            }
        }

        public IEnumerable<IEvent> GetEvents(string aggregateId, int fromVersion)
        {
            const string sql = "SELECT Event, EventType FROM Events WHERE AggregateId=@AggregateId and Version>@Version";
            using (var conn = new SqlConnection(_settings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();

                cmd.Parameters.Add(new SqlParameter("@AggregateId", aggregateId));
                cmd.Parameters.Add(new SqlParameter("@Version", fromVersion));

                List<IEvent> events = new List<IEvent>();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    events.Add(reader.GetString(0).ToEvent(reader.GetString(1)));
                }

                return events;
            }
        }
    }
}
