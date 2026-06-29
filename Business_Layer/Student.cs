using System;
using System.Collections.Generic;

namespace University_Admission_System.Business_Layer
{
    public class Student
    {
        // Auto generated student ID
        private static int idCounter = 1;
        public string StudentID { get; private set; }

        public string Name { get; set; }
        public int Age { get; set; }
        public float MatricObtained { get; set; }
        public float MatricTotal { get; set; }
        public float FscObtained { get; set; }
        public float FscTotal { get; set; }
        public float EcatMarks { get; set; }
        public int TotalRegisteredCredits { get; private set; }

        public List<AcademicProgram> Preferences { get; set; }
        public List<Subject> RegisteredSubjects { get; set; }
        public AcademicProgram AssignedProgram { get; set; }

        // Admission status
        public string AdmissionStatus { get; set; }

        public Student(string name, int age,
                       float matricObtained, float matricTotal,
                       float fscObtained, float fscTotal,
                       float ecatMarks)
        {
            // Generate unique student ID
            StudentID = "UET-2026-" + idCounter.ToString("D3");
            idCounter++;

            Name = name;
            Age = age;
            MatricObtained = matricObtained;
            MatricTotal = matricTotal;
            FscObtained = fscObtained;
            FscTotal = fscTotal;
            EcatMarks = ecatMarks;
            TotalRegisteredCredits = 0;
            Preferences = new List<AcademicProgram>();
            RegisteredSubjects = new List<Subject>();
            AssignedProgram = null;
            AdmissionStatus = "Pending";
        }

        public bool MeetsFscRequirement()
        {
            float fscPercent = (FscObtained / FscTotal) * 100f;
            return fscPercent >= 60f;
        }

        public float CalculateMerit()
        {
            float matricComponent = (MatricObtained / MatricTotal) * 17f;
            float fscComponent = (FscObtained / FscTotal) * 50f;
            float ecatComponent = (EcatMarks / 400f) * 33f;
            float aggregate = matricComponent + fscComponent + ecatComponent;
            return (float)Math.Round(aggregate, 2);
        }

        public void AddPreference(AcademicProgram p)
        {
            Preferences.Add(p);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Preference added: " + p.DegreeTitle);
            Console.ResetColor();
        }

        public bool RegisterSubject(Subject s)
        {
            // Check duplicate subject
            foreach (Subject existing in RegisteredSubjects)
            {
                if (existing.Code == s.Code)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Subject " + s.Code + " is already registered!");
                    Console.ResetColor();
                    return false;
                }
            }

            if (TotalRegisteredCredits + s.CreditHours > 9)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cannot register " + s.Code +
                                  ". Credit hour limit (9) exceeded!");
                Console.ResetColor();
                return false;
            }

            if (TotalRegisteredCredits + s.CreditHours == 9)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Warning: You will reach maximum credit hours (9)!");
                Console.ResetColor();
            }
            else if (9 - (TotalRegisteredCredits + s.CreditHours) <= 3)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Warning: Only " +
                    (9 - TotalRegisteredCredits - s.CreditHours) +
                    " credit hours remaining!");
                Console.ResetColor();
            }

            RegisteredSubjects.Add(s);
            TotalRegisteredCredits += s.CreditHours;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Subject " + s.Code + " registered successfully.");
            Console.ResetColor();
            return true;
        }

        // Credit hour visual bar
        public string GetCreditBar()
        {
            int filled = TotalRegisteredCredits;
            int empty = 9 - filled;
            string bar = "[";
            for (int i = 0; i < filled; i++) bar += "█";
            for (int i = 0; i < empty; i++) bar += "░";
            bar += "] " + filled + "/9";
            return bar;
        }

        public void DisplayInfo()
        {
            float fscPercent = (float)Math.Round((FscObtained / FscTotal) * 100f, 1);
            float matricPercent = (float)Math.Round((MatricObtained / MatricTotal) * 100f, 1);
            float ecatPercent = (float)Math.Round((EcatMarks / 400f) * 100f, 1);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("           STUDENT PROFILE              ");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.ResetColor();

            Console.WriteLine("  Student ID    : " + StudentID);
            Console.WriteLine("  Name          : " + Name);
            Console.WriteLine("  Age           : " + Age);
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("  Matric Marks  : " + MatricObtained +
                              " / " + MatricTotal + "  (" + matricPercent + "%)");
            Console.WriteLine("  FSc Marks     : " + FscObtained +
                              " / " + FscTotal + "  (" + fscPercent + "%)");
            Console.WriteLine("  ECAT Marks    : " + EcatMarks +
                              " / 400  (" + ecatPercent + "%)");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("  Aggregate     : " + CalculateMerit() + " / 100");
            Console.WriteLine("--------------------------------------");

            // Admission status with color
            Console.Write("  Status        : ");
            if (AdmissionStatus == "Admitted")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("ADMITTED");
                Console.ResetColor();
                Console.WriteLine("  Program       : " + AssignedProgram.DegreeTitle);
            }
            else if (AdmissionStatus == "Rejected")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("REJECTED");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("PENDING");
                Console.ResetColor();
            }

            // Credit hour bar
            Console.WriteLine("  Credits       : " + GetCreditBar());

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
        }
    }
}

