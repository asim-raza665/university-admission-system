using System;
using System.Collections.Generic;
using System.Threading;
using University_Admission_System.Business_Layer;
using University_Admission_System.Data_Layer;
using University_Admission_System.Presentation_Layer;

namespace University_Admission_System
{
    internal class Program
    {
        // ===== TYPEWRITER EFFECT =====
        static void Type(string text, int delay = 25,
            ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        // ===== LOADING BAR =====
        static void LoadBar(int total = 20, int delay = 40,
            ConsoleColor color = ConsoleColor.Green)
        {
            Console.Write("  [");
            Console.ForegroundColor = color;
            for (int i = 0; i < total; i++)
            {
                Console.Write("█");
                Thread.Sleep(delay);
            }
            Console.ResetColor();
            Console.WriteLine("] 100%");
        }

        // ===== STARTUP SCREEN =====
        static void ShowStartup(ProgramDL programDL,
            StudentDL studentDL)
        {
            Console.Clear();
            Console.Title = "University Admission System";

            int totalPrograms = programDL.GetAllPrograms().Count;
            int totalSeats = 0;
            foreach (AcademicProgram p in programDL.GetAllPrograms())
                totalSeats += p.Seats;
            int totalStudents = studentDL.GetAllStudents().Count;

            Thread.Sleep(200);

            Type("  ╔══════════════════════════════════════════════╗",
                10, ConsoleColor.DarkGray);
            Type("  ║      UNIVERSITY ADMISSION MANAGEMENT         ║",
                10, ConsoleColor.Cyan);
            Type("  ║                  SYSTEM                      ║",
                10, ConsoleColor.DarkGray);
            Type("  ║              Fall 2026                       ║",
                10, ConsoleColor.Yellow);
            Type("  ╠══════════════════════════════════════════════╣",
                10, ConsoleColor.DarkGray);
            Type("  ║          SYSTEM STATISTICS                   ║",
                10, ConsoleColor.DarkMagenta);

            Thread.Sleep(150);

            Console.ForegroundColor = ConsoleColor.Green;
            Type("  ║  Total Programs Available : " +
                totalPrograms.ToString().PadRight(17) + "║",
                10, ConsoleColor.Green);
            Type("  ║  Total Seats Available    : " +
                totalSeats.ToString().PadRight(17) + "║",
                10, ConsoleColor.Green);
            Type("  ║  Current Applicants       : " +
                totalStudents.ToString().PadRight(17) + "║",
                10, ConsoleColor.Green);
            Console.ResetColor();

            Type("  ╚══════════════════════════════════════════════╝",
                10, ConsoleColor.Cyan);

            Console.WriteLine();
            LoadBar(20, 40, ConsoleColor.Cyan);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Type("  Press any key to continue...", 30,
                ConsoleColor.Yellow);
            Console.ResetColor();
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            StudentDL studentDL = new StudentDL();
            ProgramDL programDL = new ProgramDL();
            SubjectDL subjectDL = new SubjectDL();

            SeedData(programDL, subjectDL, studentDL);
            ShowStartup(programDL, studentDL);

            Menu menu = new Menu();
            ProgramView programView =
                new ProgramView(programDL, subjectDL);
            StudentView studentView =
                new StudentView(studentDL, programDL);
            AdmissionView admissionView =
                new AdmissionView(studentDL, programDL);

            string roleChoice = "";

            while (roleChoice != "0")
            {
                menu.ShowRoleMenu();
                roleChoice = Console.ReadLine();

                switch (roleChoice)
                {
                    case "1":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Type("\n╔══════════════════════════════════╗",
                            8, ConsoleColor.Green);
                        Type("  ║                                  ║",
                            8, ConsoleColor.Green);
                        Type("  ║    Welcome to Student Portal     ║",
                            8, ConsoleColor.Green);
                        Type("  ║  Your Gateway to University      ║",
                            8, ConsoleColor.Green);
                        Type("  ║                                  ║",
                            8, ConsoleColor.Green);
                        Type("  ╚══════════════════════════════════╝",
                            8, ConsoleColor.Green);
                        Console.ResetColor();
                        Thread.Sleep(800);

                        string sc = "";
                        while (sc != "0")
                        {
                            menu.ShowStudentMenu();
                            sc = Console.ReadLine();
                            switch (sc)
                            {
                                case "1":
                                    studentView.AddStudent();
                                    break;
                                case "2":
                                    admissionView.ShowMeritList();
                                    break;
                                case "3":
                                    studentView.DisplayMyProfile();
                                    break;
                                case "4":
                                    studentView.RegisterSubjects();
                                    break;
                                case "5":
                                    admissionView.ShowFee();
                                    break;
                                case "6":
                                    studentView.CheckAdmissionStatus();
                                    break;
                                case "0":
                                    break;
                                default:
                                    Console.ForegroundColor =
                                        ConsoleColor.Red;
                                    Console.WriteLine(
                                        "  Invalid choice!");
                                    Console.ResetColor();
                                    Console.ReadKey();
                                    break;
                            }
                        }
                        break;

                    case "2":
                        Console.Clear();
                        Type("\n╔══════════════════════════════════╗",
                            8, ConsoleColor.Yellow);
                        Type("  ║                                  ║",
                            8, ConsoleColor.Yellow);
                        Type("  ║    Welcome Faculty Member        ║",
                            8, ConsoleColor.Yellow);
                        Type("  ║  Manage Programs & Subjects      ║",
                            8, ConsoleColor.Yellow);
                        Type("  ║                                  ║",
                            8, ConsoleColor.Yellow);
                        Type("  ╚══════════════════════════════════╝",
                            8, ConsoleColor.Yellow);
                        Console.ResetColor();
                        Thread.Sleep(800);

                        string fc = "";
                        while (fc != "0")
                        {
                            menu.ShowFacultyMenu();
                            fc = Console.ReadLine();
                            switch (fc)
                            {
                                case "1":
                                    programView.AddProgram();
                                    Console.ReadKey();
                                    break;
                                case "2":
                                    programView.AddSubjectToProgram();
                                    Console.ReadKey();
                                    break;
                                case "3":
                                    programView.DisplayAllPrograms();
                                    Console.ReadKey();
                                    break;
                                case "4":
                                    programView
                                        .DisplayProgramWithSubjects();
                                    Console.ReadKey();
                                    break;
                                case "5":
                                    programView
                                        .DisplayEnrolledStudents();
                                    Console.ReadKey();
                                    break;
                                case "0":
                                    break;
                                default:
                                    Console.ForegroundColor =
                                        ConsoleColor.Red;
                                    Console.WriteLine(
                                        "  Invalid choice!");
                                    Console.ResetColor();
                                    Console.ReadKey();
                                    break;
                            }
                        }
                        break;

                    case "3":
                        Console.Clear();
                        Type("\n╔══════════════════════════════════╗",
                            8, ConsoleColor.Magenta);
                        Type("  ║                                  ║",
                            8, ConsoleColor.Magenta);
                        Type("  ║      Welcome Admin               ║",
                            8, ConsoleColor.Magenta);
                        Type("  ║   Admission Control Center       ║",
                            8, ConsoleColor.Magenta);
                        Type("  ║                                  ║",
                            8, ConsoleColor.Magenta);
                        Type("  ╚══════════════════════════════════╝",
                            8, ConsoleColor.Magenta);
                        Console.ResetColor();
                        Thread.Sleep(800);

                        string ac = "";
                        while (ac != "0")
                        {
                            menu.ShowAdminMenu();
                            ac = Console.ReadLine();
                            switch (ac)
                            {
                                case "1":
                                    studentView.DisplayAllStudents();
                                    break;
                                case "2":
                                    admissionView.ShowMeritList();
                                    break;
                                case "3":
                                    admissionView.ProcessAdmissions();
                                    break;
                                case "4":
                                    studentView
                                        .SearchStudentByName();
                                    break;
                                case "5":
                                    admissionView.ShowSummaryReport();
                                    break;
                                case "6":
                                    admissionView
                                        .ShowUnassignedStudents();
                                    break;
                                case "7":
                                    admissionView.ShowWaitlist();
                                    break;
                                case "0":
                                    break;
                                default:
                                    Console.ForegroundColor =
                                        ConsoleColor.Red;
                                    Console.WriteLine(
                                        "  Invalid choice!");
                                    Console.ResetColor();
                                    Console.ReadKey();
                                    break;
                            }
                        }
                        break;

                    case "0":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\n  Exit? (y/n): ");
                        Console.ResetColor();
                        string confirm = Console.ReadLine();
                        if (confirm.ToLower() != "y")
                            roleChoice = "";
                        else
                        {
                            Console.Clear();
                            Type(
                                "\n  Thank you for using University System",
                                30, ConsoleColor.Cyan);
                            Type("  See you next time!",
                                40, ConsoleColor.Green);
                            Thread.Sleep(1000);
                        }
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("  Invalid choice!");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void SeedData(ProgramDL programDL, SubjectDL subjectDL,
     StudentDL studentDL)
        {
            // ===== PROGRAMS =====
            AcademicProgram cpe = new AcademicProgram(
                "BS Computer Engineering", 4, 60);
            Subject cpe1 = new Subject("CPE101", 3, "Core");
            Subject cpe2 = new Subject("CPE102", 3, "Core");
            Subject cpe3 = new Subject("CPE103", 2, "Core");
            subjectDL.AddSubject(cpe1); subjectDL.AddSubject(cpe2);
            subjectDL.AddSubject(cpe3);
            cpe.AddSubject(cpe1); cpe.AddSubject(cpe2);
            cpe.AddSubject(cpe3);
            programDL.AddProgram(cpe);

            AcademicProgram se = new AcademicProgram(
                "BS Software Engineering", 4, 50);
            Subject se1 = new Subject("SE101", 3, "Core");
            Subject se2 = new Subject("SE102", 3, "Core");
            Subject se3 = new Subject("SE103", 2, "Elective");
            subjectDL.AddSubject(se1); subjectDL.AddSubject(se2);
            subjectDL.AddSubject(se3);
            se.AddSubject(se1); se.AddSubject(se2);
            se.AddSubject(se3);
            programDL.AddProgram(se);

            AcademicProgram me = new AcademicProgram(
                "BS Mechanical Engineering", 4, 40);
            Subject me1 = new Subject("ME101", 3, "Core");
            Subject me2 = new Subject("ME102", 3, "Core");
            Subject me3 = new Subject("ME103", 2, "Core");
            subjectDL.AddSubject(me1); subjectDL.AddSubject(me2);
            subjectDL.AddSubject(me3);
            me.AddSubject(me1); me.AddSubject(me2);
            me.AddSubject(me3);
            programDL.AddProgram(me);

            AcademicProgram ee = new AcademicProgram(
                "BS Electrical Engineering", 4, 45);
            Subject ee1 = new Subject("EE101", 3, "Core");
            Subject ee2 = new Subject("EE102", 3, "Core");
            Subject ee3 = new Subject("EE103", 2, "Elective");
            subjectDL.AddSubject(ee1); subjectDL.AddSubject(ee2);
            subjectDL.AddSubject(ee3);
            ee.AddSubject(ee1); ee.AddSubject(ee2);
            ee.AddSubject(ee3);
            programDL.AddProgram(ee);

            AcademicProgram mte = new AcademicProgram(
                "BS Mechatronics Engineering", 4, 35);
            Subject mte1 = new Subject("MTE101", 3, "Core");
            Subject mte2 = new Subject("MTE102", 3, "Core");
            Subject mte3 = new Subject("MTE103", 2, "Core");
            subjectDL.AddSubject(mte1); subjectDL.AddSubject(mte2);
            subjectDL.AddSubject(mte3);
            mte.AddSubject(mte1); mte.AddSubject(mte2);
            mte.AddSubject(mte3);
            programDL.AddProgram(mte);

            // ===== DEFAULT STUDENTS =====
            Student s1 = new Student("Ahmed Ali", 19,
                1050, 1100, 1000, 1100, 380);
            s1.AddPreference(cpe);
            s1.AddPreference(se);
            studentDL.AddStudent(s1);

            Student s2 = new Student("Sara Khan", 20,
                980, 1100, 950, 1100, 350);
            s2.AddPreference(cpe);
            s2.AddPreference(ee);
            studentDL.AddStudent(s2);

            Student s3 = new Student("Ali Raza", 19,
                900, 1100, 880, 1100, 320);
            s3.AddPreference(se);
            s3.AddPreference(me);
            studentDL.AddStudent(s3);

            Student s4 = new Student("Zara Malik", 21,
                850, 1100, 820, 1100, 290);
            s4.AddPreference(ee);
            s4.AddPreference(cpe);
            studentDL.AddStudent(s4);

            Student s5 = new Student("Usman Tariq", 20,
                800, 1100, 780, 1100, 260);
            s5.AddPreference(mte);
            s5.AddPreference(me);
            studentDL.AddStudent(s5);

            Console.WriteLine();
        }
    }
}