using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Récupère tous les utilisateurs.
        /// </summary>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            // Connexion string à la base de données
            string cs = "server=localhost;userid=root;password=root;database=coursapi";
            List<User> users = new List<User>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(cs))
                {
                    conn.Open();
                    string query = "SELECT * FROM users";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User user = new User(
                                    Convert.ToInt32(reader["Id"]),
                                    Convert.ToString(reader["Name"]),
                                    Convert.ToString(reader["Email"]),
                                    Convert.ToString(reader["Password"]),
                                    Convert.ToString(reader["UserName"])
                                );
                                users.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }

            return users;
        }

        /// <summary>
        /// Récupère un utilisateur par son ID.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public User GetUserById(int id = 1)
        {
            // Connexion à la base de données
            string cs = "server=localhost;userid=root;password=root;database=coursapi";
            User? user = null;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(cs))
                {
                    conn.Open();
                    string query = "SELECT * FROM users WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new User(
                                    Convert.ToInt32(reader["Id"]),
                                    Convert.ToString(reader["Name"]),
                                    Convert.ToString(reader["Email"]),
                                    Convert.ToString(reader["Password"]),
                                    Convert.ToString(reader["UserName"])
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }

            return user;
        }

        /// <summary>
        /// Ajoute un nouvel utilisateur.
        /// </summary>
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            // Connexion à la base de données
            string cs = "server=localhost;userid=root;password=root;database=coursapi";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(cs))
                {
                    conn.Open();
                    string query = "INSERT INTO users (Id, Name, Email, Password, UserName) VALUES (@id, @Name, @Email, @Password, @UserName)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", user.Id);
                        cmd.Parameters.AddWithValue("@Name", user.Name);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.Parameters.AddWithValue("@UserName", user.UserName);
                        cmd.ExecuteNonQuery();
                    }
                }
                return Ok(); // 200 OK
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
                return StatusCode(500); // 500 Internal Server Error
            }
        }

        /// <summary>
        /// Supprime un utilisateur par son ID.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Connexion à la base de données
            string cs = "server=localhost;userid=root;password=root;database=coursapi";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(cs))
                {
                    conn.Open();
                    string query = "DELETE FROM users WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                return Ok(); // 200 OK
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
                return StatusCode(500); // 500 Internal Server Error
            }
        }

        /// <summary>
        /// Met à jour un utilisateur par son ID.
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User user)
        {
            // Connexion à la base de données
            string cs = "server=localhost;userid=root;password=root;database=coursapi";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(cs))
                {
                    conn.Open();
                    string query = "UPDATE users SET Name = @Name, Email = @Email, Password = @Password, UserName = @UserName WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@Name", user.Name);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.Parameters.AddWithValue("@UserName", user.UserName);
                        cmd.ExecuteNonQuery();
                    }
                }
                return Ok(); // Statut 200 OK pour indiquer que l'opération a réussi
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
                return StatusCode(500); // Statut 500 Internal Server Error pour indiquer une erreur serveur
            }
        }


    }
}
