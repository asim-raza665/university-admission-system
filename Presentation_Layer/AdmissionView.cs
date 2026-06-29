using System;
using System.Collections.Generic;
using University_Admission_System.Business_Layer;
using University_Admission_System.Data_Layer;

namespace University_Admission_System.Presentation_Layer
{
    public class AdmissionView
    {
        private StudentDL studentDL;
        private ProgramDL programDL;
        private AdmissionDept admissionDept;
        private FeeDept feeDept;

        public AdmissionView(StudentDL studentDL, ProgramDL programDL)
        {
            this.studentDL = studentDL;
            this.programDL = programDL;
            this.admissionDept = new AdmissionDept();
            this.feeDept = new FeeDept();
        }

        private int GetValidInt(string prompt, int min, int max)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out value) && value >= min && value <= max)
                    return value;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid! Enter between " + min + " and " + max);
                Console.ResetColor();
            }
        }

        public void ShowMeritList()
        {
            List<Student> students = studentDL.GetAllStudents();
            if (students.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No students found.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }
            List<Student> meritList = admissionDept.GenerateMeritList(students);
            admissionDept.DisplayMeritList(meritList);
            Console.ReadKey();
        }

        public void ProcessAdmissions()
        {
            List<Student> students = studentDL.GetAllStudents();
            if (students.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No students found.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }
            List<Student> meritList = admissionDept.GenerateMeritList(students);
            admissionDept.ProcessAdmissions(meritList);
            Console.ReadKey();
        }

        public void ShowFee()
        {
            Console.WriteLine("\n===== VIEW FEE =====");
            List<Student> students = studentDL.GetAllStudents();
            if (students.Count == 0)
            {
                Console.WriteLine("No students found.");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < students.Count; i++)
                Console.WriteLine("  " + (i + 1) + ". " +
                    students[i].Name + " (" + students[i].StudentID + ")");

            int choice = GetValidInt("Enter Number: ", 1, students.Count) - 1;
            feeDept.DisplayFee(students[choice]);
            Console.ReadKey();
        }

        public void ShowSummaryReport()
        {
            List<Student> students = studentDL.GetAllStudents();
            List<AcademicProgram> programs = programDL.GetAllPrograms();
            admissionDept.DisplaySummaryReport(students, programs);
            Console.ReadKey();
        }
        public void ShowWaitlist()
        {
            List<AcademicProgram> programs = programDL.GetAllPrograms();
            admissionDept.DisplayWaitlist(programs);
            Console.ReadKey();
        }

        public void ShowUnassignedStudents()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n===== UNASSIGNED STUDENTS =====");
            Console.ResetColor();

            List<Student> students = studentDL.GetAllStudents();
            bool found = false;

            foreach (Student st in students)
            {
                if (st.AdmissionStatus == "Rejected" ||
                    st.AdmissionStatus == "Pending")
                {
                    Console.WriteLine("  ID     : " + st.StudentID);
                    Console.WriteLine("  Name   : " + st.Name);
                    Console.Write("  Status : ");
                    if (st.AdmissionStatus == "Rejected")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("REJECTED");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("PENDING");
                    }
                    Console.ResetColor();
                    Console.WriteLine("  Merit  : " + st.CalculateMerit() + "%");
                    Console.WriteLine(new string('─', 35));
                    found = true;
                }
            }

            if (!found)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("All students have been assigned programs!");
                Console.ResetColor();
            }

            Console.ReadKey();
        }

        internal void ShowMeritGraph()
        {
            throw new NotImplementedException();
        }
    }
}
