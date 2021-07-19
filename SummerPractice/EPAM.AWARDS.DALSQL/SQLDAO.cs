using EPAM.AWARDS.DAL.Interfaces;
using EPAM.AWARDS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace EPAM.AWARDS.DAL
{
    public class SQLDAO : IAwardsDAO
    {

        private static string _connectionStrong = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        private static SqlConnection _connection = new SqlConnection(_connectionStrong);



        public Award CreateAward(string title)
        {
            int id = 0;
            do
            {
                id++;
            } while (GetAward(id) != null);

            using (_connection = new SqlConnection(_connectionStrong))
            {
                var procedure = "CreateAward";
                SqlCommand command = new SqlCommand(procedure, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@Title", title);
                _connection.Open();
                var result = command.ExecuteNonQuery();
                return new Award(id, title);
            }
        }

        public User CreateUser(string name, DateTime dateOfBirth, string passHash)
        {
            int id = 0;
            do
            {
                id++;
            } while (GetUser(id) != null);

            using (_connection = new SqlConnection(_connectionStrong))
            {

                var procedure = "CreateUser";
                SqlCommand command = new SqlCommand(procedure, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
                command.Parameters.AddWithValue("@passHash", passHash);
                //todo
                if (name == "admin123")
                    command.Parameters.AddWithValue("@roll", 2);
                else
                    command.Parameters.AddWithValue("@roll", 1);
                _connection.Open();
                var result = command.ExecuteNonQuery();
                return new User(id, name, dateOfBirth, passHash);
            }
        }

        public bool DeleteAward(int id)
        {
            using (_connection = new SqlConnection(_connectionStrong))
            {
                var procedure = "DeleteAward";
                SqlCommand command = new SqlCommand(procedure, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@id", id);
                _connection.Open();
                var result = command.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool DeleteAward(Award Award)
        {
            return DeleteAward(Award.Id);
        }

        public bool DeleteUser(int id)
        {
            var procedure = "DeleteUser";
            using (_connection = new SqlConnection(_connectionStrong))
            {
                SqlCommand command = new SqlCommand(procedure, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@id", id);
                _connection.Open();
                var result = command.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool DeleteUser(User user)
        {
            return DeleteUser(user.Id);
        }

        public Award GetAward(int id)
        {
            using (_connection = new SqlConnection(_connectionStrong))
            {
                var stProc = "Award_GetByID";

                var command = new SqlCommand(stProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@id", id);
                _connection.Open();

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new Award(
                        id: (int)reader["id"],
                        title: (string)reader["title"]);

                }
                else
                {
                    return null;
                }

            }
        }

        public IEnumerable<Award> GetAwards(bool orderedById)
        {
            using (_connection = new SqlConnection(_connectionStrong))
            {
                var procedure_getAllUsers = "GetAwards";
                SqlCommand command = new SqlCommand(procedure_getAllUsers, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                _connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return new Award(
                        id: (int)reader["id"],
                        title: (string)reader["title"]);
                }
            }
        }


        public User GetUser(int id)
        {
            using (_connection = new SqlConnection(_connectionStrong))
            {
                var stProc = "User_GetByID";

                var command = new SqlCommand(stProc, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@id", id);
                _connection.Open();

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new User(
                        id: (int)reader["id"],
                        name: (string)reader["name"],
                        dateOfBirth: (DateTime)reader["dateOfBirth"],
                        passHash: (string)reader["passHash"],
                        roll: (Roll)reader["roll"],
                        awards: GetAllAwardOfUser((int)reader["id"]));
                }
                else
                {
                    return null;
                }

            }
        }



        public IEnumerable<User> GetUsers(bool orderedById)
        {
            using (_connection = new SqlConnection(_connectionStrong))
            {
                var procedure = "GetUsers";
                SqlCommand command = new SqlCommand(procedure, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                _connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return new User(
                        id: (int)reader["id"],
                        name: (string)reader["name"],
                        dateOfBirth: (DateTime)(reader["dateOfBirth"]),
                        passHash: (string)reader["passHash"],
                        roll: (Roll)reader["roll"],
                        awards: GetAllAwardOfUser((int)reader["id"]));
                }
            }
        }

        private IEnumerable<Award> GetAllAwardOfUser(int userID)
        {
            using (_connection = new SqlConnection(_connectionStrong))
            {
                var procedure = "GetAllAwardOfUser";
                SqlCommand command = new SqlCommand(procedure, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@idUser", userID);
                _connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return GetAward((int)reader["idAward"]);
                }
            }
        }


        public User UpdateUser(User user)
        {

            using (_connection = new SqlConnection(_connectionStrong))
            {
                var procedure = "UpdateUser";
                SqlCommand UpdateUser = new SqlCommand(procedure, _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                UpdateUser.Parameters.AddWithValue("@id", user.Id);
                UpdateUser.Parameters.AddWithValue("@name", user.Name);
                UpdateUser.Parameters.AddWithValue("@dateOfBirth", user.DateOfBirth);
                UpdateUser.Parameters.AddWithValue("@passHash", user.PassHash);
                UpdateUser.Parameters.AddWithValue("@roll", user.Roll);
                _connection.Open();

                var res = UpdateUser.ExecuteNonQuery();
                return res != 0 ? GetUser(user.Id) : null;
            }
            

        }

        public bool DeleteAllAwardOfUser(User user) 
        {
            int res = 0;
            using (_connection = new SqlConnection(_connectionStrong))
            {
                SqlCommand DeleteAllAwardOfUser = new SqlCommand("DeleteAllAwardOfUser", _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                DeleteAllAwardOfUser.Parameters.AddWithValue("@idUser", user.Id);
                _connection.Open();
                res = DeleteAllAwardOfUser.ExecuteNonQuery();
            }
            return 0 < res;

        }

        public bool AddAwardToUser(int idUser, int idAward)
        {
            int res = 0;
            using (_connection = new SqlConnection(_connectionStrong))
            {
                SqlCommand AddAward = new SqlCommand("AddAwardToUser", _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                AddAward.Parameters.AddWithValue("@idUser", idUser);
                AddAward.Parameters.AddWithValue("@idAward", idAward);
                _connection.Open();
                res =AddAward.ExecuteNonQuery();
            }
            return 0 < res;

        }

        public bool DeleteAwardForUser(int idUser, int idAward)
        {
            int res = 0;
            using (_connection = new SqlConnection(_connectionStrong))
            {
                SqlCommand DeleteAwardForUser = new SqlCommand("DeleteAwardForUser", _connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                DeleteAwardForUser.Parameters.AddWithValue("@idUser", idUser);
                DeleteAwardForUser.Parameters.AddWithValue("@idAward", idAward);
                _connection.Open();
                res = DeleteAwardForUser.ExecuteNonQuery();
            }
            return 0 < res;

        }
    }
}
