using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Admission_System.Business_Layer
{
    public class Department
    {


        // Properties
        public string Name { get; set; }
        public List<AcademicProgram> Programs { get; set; }

        // Constructor
        public Department(string name)
        {
            Name = name;
            Programs = new List<AcademicProgram>();
        }

        // Add program to department
        public void AddProgram(AcademicProgram p)
        {
            Programs.Add(p);
            Console.WriteLine("Program " + p.DegreeTitle +
                              " added to department " + Name);
        }

        // Display all programs in department
        public void DisplayPrograms()
        {
            Console.WriteLine("Department: " + Name);
            Console.WriteLine("Programs Offered:");
            Console.WriteLine("---------------------------");

            if (Programs.Count == 0)
            {
                Console.WriteLine("No programs added yet.");
                return;
            }

            foreach (AcademicProgram p in Programs)
            {
                p.DisplayInfo();
            }
        }
    }
}
