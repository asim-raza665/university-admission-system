using System;

namespace University_Admission_System.Presentation_Layer
{
    public class Menu
    {
        public void ShowRoleMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║  UNIVERSITY ADMISSION MANAGEMENT     ║");
            Console.WriteLine("║            SYSTEM                    ║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.ResetColor();
            Console.WriteLine("║  Who are you?                        ║");
            Console.WriteLine("║                                      ║");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("║   1. Student                         ║");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("║   2. University Faculty              ║");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("║   3. Admin (Admission Department)    ║");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("║   0. Exit                            ║");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
            Console.Write("Enter Your Role: ");
        }

        public void ShowStudentMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║           STUDENT PORTAL             ║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.WriteLine("║   1. Apply for Admission             ║");
            Console.WriteLine("║   2. View Merit List                 ║");
            Console.WriteLine("║   3. View My Profile                 ║");
            Console.WriteLine("║   4. Register Subjects               ║");
            Console.WriteLine("║   5. View My Fee                     ║");
            Console.WriteLine("║   6. Check Admission Status          ║");
            Console.WriteLine("║   0. Back to Main Menu               ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
            Console.Write("Enter Choice: ");
        }

        public void ShowFacultyMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║          FACULTY PORTAL              ║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.WriteLine("║   1. Add Program                     ║");
            Console.WriteLine("║   2. Add Subject to Program          ║");
            Console.WriteLine("║   3. View All Programs               ║");
            Console.WriteLine("║   4. View Program with Subjects      ║");
            Console.WriteLine("║   5. View Enrolled Students          ║");
            Console.WriteLine("║   0. Back to Main Menu               ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
            Console.Write("Enter Choice: ");
        }
        public void ShowAdminMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║           ADMIN PORTAL               ║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.WriteLine("║   1. View All Students               ║");
            Console.WriteLine("║   2. Generate Merit List             ║");
            Console.WriteLine("║   3. Process Admissions              ║");
            Console.WriteLine("║   4. Search Student by ID            ║");
            Console.WriteLine("║   5. View Summary Report             ║");
            Console.WriteLine("║   6. View Unassigned Students        ║");
            Console.WriteLine("║   7. View Waitlist                   ║");
            Console.WriteLine("║   0. Back to Main Menu               ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
            Console.Write("Enter Choice: ");
        }
    }

}
