using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using App.domain;

namespace App.repository
{
    public class InscriereDBRepo : IInscriereRepo
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(InscriereDBRepo));
        private readonly IDictionary<string, string> _props;

        public InscriereDBRepo(IDictionary<string, string> props)
        {
            Log.Info("Creating InscriereDBRepo ");
            _props = props;
        }

        public List<Inscriere> GetAll()
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM \"inscriere_proba\"";
                    var resultSet = command.ExecuteReader();
                    var inscrieri = new List<Inscriere>();
                    
                    while (resultSet.Read())
                    {
                        var idParticipant = resultSet.GetInt32(0);
                        var idProba = resultSet.GetInt32(1);
                        var idCategorie = resultSet.GetInt32(2);
                        var inscriere = new Inscriere(idParticipant, idProba, idCategorie);
                        Log.InfoFormat("Successfully retrieved inscriere with participant id {0} from database.", idParticipant);
                        inscrieri.Add(inscriere);
                    }

                    return inscrieri;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while retrieving inscriere from database.", ex);
                return null;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }
        }

        public Inscriere? GetOne(int entityId)
        {
            throw new NotImplementedException();
        }

        public Inscriere GetOne(string id)
        {
            return null;
        }

        public bool SaveEntity(Inscriere entity)
        {
            if (IsParticipantInscribedToTwoEvents(entity.IdParticipant))
            {
                Log.Warn("Participant is already inscribed to 2 events.");
                return false;  // Can't register if already inscribed to two events
            }

            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO \"inscriere_proba\" (\"participant_id\", \"proba_id\", \"categorie_id\") VALUES (@participantId, @probaId, @categorieId)";
                    var paramParticipantId = command.CreateParameter();
                    var paramProbaId = command.CreateParameter();
                    var paramCategorieId = command.CreateParameter();
                    paramParticipantId.ParameterName = "@participantId";
                    paramProbaId.ParameterName = "@probaId";
                    paramCategorieId.ParameterName = "@categorieId";
                    paramParticipantId.Value = entity.IdParticipant;
                    paramProbaId.Value = entity.IdProba;
                    paramCategorieId.Value = entity.idCategorie;
                    command.Parameters.Add(paramParticipantId);
                    command.Parameters.Add(paramProbaId);
                    command.Parameters.Add(paramCategorieId);
                    command.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while saving inscriere to database.", ex);
                return false;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }
        }

        public bool DeleteEntity(int entityId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEntity(string id)
        {
            return false;
        }

        public bool DeleteEntity(int idParticipant, int idProba, int idCategorie)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "DELETE FROM \"inscriere_proba\" WHERE participant_id = @participantId AND proba_id = @probaId AND categorie_id = @categorieId";
                    var paramParticipantId = command.CreateParameter();
                    var paramProbaId = command.CreateParameter();
                    var paramCategorieId = command.CreateParameter();
                    paramParticipantId.ParameterName = "@participantId";
                    paramProbaId.ParameterName = "@probaId";
                    paramCategorieId.ParameterName = "@categorieId";
                    paramParticipantId.Value = idParticipant;
                    paramProbaId.Value = idProba;
                    paramCategorieId.Value = idCategorie;
                    command.Parameters.Add(paramParticipantId);
                    command.Parameters.Add(paramProbaId);
                    command.Parameters.Add(paramCategorieId);
                    command.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while deleting inscriere from database.", ex);
                return false;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }
        }

        public bool UpdateEntity(Inscriere entity)
        {
            return false;
        }

        public Inscriere GetInscriereByParticipantAndProbaAndCategorie(int idParticipant, int idProba, int idCategorie)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM \"inscriere_proba\" WHERE participant_id = @participantId AND proba_id = @probaId AND categorie_id = @categorieId";
                    var paramParticipantId = command.CreateParameter();
                    var paramProbaId = command.CreateParameter();
                    var paramCategorieId = command.CreateParameter();
                    paramParticipantId.ParameterName = "@participantId";
                    paramProbaId.ParameterName = "@probaId";
                    paramCategorieId.ParameterName = "@categorieId";
                    paramParticipantId.Value = idParticipant;
                    paramProbaId.Value = idProba;
                    paramCategorieId.Value = idCategorie;
                    command.Parameters.Add(paramParticipantId);
                    command.Parameters.Add(paramProbaId);
                    command.Parameters.Add(paramCategorieId);
                    var resultSet = command.ExecuteReader();

                    if (resultSet.Read())
                    {
                        return new Inscriere(idParticipant, idProba, idCategorie);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while retrieving inscriere from database.", ex);
                return null;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }

            return null;
        }

        public Inscriere? GetInscriereByParticipantAndProba(int idParticipant, int idProba)
        {
            throw new NotImplementedException();
        }

        public int GetNumRegistrations(int idProba)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT COUNT(participant_id) FROM \"inscriere_proba\" WHERE proba_id = @probaId";
                    var paramProbaId = command.CreateParameter();
                    paramProbaId.ParameterName = "@probaId";
                    paramProbaId.Value = idProba;
                    command.Parameters.Add(paramProbaId);
                    var resultSet = command.ExecuteReader();

                    resultSet.Read();
                    return resultSet.GetInt32(0);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while getting number of registrations.", ex);
                return -1;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }
        }

        public List<Inscriere> GetInscrieriByParticipantId(int idParticipant)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM \"inscriere_proba\" WHERE participant_id = @participantId";
                    var paramParticipantId = command.CreateParameter();
                    paramParticipantId.ParameterName = "@participantId";
                    paramParticipantId.Value = idParticipant;
                    command.Parameters.Add(paramParticipantId);
                    var resultSet = command.ExecuteReader();
                    var inscrieri = new List<Inscriere>();
                    
                    while (resultSet.Read())
                    {
                        var idProba = resultSet.GetInt32(1);
                        var idCategorie = resultSet.GetInt32(2);
                        var inscriere = new Inscriere(idParticipant, idProba, idCategorie);
                        inscrieri.Add(inscriere);
                    }

                    return inscrieri;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while retrieving inscriere by participant id from database.", ex);
                return null;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }
        }

        public bool IsParticipantInscribedToTwoEvents(int idParticipant)
        {
            try
            {
                using (var connection = DbUtils.GetConnection(_props))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT COUNT(*) FROM \"inscriere_proba\" WHERE participant_id = @participantId";
                    var paramParticipantId = command.CreateParameter();
                    paramParticipantId.ParameterName = "@participantId";
                    paramParticipantId.Value = idParticipant;
                    command.Parameters.Add(paramParticipantId);
                    var resultSet = command.ExecuteReader();

                    resultSet.Read();
                    return resultSet.GetInt32(0) >= 2;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while checking participant inscriptions.", ex);
                return false;
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }
        }
        
       
        
        public Dictionary<string, Dictionary<string, int>> GetCompetitionsWithParticipants()
        {
            var result = new Dictionary<string, Dictionary<string, int>>();

            try
            {
                using (var connection = DbUtils.GetConnection(_props))  // Conexiunea este gestionată corect în acest bloc
                {
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                SELECT p.nume AS proba, 
                       c.varsta_min, 
                       c.varsta_max, 
                       COUNT(i.id) AS numar_inscrisi 
                FROM nume_probe p
                JOIN categorii c ON 1=1
                LEFT JOIN inscriere_proba i ON i.proba_id = p.id AND i.categorie_id = c.id
                GROUP BY p.nume, c.varsta_min, c.varsta_max;
            ";

                    using (var reader = command.ExecuteReader())  // Blocul reader-ului
                    {
                        while (reader.Read())
                        {
                            string proba = reader.GetString(reader.GetOrdinal("proba"));
                            int varstaMin = reader.GetInt32(reader.GetOrdinal("varsta_min"));
                            int varstaMax = reader.GetInt32(reader.GetOrdinal("varsta_max"));
                            int numarInscrisi = reader.GetInt32(reader.GetOrdinal("numar_inscrisi"));

                            string categorie = $"{varstaMin}-{varstaMax}";

                            if (!result.ContainsKey(proba))
                            {
                                result[proba] = new Dictionary<string, int>();
                            }

                            result[proba][categorie] = numarInscrisi;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error fetching competitions with participants: ", ex);
            }
            finally
            {
                // Close the connection after the operation is done
                DbUtils.CloseConnection();
            }

            return result;
        }


    }
}
