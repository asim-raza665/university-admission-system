using System;
using System.Collections.Generic;

namespace University_Admission_System.Business_Layer
{
    public class AdmissionDept
    {
        public int AvailableSeats { get; set; }

        public List<Student> GenerateMeritList(List<Student> applicants)
        {
            List<Student> meritList = new List<Student>(applicants);
            for (int i = 0; i < meritList.Count - 1; i++)
            {
                for (int j = 0; j < meritList.Count - i - 1; j++)
                {
                    if (meritList[j].CalculateMerit() < meritList[j + 1].CalculateMerit())
                    {
                        Student temp = meritList[j];
                        meritList[j] = meritList[j + 1];
                        meritList[j + 1] = temp;
                    }
                }
            }
            return meritList;
        }

        public void AssignProgram(Student st, AcademicProgram p)
        {
            if (p.EnrolledStudents.Count < p.Seats)
            {
                st.AssignedProgram = p;
                st.AdmissionStatus = "Admitted";
                p.EnrolledStudents.Add(st);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(st.Name + " (" + st.StudentID + ") assigned to " + p.DegreeTitle);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No seats available in " + p.DegreeTitle);
                Console.ResetColor();
            }
        }

        public void ProcessAdmissions(List<Student> meritList)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n--- Processing Admissions ---");
            Console.ResetColor();

            foreach (Student st in meritList)
            {
                bool assigned = false;

                foreach (AcademicProgram pref in st.Preferences)
                {
                    if (pref.EnrolledStudents.Count < pref.Seats)
                    {
                        AssignProgram(st, pref);
                        assigned = true;
                        break;
                    }
                }

                if (!assigned)
                {
                    // Add to waitlist of first preference
                    if (st.Preferences.Count > 0)
                    {
                        st.Preferences[0].Waitlist.Add(st);
                        st.AdmissionStatus = "Waitlisted";
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("  " + st.Name +
                            " added to waitlist for " +
                            st.Preferences[0].DegreeTitle);
                        Console.ResetColor();
                    }
                    else
                    {
                        st.AdmissionStatus = "Rejected";
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("  " + st.Name +
                            " rejected — no preferences selected.");
                        Console.ResetColor();
                    }
                }
            }
        }

        public void DisplayMeritList(List<Student> meritList)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("                         MERIT LIST                             ");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════╣");
            Console.ResetColor();

            Console.WriteLine("{0,-5} {1,-12} {2,-20} {3,-10} {4,-10} {5,-10} {6,-10}",
                "Rank", "ID", "Name", "Matric%", "FSc%", "ECAT/400", "Aggregate");
            Console.WriteLine(new string('─', 80));

            int rank = 1;
            foreach (Student st in meritList)
            {
                float matricPercent = (float)Math.Round(
                    (st.MatricObtained / st.MatricTotal) * 100f, 1);
                float fscPercent = (float)Math.Round(
                    (st.FscObtained / st.FscTotal) * 100f, 1);

                Console.WriteLine("{0,-5} {1,-12} {2,-20} {3,-10} {4,-10} {5,-10} {6,-10}",
                    rank, st.StudentID, st.Name,
                    matricPercent + "%", fscPercent + "%",
                    st.EcatMarks + "/400",
                    st.CalculateMerit() + "%");
                rank++;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
        }

        // Summary report
        public void DisplaySummaryReport(List<Student> students,
                                  List<AcademicProgram> programs)
        {
            int totalApplicants = students.Count;
            int totalAdmitted = 0;
            int totalRejected = 0;
            int totalPending = 0;
            int totalWaitlisted = 0;

            foreach (Student st in students)
            {
                if (st.AdmissionStatus == "Admitted") totalAdmitted++;
                else if (st.AdmissionStatus == "Rejected") totalRejected++;
                else if (st.AdmissionStatus == "Waitlisted") totalWaitlisted++;
                else totalPending++;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔══════════════════════════════════════╗");
            Console.WriteLine("           SUMMARY REPORT               ");
            Console.WriteLine("  Date : " + DateTime.Now.ToString("dd-MM-yyyy  hh:mm tt"));
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.ResetColor();

            Console.WriteLine("  Total Applicants  : " + totalApplicants);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Total Admitted    : " + totalAdmitted);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  Total Waitlisted  : " + totalWaitlisted);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  Total Rejected    : " + totalRejected);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  Total Pending     : " + totalPending);
            Console.ResetColor();

            Console.WriteLine("\n  Program Capacity:");
            Console.WriteLine("  " + new string('─', 50));

            foreach (AcademicProgram p in programs)
            {
                int enrolled = p.EnrolledStudents.Count;
                int total = p.Seats;
                int remaining = total - enrolled;
                int waitCount = p.Waitlist != null ? p.Waitlist.Count : 0;
                float percent = total > 0 ? (float)enrolled / total * 100f : 0;

                // Fill bar
                int barFilled = (int)(percent / 10);
                int barEmpty = 10 - barFilled;

                Console.Write("  " + p.DegreeTitle.PadRight(33));

                // Color bar based on fill
                Console.Write(" [");
                if (percent >= 80)
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (percent >= 50)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.Green;

                for (int i = 0; i < barFilled; i++) Console.Write("█");
                Console.ResetColor();
                for (int i = 0; i < barEmpty; i++) Console.Write("░");
                Console.Write("] ");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(Math.Round(percent, 0) + "% filled");
                Console.ResetColor();

                if (waitCount > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("  (" + waitCount + " waiting)");
                    Console.ResetColor();
                }

                if (percent >= 80)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("  ⚠ Almost Full");
                    Console.ResetColor();
                }

                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
        }
        public void WithdrawStudent(Student st, AcademicProgram p)
        {
            if (st.AdmissionStatus != "Admitted")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  Student is not admitted.");
                Console.ResetColor();
                return;
            }

            p.EnrolledStudents.Remove(st);
            st.AssignedProgram = null;
            st.AdmissionStatus = "Rejected";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  " + st.Name + " withdrawn from " +
                p.DegreeTitle);
            Console.ResetColor();

            // Give seat to next on waitlist
            if (p.Waitlist != null && p.Waitlist.Count > 0)
            {
                Student next = p.Waitlist[0];
                p.Waitlist.RemoveAt(0);
                AssignProgram(next, p);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  " + next.Name +
                    " admitted from waitlist!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  No students on waitlist.");
                Console.ResetColor();
            }
        }
        public void DisplayWaitlist(List<AcademicProgram> programs)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔══════════════════════════════════════╗");
            Console.WriteLine("           WAITLIST REPORT              ");
            Console.WriteLine("  Date : " +
                DateTime.Now.ToString("dd-MM-yyyy  hh:mm tt"));
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.ResetColor();

            bool anyWaiting = false;

            foreach (AcademicProgram p in programs)
            {
                if (p.Waitlist != null && p.Waitlist.Count > 0)
                {
                    anyWaiting = true;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n  Program : " + p.DegreeTitle);
                    Console.ResetColor();
                    Console.WriteLine("  " + new string('─', 40));
                    Console.WriteLine("  {0,-5} {1,-15} {2,-12} {3,-10}",
                        "Pos", "Name", "ID", "Merit%");
                    Console.WriteLine("  " + new string('─', 40));

                    int pos = 1;
                    foreach (Student st in p.Waitlist)
                    {
                        Console.WriteLine("  {0,-5} {1,-15} {2,-12} {3,-10}",
                            pos, st.Name, st.StudentID,
                            st.CalculateMerit() + "%");
                        pos++;
                    }
                }
            }

            if (!anyWaiting)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  No students on waitlist.");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
        }
    }
}
