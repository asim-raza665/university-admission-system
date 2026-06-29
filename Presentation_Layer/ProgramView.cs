using System;
using System.Collections.Generic;
using University_Admission_System.Business_Layer;
using University_Admission_System.Data_Layer;

namespace University_Admission_System.Presentation_Layer
{
    public class ProgramView
    {
        private ProgramDL programDL;
        private SubjectDL subjectDL;

        public ProgramView(ProgramDL programDL, SubjectDL subjectDL)
        {
            this.programDL = programDL;
            this.subjectDL = subjectDL;
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

        public void AddProgram()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n===== ADD NEW PROGRAM =====");
            Console.ResetColor();

            Console.Write("Enter Degree Title    : ");
            string title = Console.ReadLine();
            int duration = GetValidInt("Enter Duration (years): ", 1, 6);
            int seats = GetValidInt("Enter Total Seats     : ", 1, 500);

            AcademicProgram p = new AcademicProgram(title, duration, seats);
            programDL.AddProgram(p);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Program added successfully!");
            Console.ResetColor();
        }

        public void AddSubjectToProgram()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n===== ADD SUBJECT TO PROGRAM =====");
            Console.ResetColor();

            List<AcademicProgram> programs = programDL.GetAllPrograms();
            if (programs.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No programs available.");
                Console.ResetColor();
                return;
            }

            for (int i = 0; i < programs.Count; i++)
                Console.WriteLine("  " + (i + 1) + ". " + programs[i].DegreeTitle +
                    "  (Credits used: " + programs[i].TotalCreditHrs + "/20)");

            int choice = GetValidInt("Select Program Number: ", 1, programs.Count) - 1;
            AcademicProgram selected = programs[choice];

            Console.Write("Enter Subject Code             : ");
            string code = Console.ReadLine();
            int credits = GetValidInt("Enter Credit Hours             : ", 1, 6);
            Console.Write("Enter Subject Type (Core/Elective): ");
            string type = Console.ReadLine();

            Subject s = new Subject(code, credits, type);
            subjectDL.AddSubject(s);
            selected.AddSubject(s);
        }

        public void DisplayAllPrograms()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n===== ALL PROGRAMS =====");
            Console.ResetColor();

            List<AcademicProgram> programs = programDL.GetAllPrograms();
            if (programs.Count == 0)
            {
                Console.WriteLine("No programs found.");
                return;
            }

            foreach (AcademicProgram p in programs)
            {
                int remaining = p.Seats - p.EnrolledStudents.Count;
                Console.WriteLine("  Degree    : " + p.DegreeTitle);
                Console.WriteLine("  Duration  : " + p.Duration + " years");
                Console.WriteLine("  Seats     : " + p.Seats);
                Console.Write("  Remaining : ");
                if (remaining == 0)
                    Console.ForegroundColor = ConsoleColor.Red;
                else
                    Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(remaining);
                Console.ResetColor();
                Console.WriteLine(new string('─', 35));
            }
        }

        public void DisplayProgramWithSubjects()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n===== PROGRAM DETAILS WITH SUBJECTS =====");
            Console.ResetColor();

            List<AcademicProgram> programs = programDL.GetAllPrograms();
            if (programs.Count == 0)
            {
                Console.WriteLine("No programs found.");
                return;
            }

            for (int i = 0; i < programs.Count; i++)
                Console.WriteLine("  " + (i + 1) + ". " + programs[i].DegreeTitle);

            int choice = GetValidInt("Select Program: ", 1, programs.Count) - 1;
            AcademicProgram selected = programs[choice];

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n  Program   : " + selected.DegreeTitle);
            Console.WriteLine("  Duration  : " + selected.Duration + " years");
            Console.WriteLine("  Seats     : " + selected.Seats);
            Console.WriteLine("  Credits   : " + selected.TotalCreditHrs + " / 20");
            Console.WriteLine("\n  Subjects:");
            Console.WriteLine("  {0,-15} {1,-12} {2,-10}", "Code", "Type", "Credits");
            Console.WriteLine("  " + new string('─', 40));
            Console.ResetColor();

            if (selected.Subjects.Count == 0)
                Console.WriteLine("  No subjects added yet.");
            else
                foreach (Subject s in selected.Subjects)
                    Console.WriteLine("  {0,-15} {1,-12} {2,-10}",
                        s.Code, s.Type, s.CreditHours);
        }

        public void DisplayEnrolledStudents()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n===== ENROLLED STUDENTS PER PROGRAM =====");
            Console.ResetColor();

            List<AcademicProgram> programs = programDL.GetAllPrograms();
            if (programs.Count == 0)
            {
                Console.WriteLine("No programs found.");
                return;
            }

            for (int i = 0; i < programs.Count; i++)
                Console.WriteLine("  " + (i + 1) + ". " + programs[i].DegreeTitle);

            int choice = GetValidInt("Select Program: ", 1, programs.Count) - 1;
            AcademicProgram selected = programs[choice];

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n  Program  : " + selected.DegreeTitle);
            Console.WriteLine("  Enrolled : " + selected.EnrolledStudents.Count +
                              " / " + selected.Seats);
            Console.WriteLine("  " + new string('─', 45));
            Console.ResetColor();

            if (selected.EnrolledStudents.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  No students enrolled yet.");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("  {0,-12} {1,-20} {2,-10}",
                    "ID", "Name", "Merit%");
                Console.WriteLine("  " + new string('─', 45));
                foreach (Student st in selected.EnrolledStudents)
                {
                    Console.WriteLine("  {0,-12} {1,-20} {2,-10}",
                        st.StudentID, st.Name, st.CalculateMerit() + "%");
                }
            }
        }
    }
}

