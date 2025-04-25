using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using App.domain;

namespace App.repository
{
    public class PersoanaOficiuDBRepo : IPersoanaOficiuRepo
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PersoanaOficiuDBRepo));
        private readonly IDictionary<string, string> _props;

        public PersoanaOficiuDBRepo(IDictionary<string, string> props)
        {
            Log.Info("Creating PersoanaOficiuDBRepo");
            _props = props;
        }

        public bool SaveEntity(PersoanaOficiu entity)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO \"persoana_oficiu\" (\"id\", \"username\", \"password\", \"locatie_oficiu\") VALUES (@id, @username, @password, @locatieOficiu)";
                    
                    var paramId = command.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = entity.Id;

                    var paramUsername = command.CreateParameter();
                    paramUsername.ParameterName = "@username";
                    paramUsername.Value = entity.username;

                    var paramPassword = command.CreateParameter();
                    paramPassword.ParameterName = "@password";
                    paramPassword.Value = entity.password;

                    var paramLocatie = command.CreateParameter();
                    paramLocatie.ParameterName = "@locatieOficiu";
                    paramLocatie.Value = entity.locatie_oficiu;
                    
                    command.Parameters.Add(paramId);
                    command.Parameters.Add(paramUsername);
                    command.Parameters.Add(paramPassword);
                    command.Parameters.Add(paramLocatie);
                    
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error saving PersoanaOficiu", ex);
                return false;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }
        }

        public bool DeleteEntity(int id)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "DELETE FROM \"persoana_oficiu\" WHERE \"id\" = @id";

                    var paramId = command.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = id;
                    command.Parameters.Add(paramId);
                    
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error deleting PersoanaOficiu", ex);
                return false;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }
        }

        public bool UpdateEntity(PersoanaOficiu entity)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "UPDATE \"persoana_oficiu\" SET \"username\" = @username, \"password\" = @password, \"locatie_oficiu\" = @locatieOficiu WHERE \"id\" = @id";

                    var paramId = command.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = entity.Id;

                    var paramUsername = command.CreateParameter();
                    paramUsername.ParameterName = "@username";
                    paramUsername.Value = entity.username;

                    var paramPassword = command.CreateParameter();
                    paramPassword.ParameterName = "@password";
                    paramPassword.Value = entity.password;

                    var paramLocatie = command.CreateParameter();
                    paramLocatie.ParameterName = "@locatieOficiu";
                    paramLocatie.Value = entity.locatie_oficiu;

                    command.Parameters.Add(paramId);
                    command.Parameters.Add(paramUsername);
                    command.Parameters.Add(paramPassword);
                    command.Parameters.Add(paramLocatie);
                    
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error updating PersoanaOficiu", ex);
                return false;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }
        }

        public PersoanaOficiu GetOne(int id)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM \"persoana_oficiu\" WHERE \"id\" = @id";

                    var paramId = command.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = id;
                    command.Parameters.Add(paramId);

                    var resultSet = command.ExecuteReader();
                    if (resultSet.Read())
                    {
                        var username = resultSet.GetString(1);
                        var password = resultSet.GetString(2);
                        var locatieOficiu = resultSet.GetString(3);
                        return new PersoanaOficiu(id, username, password, locatieOficiu);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error retrieving PersoanaOficiu", ex);
            }

            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }
            return null;
        }

        public PersoanaOficiu GetOneByUsername(string username)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM \"persoana_oficiu\" WHERE \"username\" = @username";

                    var paramUsername = command.CreateParameter();
                    paramUsername.ParameterName = "@username";
                    paramUsername.Value = username;
                    command.Parameters.Add(paramUsername);

                    var resultSet = command.ExecuteReader();
                    if (resultSet.Read())
                    {
                        var id = resultSet.GetInt32(0);
                        var password = resultSet.GetString(2);
                        var locatieOficiu = resultSet.GetString(3);
                        return new PersoanaOficiu(id, username, password, locatieOficiu);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error retrieving PersoanaOficiu by username", ex);
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }

            return null;
        }

        public List<PersoanaOficiu> GetAll()
        {
            var persoaneOficiu = new List<PersoanaOficiu>();
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM \"persoana_oficiu\"";

                    var resultSet = command.ExecuteReader();
                    while (resultSet.Read())
                    {
                        var id = resultSet.GetInt32(0);
                        var username = resultSet.GetString(1);
                        var password = resultSet.GetString(2);
                        var locatieOficiu = resultSet.GetString(3);

                        persoaneOficiu.Add(new PersoanaOficiu(id, username, password, locatieOficiu));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error retrieving all PersoanaOficiu", ex);
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }

            return persoaneOficiu;
        }
    }
}
