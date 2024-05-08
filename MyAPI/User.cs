/*
 * Cette classe defini c'est quoi un user pour notre API
 */

namespace MyAPI
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; } = string.Empty;

        /*
         * Le constructeur de la classe 
         */
        public User(int _id, string _name, string _email, string _password, string _username) { 
            Id = _id;
            Name = _name;
            Email = _email;
            Password = _password;
            UserName = _username;
        }

        // Ajoutez un constructeur sans paramètres pour permettre la désérialisation
        public User() { }

        public override string ToString()
        {
            return $"Name : {Name} , ID : {Id}";
        }

    }
}
