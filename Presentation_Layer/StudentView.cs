using System;
using System.Collections.Generic;
using System.Threading;
using University_Admission_System.Business_Layer;
using University_Admission_System.Data_Layer;

namespace University_Admission_System.Presentation_Layer
{
    public class StudentView
    {
        private StudentDL studentDL;
        private ProgramDL programDL;

        public StudentView(StudentDL studentDL, ProgramDL programDL)
        {
            this.studentDL = studentDL;
            this.programDL = programDL;
        }

        private int GetValidInt(string prompt, int min, int max)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out value) &&
                    value >= min && value <= max)
                    return value;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  Invalid! Enter between " +
                    min + " and " + max);
                Console.ResetColor();
            }
        }

        private float GetValidFloat(string prompt, float min, float max)
        {
            float value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (float.TryParse(input, out value) &&
                    value >= min && value <= max)
                    return value;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  Invalid! Enter between " +
                    min + " and " + max);
                Console.ResetColor();
            }
        }

        public void AddStudent()
        {
            Console.WriteLine("\n===== APPLY FOR ADMISSION =====");

            Console.Write("Enter Your Name : ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Name cannot be empty!");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            Student existing = studentDL.FindByName(name);
            if (existing != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Student already exists!");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            int age = GetValidInt("Enter Your Age : ", 15, 30);

            // ===== MATRIC MARKS =====
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n--- Matric (SSC) Marks ---");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  Total Marks of Matric (SSC) : ");
            Console.ResetColor();
            float matricTotal = GetValidFloat("", 1, 1100);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  Obtained Marks          : ");
            Console.ResetColor();
            float matricObtained = GetValidFloat("", 0, matricTotal);

            // ===== FSC MARKS =====
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n--- FSc Part 1 (HSSC) Marks ---");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Total Marks Inter (HSSC) : ");
            Console.ResetColor();
            float fscTotal = GetValidFloat("", 1, 1100);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  Obtained Marks          : ");
            Console.ResetColor();
            float fscObtained = GetValidFloat("", 0, fscTotal);

            // FSc check
            float fscPercent = (fscObtained / fscTotal) * 100f;
            if (fscPercent < 60)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  ADMISSION REJECTED!");
                Console.WriteLine("  Minimum 60% FSc required.");
                Console.WriteLine("  Your FSc: " +
                    Math.Round(fscPercent, 1) + "%");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            // ===== ECAT MARKS =====
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n--- ECAT Marks ---");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  ECAT Marks (0-400) : ");
            Console.ResetColor();
            float ecatMarks = GetValidFloat("", 0, 400);

            Student st = new Student(
                name, age,
                matricObtained, matricTotal,
                fscObtained, fscTotal,
                ecatMarks);

            // ===== AGGREGATE =====
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n===== AGGREGATE =====");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  Matric Contribution : " +
                Math.Round((matricObtained / matricTotal) * 17f, 2));
            Console.WriteLine("  FSc Contribution    : " +
                Math.Round((fscObtained / fscTotal) * 50f, 2));
            Console.WriteLine("  ECAT Contribution   : " +
                Math.Round((ecatMarks / 400f) * 33f, 2));
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  --------------------------------");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  TOTAL AGGREGATE     : " +
                st.CalculateMerit() + "%");
            Console.ResetColor();

            AddPreferences(st);
            studentDL.AddStudent(st);

            // ===== STAR CELEBRATION =====
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  ★                                  ★");
            Console.WriteLine("  ★   Application Submitted!         ★");
            Console.WriteLine("  ★   Student ID : " +
                st.StudentID.PadRight(18) + "★");
            Console.WriteLine("  ★   Good luck with your admission! ★");
            Console.WriteLine("  ★                                  ★");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★ ★");
            Console.ResetColor();

            Console.WriteLine("\n  Press any key to continue...");
            Console.ReadKey();
        }

        private void AddPreferences(Student st)
        {
            List<AcademicProgram> programs = programDL.GetAllPrograms();
            if (programs.Count == 0)
            {
                Console.WriteLine("No programs available.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n===== AVAILABLE PROGRAMS =====");
            Console.ResetColor();

            for (int i = 0; i < programs.Count; i++)
            {
                int remaining = programs[i].Seats -
                    programs[i].EnrolledStudents.Count;
                Console.Write("  " + (i + 1) + ". " +
                    programs[i].DegreeTitle);
                if (remaining == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" (FULL)");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" (" + remaining + " seats left)");
                }
                Console.ResetColor();
            }

            Console.WriteLine("\n  Add preferences in order.");
            Console.WriteLine("  Enter 0 to stop.");

            while (true)
            {
                int choice = GetValidInt(
                    "  Program Number : ", 0, programs.Count);
                if (choice == 0) break;
                st.AddPreference(programs[choice - 1]);
            }
        }

        public void DisplayMyProfile()
        {
            Console.WriteLine("\n===== VIEW MY PROFILE =====");
            List<Student> students = studentDL.GetAllStudents();
            if (students.Count == 0)
            {
                Console.WriteLine("No students found.");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < students.Count; i++)
                Console.WriteLine("  " + (i + 1) + ". " +
                    students[i].Name +
                    " (" + students[i].StudentID + ")");

            int choice = GetValidInt("  Enter Number : ",
                1, students.Count) - 1;
            students[choice].DisplayInfo();
            Console.ReadKey();
        }

        public void DisplayAllStudents()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n===== ALL STUDENTS =====");
            Console.ResetColor();

            List<Student> students = studentDL.GetAllStudents();
            if (students.Count == 0)
            {
                Console.WriteLine("No students found.");
                Console.ReadKey();
                return;
            }

            foreach (Student st in students)
                st.DisplayInfo();

            Console.ReadKey();
        }

        public void RegisterSubjects()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n===== REGISTER SUBJECTS =====");
            Console.ResetColor();

            List<Student> students = studentDL.GetAllStudents();
            if (students.Count == 0)
            {
                Console.WriteLine("No students found.");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < students.Count; i++)
                Console.WriteLine("  " + (i + 1) + ". " +
                    students[i].Name +
                    " (" + students[i].StudentID + ")");

            int choice = GetValidInt("  Enter Number : ",
                1, students.Count) - 1;
            Student selected = students[choice];

            if (selected.AssignedProgram == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  You are not admitted yet.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            List<Subject> subjects = selected.AssignedProgram.Subjects;
            if (subjects.Count == 0)
            {
                Console.WriteLine("  No subjects available.");
                Console.ReadKey();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n  Program : " +
                selected.AssignedProgram.DegreeTitle);
            Console.ResetColor();

            Console.WriteLine("  " + new string('─', 40));
            for (int i = 0; i < subjects.Count; i++)
                Console.WriteLine("  " + (i + 1) + ". " +
                    subjects[i].Code + " | " +
                    subjects[i].Type + " | CH: " +
                    subjects[i].CreditHours);
            Console.WriteLine("  " + new string('─', 40));

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  Credits : " +
                selected.TotalRegisteredCredits + " / 9");
            Console.ResetColor();

            while (true)
            {
                int sub = GetValidInt(
                    "  Subject Number (0 to stop): ",
                    0, subjects.Count);
                if (sub == 0) break;
                selected.RegisterSubject(subjects[sub - 1]);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  Credits : " +
                    selected.TotalRegisteredCredits + " / 9");
                Console.ResetColor();
            }
            Console.ReadKey();
        }

        public void CheckAdmissionStatus()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n===== CHECK ADMISSION STATUS =====");
            Console.ResetColor();

            List<Student> students = studentDL.GetAllStudents();
            if (students.Count == 0)
            {
                Console.WriteLine("No students found.");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < students.Count; i++)
                Console.WriteLine("  " + (i + 1) + ". " +
                    students[i].Name +
                    " (" + students[i].StudentID + ")");

            int choice = GetValidInt("  Enter Number : ",
                1, students.Count) - 1;
            Student st = students[choice];

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(
              "\n╔══════════════════════════════════════════╗");
            Console.WriteLine(
                "║         ADMISSION RESULT CARD            ║");
            Console.WriteLine(
                "╠══════════════════════════════════════════╣");
            Console.ResetColor();

            Console.WriteLine("║  Name    : " +
                st.Name.PadRight(31) + "║");
            Console.WriteLine("║  ID      : " +
                st.StudentID.PadRight(31) + "║");

            if (st.AssignedProgram != null)
                Console.WriteLine("║  Program : " +
                    st.AssignedProgram.DegreeTitle.PadRight(31) + "║");
            else
                Console.WriteLine("║  Program : " +
                    "Not Assigned".PadRight(31) + "║");

            Console.WriteLine("║  Merit   : " +
                (st.CalculateMerit() + "%").PadRight(31) + "║");

            Console.Write("║  Status  : ");
            if (st.AdmissionStatus == "Admitted")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("ADMITTED".PadRight(31) + "║");
                Console.ResetColor();
            }
            else if (st.AdmissionStatus == "Rejected")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("REJECTED".PadRight(31) + "║");
                Console.ResetColor();
            }
            else if (st.AdmissionStatus == "Waitlisted")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("WAITLISTED".PadRight(31) + "║");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("PENDING".PadRight(31) + "║");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(
                "╚══════════════════════════════════════════╝");
            Console.ResetColor();

            Console.WriteLine("\n  Press any key to continue...");
            Console.ReadKey();
        }

        // ===== SEARCH BY STUDENT ID =====
        public void SearchStudentByName()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n===== SEARCH STUDENT BY ID =====");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  Enter Student ID (e.g. UET-2026-001): ");
            Console.ResetColor();
            string id = Console.ReadLine();

            Student found = null;
            List<Student> students = studentDL.GetAllStudents();

            foreach (Student s in students)
            {
                if (s.StudentID == id)
                {
                    found = s;
                    break;
                }
            }

            if (found == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  No student found with ID: " + id);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  Student found!");
                Console.ResetColor();
                found.DisplayInfo();
            }

            Console.ReadKey();
        }
    }
}