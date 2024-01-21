using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeAvarageExperiment.Model
{

    public enum Genre
    {
        Male,
        Female
    }

    public class Employee
    {
        private string _name;

        public int Id { get; set; }
        public string Name
        {
            get { return _name; }

            set
            {
                // The name must have more than 3 Characters
                if (value.Length < 3)
                {
                    throw new FormatException("Name most have more than 10 Chars");
                }

                _name = value;
            }
        }
        public DateOnly BirthDate { get; set; }
        public Genre Genre { get; set; }
    }
}
