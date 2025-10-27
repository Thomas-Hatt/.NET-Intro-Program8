using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program_8
{
    public class clsEmployee
    {
        // Public Properties
        public string LastName;
        public string FirstName;
        public int WeeklyHours;
        public double Rate;

        // Read-only properties
        public string FullName
        {
            get
            {
                return $"{LastName}, {FirstName}";
            }
        }

        public int AnnualHours
        {
            get
            {
                return WeeklyHours * 52;
            }
        }

        public double AnnualRate
        {
            get
            {
                if (WeeklyHours <= 40)
                {
                    return WeeklyHours * Rate * 52;
                }

                else
                {
                    {
                        double overtimeHours = WeeklyHours - 40;
                        return (40 * Rate * 52) + (overtimeHours * Rate * 1.5 * 52);
                    }
                }
            }
        }

        // Constructors 
        // Default constructor
        public clsEmployee()
        {
            LastName = string.Empty; 
            FirstName = string.Empty; 
            WeeklyHours = 0; 
            Rate = 0;
        }

        // Constructor with parameters
        public clsEmployee(string lastname, string firstname, double hourlyrate, int weeklyhours)
        {
            LastName = lastname;
            FirstName = firstname;
            Rate = hourlyrate;
            WeeklyHours = weeklyhours;
        }

        // Override ToString method
        public override string ToString() 
        { 
            return $"{LastName}, {FirstName}"; 
        }
    }
}