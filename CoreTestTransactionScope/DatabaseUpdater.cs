using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace CoreTestTransactionScope
{
    public class DatabaseUpdater
    {
        public async Task<Person> CreatePersonAsync(Person person)
        {
            // string connectionString = Configuration["ConnectionStrings:DefaultConnection"];

            var connectionString = @"Server=.;Database=CoreTestTransactionScope.db;Trusted_Connection=True;ConnectRetryCount=0;";
            var personId = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlParameter fullNameParam = new SqlParameter();
                fullNameParam.ParameterName = "@FullName";
                fullNameParam.Value = person.FullName;

                SqlParameter dateUpdateParam = new SqlParameter();
                dateUpdateParam.ParameterName = "@DateUpdated";
                dateUpdateParam.Value = person.DateUpdated;

                string sql = @"
                        INSERT INTO dbo.Person (FullName, DateUpdated)
                        VALUES (@FullName, @DateUpdated);

                        SELECT CAST(SCOPE_IDENTITY() AS INT);";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(fullNameParam);
                    command.Parameters.Add(dateUpdateParam);

                    command.CommandType = CommandType.Text;
                    connection.Open();
                    personId = (int)await command.ExecuteScalarAsync();
                    connection.Close();
                }

                person.Id = personId;
            }

            return person;
        }

    }
}
