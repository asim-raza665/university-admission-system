using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Admission_System.Business_Layer
{
    public class AcademicProgram
    {
        // Properties
        public string DegreeTitle { get; set; }
        public int Duration { get; set; }
        public int Seats { get; set; }
        public int TotalCreditHrs { get; private set; }

        public List<Subject> Subjects { get; set; }
        public List<Student> EnrolledStudents { get; set; }
        public List<Student> Waitlist { get; set; }
        // Constructor
        public AcademicProgram(string degreeTitle, int duration, int seats)
        {
            DegreeTitle = degreeTitle;
            Duration = duration;
            Seats = seats;
            TotalCreditHrs = 0;
            Subjects = new List<Subject>();
            EnrolledStudents = new List<Student>();
            Waitlist = new List<Student>(); // add this line
        }

        // Add subject — max 20 credit hours per program
        public void AddSubject(Subject s)
        {
            if (TotalCreditHrs + s.CreditHours <= 20)
            {
                Subjects.Add(s);
                TotalCreditHrs += s.CreditHours;
                Console.WriteLine("Subject " + s.Code + " added to " + DegreeTitle);
            }
            else
            {
                Console.WriteLine("Cannot add subject. Program credit hour limit (20) reached!");
            }
        }

        // Add enrolled student
        public void AddStudent(Student st)
        {
            EnrolledStudents.Add(st);
        }

        // Display program info
        public void DisplayInfo()
        {
            Console.WriteLine("Degree Title  : " + DegreeTitle);
            Console.WriteLine("Duration      : " + Duration + " years");
            Console.WriteLine("Total Seats   : " + Seats);
            Console.WriteLine("Credit Hours  : " + TotalCreditHrs);
            Console.WriteLine("---------------------------");
        }
    }
}
