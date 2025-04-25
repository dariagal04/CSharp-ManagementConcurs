using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using App.domain;

namespace App.repository
{
    public class CategorieDBRepo : ICategorieVarstaRepo
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(CategorieDBRepo));
        private readonly IDictionary<string, string> _props;

        public CategorieDBRepo(IDictionary<string, string> props)
        {
            Log.Info("Creating ParticipantRepository ");
            _props = props;
        }
       

        
        public List<CategorieVarsta> GetAll()
        {
            var categorii = new List<CategorieVarsta>();
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM \"categorii\"";

                    var resultSet = command.ExecuteReader();
                    while (resultSet.Read())
                    {
                        var id = resultSet.GetInt32(0);
                        var VarstaMin = resultSet.GetInt32(1);
                        var VarstaMax = resultSet.GetInt32(2);
                       

                        categorii.Add(new CategorieVarsta(id,VarstaMin,VarstaMax));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error retrieving all Participanti", ex);
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }
            

            return categorii;
            
        
        }

        public CategorieVarsta GetOne(int id)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = $"SELECT * FROM \"categorii\" WHERE id = {id}";
                    var resultSet = command.ExecuteReader();
                    
                    if (resultSet.Read())
                    {
                        var varstaMin = resultSet.GetInt32(1);
                        var varstaMax = resultSet.GetInt32(2);
                        var category = new CategorieVarsta(id, varstaMin, varstaMax);
                        return category;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while retrieving category from database.", ex);
                return null;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }

            return null;
        }

        public bool SaveEntity(CategorieVarsta entity)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO \"categorii\" (\"varsta_min\", \"varsta_max\") VALUES (@min, @max)";
                    var paramMin = command.CreateParameter();
                    var paramMax = command.CreateParameter();
                    paramMin.ParameterName = "@min";
                    paramMax.ParameterName = "@max";
                    paramMin.Value = entity.VarstaMin;
                    paramMax.Value = entity.VarstaMax;
                    command.Parameters.Add(paramMin);
                    command.Parameters.Add(paramMax);
                    command.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while saving category to database.", ex);
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
                    command.CommandText = "DELETE FROM \"categorii\" WHERE id = @id";
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
                Log.Error("Error while deleting category from database.", ex);
                return false;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }
        }

        public bool UpdateEntity(CategorieVarsta entity)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "UPDATE \"categorii\" SET \"varsta_min\" = @min, \"varsta_max\" = @max WHERE id = @id";
                    var paramMin = command.CreateParameter();
                    var paramMax = command.CreateParameter();
                    var paramId = command.CreateParameter();
                    paramMin.ParameterName = "@min";
                    paramMax.ParameterName = "@max";
                    paramId.ParameterName = "@id";
                    paramMin.Value = entity.VarstaMin;
                    paramMax.Value = entity.VarstaMax;
                    paramId.Value = entity.Id;
                    command.Parameters.Add(paramMin);
                    command.Parameters.Add(paramMax);
                    command.Parameters.Add(paramId);
                    command.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while updating category in database.", ex);
                return false;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }
        }

        public CategorieVarsta GetCategorieVarstaByAgeGroup(int varstaMin, int varstaMax)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM \"categorii\" WHERE \"varsta_min\" = @varstaMin AND \"varsta_max\" = @varstaMax";
                    var paramMin = command.CreateParameter();
                    var paramMax = command.CreateParameter();
                    paramMin.ParameterName = "@varstaMin";
                    paramMax.ParameterName = "@varstaMax";
                    paramMin.Value = varstaMin;
                    paramMax.Value = varstaMax;
                    command.Parameters.Add(paramMin);
                    command.Parameters.Add(paramMax);
                    var resultSet = command.ExecuteReader();

                    if (resultSet.Read())
                    {
                        var id = resultSet.GetInt32(0);
                        return new CategorieVarsta(id, varstaMin, varstaMax);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while retrieving category by age group from database.", ex);
                return null;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }

            return null;
        }

        public CategorieVarsta GetCategorieVarstaByAge(int varsta)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM \"categorii\" WHERE \"varsta_min\" <= @varsta AND \"varsta_max\" >= @varsta";
                    var paramVarsta = command.CreateParameter();
                    paramVarsta.ParameterName = "@varsta";
                    paramVarsta.Value = varsta;
                    command.Parameters.Add(paramVarsta);
                    var resultSet = command.ExecuteReader();

                    if (resultSet.Read())
                    {
                        var id = resultSet.GetInt32(0);
                        var varstaMin = resultSet.GetInt32(1);
                        var varstaMax = resultSet.GetInt32(2);
                        return new CategorieVarsta(id, varstaMin, varstaMax);
                    }
                    else
                    {
                        Log.WarnFormat("No category found for age: {0}", varsta);
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while retrieving category by age from database.", ex);
                return null;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }
        }
    }
}
