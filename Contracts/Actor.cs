using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class Actor
    {
        public int id { get; set; }
        public string name { get; set; }
        public string birthDate { get; set; }
        public string role { get; set; }

        public Actor(string name, string birthDate, string role)
        {
            this.name = name;
            this.birthDate = birthDate;
            this.role = role;
        }

        public Actor()
        {

        }

        public override string ToString()
        {
            return "Person: " + id + "" + name + " " + birthDate + " " + role;
        }

    }
}
