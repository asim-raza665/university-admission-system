using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Admission_System.Business_Layer
{
    public class Subject
    {
        // Properties
        public string Code { get; set; }
        public int CreditHours { get; set; }
        public string Type { get; set; }  // Core / Elective

        // Constructor
        public Subject(string code, int creditHours, string type)
        {
            Code = code;
            CreditHours = creditHours;
            Type = type;
        }

        // Display subject info
        public void DisplayInfo()
        {
            Console.WriteLine("Subject Code : " + Code);
            Console.WriteLine("Credit Hours : " + CreditHours);
            Console.WriteLine("Type         : " + Type);
            Console.WriteLine("---------------------------");
        }
    }
}
