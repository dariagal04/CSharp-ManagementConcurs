using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using App.domain;

namespace App.repository
{
    public class NumeProbaDBRepo : INumeProbaRepo
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(NumeProbaDBRepo));
        private readonly IDictionary<string, string> _props;

        public NumeProbaDBRepo(IDictionary<string, string> props)
        {
            Log.Info("Initializing NumeProbaDBRepo with properties.");
            _props = props;
        }

        public List<NumeProba> GetAll()
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM \"nume_probe\"";
                    var resultSet = command.ExecuteReader();
                    var numeProbe = new List<NumeProba>();

                    while (resultSet.Read())
                    {
                        var id = resultSet.GetInt32(0);
                        var nume = resultSet.GetString(1);
                        var numeProba = new NumeProba(id, nume);
                        numeProbe.Add(numeProba);
                    }

                    return numeProbe;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error retrieving nume_probe from database.", ex);
                return null;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }
        }

        public NumeProba GetOne(int id)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM \"nume_probe\" WHERE id = @id";
                    var paramId = command.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = id;
                    command.Parameters.Add(paramId);
                    var resultSet = command.ExecuteReader();

                    if (resultSet.Read())
                    {
                        var nume = resultSet.GetString(1);
                        return new NumeProba(id, nume);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error retrieving nume_proba from database.", ex);
                return null;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }
            return null;
        }

        public bool SaveEntity(NumeProba entity)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO \"nume_probe\" (nume) VALUES (@nume)";
                    var paramNume = command.CreateParameter();
                    paramNume.ParameterName = "@nume";
                    paramNume.Value = entity.numeProba;
                    command.Parameters.Add(paramNume);
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error inserting nume_proba into database.", ex);
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
                    command.CommandText = "DELETE FROM \"nume_probe\" WHERE id = @id";
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
                Log.Error("Error deleting nume_proba from database.", ex);
                return false;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }
        }

        public bool UpdateEntity(NumeProba entity)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "UPDATE \"nume_probe\" SET nume = @nume WHERE id = @id";
                    var paramNume = command.CreateParameter();
                    var paramId = command.CreateParameter();
                    paramNume.ParameterName = "@nume";
                    paramId.ParameterName = "@id";
                    paramNume.Value = entity.numeProba;
                    paramId.Value = entity.Id;
                    command.Parameters.Add(paramNume);
                    command.Parameters.Add(paramId);
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error updating nume_proba in database.", ex);
                return false;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }
        }

        public NumeProba GetNumeProbaByName(string name)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM \"nume_probe\" WHERE nume = @nume";
                    var paramNume = command.CreateParameter();
                    paramNume.ParameterName = "@nume";
                    paramNume.Value = name;
                    command.Parameters.Add(paramNume);
                    var resultSet = command.ExecuteReader();

                    if (resultSet.Read())
                    {
                        var id = resultSet.GetInt32(0);
                        return new NumeProba(id, name);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error retrieving nume_proba by name from database.", ex);
                return null;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }
            return null;
        }
    }
}
