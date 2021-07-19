using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EPAM.AWARDS.Entities
{

    public enum Roll
    {
        guest,
        user,
        admin
    }
    public class User
    {
        private string name;
        [JsonProperty]
        private Roll _roll;
        [JsonIgnore]
        public Roll Roll { get => _roll; }
        public User() { }

        private List<Award> _awards = new List<Award>();

        public List<Award> Awards { get => _awards; set { _awards = value; } }

        [JsonProperty]
        private string passHash;
        [JsonIgnore]
        public string PassHash => passHash;

        public User(int id, string name, DateTime dateOfBirth, string passHash)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            this.passHash = passHash;
            _roll = Roll.user;
        }

        public User(int id, string name, DateTime dateOfBirth, string passHash, Roll roll)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            this.passHash = passHash;
            _roll = roll;
        }

        public User(int id, string name, DateTime dateOfBirth, string passHash, Roll roll, IEnumerable<Award> awards)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            this.passHash = passHash;
            _roll = roll;
            _awards = new List<Award>(awards);
        }
        public int Id { get; set; }
        public string Name
        {
            get => name;
            set
            {
                if (value != "")
                    name = value;
                else
                    throw new ArgumentException(nameof(name), "cannot be empty.");
            }
        }

        public TimeSpan Age
        {
            get
            {
                return DateTime.Now - DateOfBirth;
            }
        }

        private DateTime dateOfBirth;
      

        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                if (value < DateTime.Now)
                    dateOfBirth = value;
                else
                    throw new ArgumentException(nameof(DateOfBirth), name + "is from the future");
            }
        }



        bool HasAward(Award award)
        {
            return this.Awards.Find(a => a?.Id == award.Id) != null;
        }

        public static User HasAward(IEnumerable<User> users, Award award)
        {
            if (users == null)
            {
                return null;
            }
            foreach (var item in users)
            {
                if (item.HasAward(award))
                {
                    return item;
                }
            }
            return null;
        }
        public override string ToString()
        {
            return Id + " " + Name + " " + DateOfBirth.Year + Environment.NewLine + string.Join(Environment.NewLine + " ", Awards);
        }
    }
}
