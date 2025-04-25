using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using App.domain;

namespace App.repository
{
    public class ParticipantiDBRepo : IParticipantRepo
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ParticipantiDBRepo));
        private readonly IDictionary<string, string> _props;

        public ParticipantiDBRepo(IDictionary<string, string> props)
        {
            Log.Info("Creating ParticipantiDBRepo");
            _props = props;
        }

        public bool SaveEntity1(Participant entity)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText =
                        "INSERT INTO \"participanti\" (\"nume\", \"varsta\", \"cnp\", \"persoanaOficiu_id\") VALUES (@nume, @varsta, @cnp, @persoanaOficiuId)";

                    var paramNume = command.CreateParameter();
                    paramNume.ParameterName = "@nume";
                    paramNume.Value = entity.Nume;

                    var paramVarsta = command.CreateParameter();
                    paramVarsta.ParameterName = "@varsta";
                    paramVarsta.Value = entity.Varsta;

                    var paramCnp = command.CreateParameter();
                    paramCnp.ParameterName = "@cnp";
                    paramCnp.Value = entity.Cnp;

                    var paramPersoanaOficiu = command.CreateParameter();
                    paramPersoanaOficiu.ParameterName = "@persoanaOficiuId";
                    paramPersoanaOficiu.Value = entity.idPersoanaOficiu;

                    command.Parameters.Add(paramNume);
                    command.Parameters.Add(paramVarsta);
                    command.Parameters.Add(paramCnp);
                    command.Parameters.Add(paramPersoanaOficiu);

                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error saving participant", ex);
                return false;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }
        }
        public bool SaveEntity(Participant entity)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        "INSERT INTO \"participanti\" (\"nume\", \"varsta\", \"cnp\", \"persoanaOficiu_id\") " +
                        "VALUES (@nume, @varsta, @cnp, @persoanaOficiuId)";

                    var paramNume = command.CreateParameter();
                    paramNume.ParameterName = "@nume";
                    paramNume.Value = entity.Nume;

                    var paramVarsta = command.CreateParameter();
                    paramVarsta.ParameterName = "@varsta";
                    paramVarsta.Value = entity.Varsta;

                    var paramCnp = command.CreateParameter();
                    paramCnp.ParameterName = "@cnp";
                    paramCnp.Value = entity.Cnp;

                    var paramPersoanaOficiu = command.CreateParameter();
                    paramPersoanaOficiu.ParameterName = "@persoanaOficiuId";
                    paramPersoanaOficiu.Value = entity.idPersoanaOficiu;

                    command.Parameters.Add(paramNume);
                    command.Parameters.Add(paramVarsta);
                    command.Parameters.Add(paramCnp);
                    command.Parameters.Add(paramPersoanaOficiu);

                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error saving participant", ex);
                MessageBox.Show($"Eroare la salvarea participantului: {ex.Message}");
                return false;
            }
        }


        public bool DeleteEntity(int id)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "DELETE FROM \"participanti\" WHERE \"id\" = @id";

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
                Log.Error("Error deleting participant", ex);
                return false;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }
        }

        public bool UpdateEntity(Participant entity)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText =
                        "UPDATE \"participanti\" SET \"nume\" = @nume, \"varsta\" = @varsta, \"cnp\" = @cnp, \"persoanaOficiu_id\" = @persoanaOficiuId WHERE \"id\" = @id";

                    var paramId = command.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = entity.Id;

                    var paramNume = command.CreateParameter();
                    paramNume.ParameterName = "@nume";
                    paramNume.Value = entity.Nume;

                    var paramVarsta = command.CreateParameter();
                    paramVarsta.ParameterName = "@varsta";
                    paramVarsta.Value = entity.Varsta;

                    var paramCnp = command.CreateParameter();
                    paramCnp.ParameterName = "@cnp";
                    paramCnp.Value = entity.Cnp;

                    var paramPersoanaOficiu = command.CreateParameter();
                    paramPersoanaOficiu.ParameterName = "@persoanaOficiuId";
                    paramPersoanaOficiu.Value = entity.idPersoanaOficiu;

                    command.Parameters.Add(paramId);
                    command.Parameters.Add(paramNume);
                    command.Parameters.Add(paramVarsta);
                    command.Parameters.Add(paramCnp);
                    command.Parameters.Add(paramPersoanaOficiu);

                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error updating participant", ex);
                return false;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }
        }

        public List<Participant> GetAll()
        {
            var participanti = new List<Participant>();
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM \"participanti\"";

                    var resultSet = command.ExecuteReader();
                    while (resultSet.Read())
                    {
                        var id = resultSet.GetInt32(0);
                        var Nume = resultSet.GetString(1);
                        var Varsta = resultSet.GetInt32(2);
                        var Cnp = resultSet.GetString(3);
                        var idPersoanaOficiu = resultSet.GetInt32(4);

                        participanti.Add(new Participant(id, Nume, Varsta, Cnp, idPersoanaOficiu));
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

            return participanti;

        }

        public Participant GetOne(int id)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM \"participanti\" WHERE \"id\" = @id";

                    var paramId = command.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = id;
                    command.Parameters.Add(paramId);

                    var resultSet = command.ExecuteReader();
                    if (resultSet.Read())
                    {
                        var name = resultSet.GetString(1);
                        var age = resultSet.GetInt32(2);
                        var cnp = resultSet.GetString(3);
                        var persoanaOficiuId = resultSet.GetInt32(4);
                        return new Participant(id, name, age, cnp, persoanaOficiuId);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error retrieving participant", ex);
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }

            return null;
        }

        public Participant GetParticipantByCNP1(string cnp)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM \"participanti\" WHERE \"cnp\" = @cnp";

                    var paramCnp = command.CreateParameter();
                    paramCnp.ParameterName = "@cnp";
                    paramCnp.Value = cnp;
                    command.Parameters.Add(paramCnp);

                    var resultSet = command.ExecuteReader();
                    if (resultSet.Read())
                    {
                        var id = resultSet.GetInt32(0);
                        var name = resultSet.GetString(1);
                        var age = resultSet.GetInt32(2);
                        var persoanaOficiuId = resultSet.GetInt32(4);
                        
                        return new Participant(id, name, age, cnp, persoanaOficiuId);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error retrieving participant by CNP", ex);
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }

            return null;
        }

        public Participant GetParticipantByCNP(string cnp)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM \"participanti\" WHERE \"cnp\" = @cnp";

                    var paramCnp = command.CreateParameter();
                    paramCnp.ParameterName = "@cnp";
                    paramCnp.Value = cnp;
                    command.Parameters.Add(paramCnp);

                    using (var resultSet = command.ExecuteReader())
                    {
                        if (resultSet.Read())
                        {
                            var id = resultSet.GetInt32(0);
                            var name = resultSet.GetString(1);
                            var age = resultSet.GetInt32(2);
                            var persoanaOficiuId = resultSet.GetInt32(4);

                            return new Participant(id, name, age, cnp, persoanaOficiuId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error retrieving participant by CNP", ex);
            }

            return null;
        }

        public bool IsCnpExists(string cnp)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT COUNT(*) FROM \"participanti\" WHERE \"cnp\" = @cnp";

                    var paramCnp = command.CreateParameter();
                    paramCnp.ParameterName = "@cnp";
                    paramCnp.Value = cnp;
                    command.Parameters.Add(paramCnp);

                    var resultSet = command.ExecuteReader();
                    if (resultSet.Read())
                    {
                        return resultSet.GetInt32(0) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error checking if CNP exists", ex);
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }

            return false;
        }

        public Participant GetParticipantByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Participant> GetParticipantsByAgeCategory(int ageMin, int ageMax)
        {
            throw new NotImplementedException();
        }

        public List<Participant> GetParticipantsByProba(int proba_id)
        {
            var participants = new List<Participant>();

            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                SELECT p.*
                FROM ""participanti"" p
                JOIN ""inscriere_proba"" ip ON p.""id"" = ip.""participant_id""
                WHERE ip.""proba_id"" = @proba_id;";

                    var paramCnp = command.CreateParameter();
                    paramCnp.ParameterName = "@proba_id";
                    paramCnp.Value = proba_id;
                    command.Parameters.Add(paramCnp);

                    var resultSet = command.ExecuteReader();

                    while (resultSet.Read()) // Modificare pentru a parcurge toate rezultatele
                    {
                        var id = resultSet.GetInt32(0);
                        var name = resultSet.GetString(1);
                        var age = resultSet.GetInt32(2);
                        var cnp = resultSet.GetString(3);
                        var persoanaOficiuId = resultSet.GetInt32(4);

                        // Adaugă participantul în listă
                        participants.Add(new Participant(id, name, age, cnp, persoanaOficiuId));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error retrieving participants by proba_id", ex);
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }

            return participants; // Returnează lista de participanți
        }
    }
}