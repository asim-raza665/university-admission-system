using System;

namespace University_Admission_System.Business_Layer
{
    public class FeeDept
    {
        private const float FeePerCreditHour = 5000f;
        private static int receiptCounter = 1001;

        public float CalculateFee(Student st)
        {
            return st.TotalRegisteredCredits * FeePerCreditHour;
        }

        public void DisplayFee(Student st)
        {
            string receiptNo = "RCP-" + receiptCounter++;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔══════════════════════════════════════════╗");
            Console.WriteLine("               FEE RECEIPT                  ");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.ResetColor();

            Console.WriteLine("  Receipt No     : " + receiptNo);
            Console.WriteLine("  Receipt No     : " + receiptNo);
            Console.WriteLine("  Date           : " +
                DateTime.Now.ToString("dd-MM-yyyy"));
            Console.WriteLine("  Time           : " +
                DateTime.Now.ToString("hh:mm tt"));
            Console.WriteLine("  Date           : " + DateTime.Now.ToString("dd-MM-yyyy"));
            Console.WriteLine("  Student ID     : " + st.StudentID);
            Console.WriteLine("  Student Name   : " + st.Name);

            if (st.AssignedProgram != null)
                Console.WriteLine("  Program        : " + st.AssignedProgram.DegreeTitle);

            Console.WriteLine("  " + new string('─', 42));
            Console.WriteLine("  {0,-15} {1,-12} {2,-10}",
                "Subject Code", "Type", "Credit Hrs");
            Console.WriteLine("  " + new string('─', 42));

            if (st.RegisteredSubjects.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  No subjects registered yet.");
                Console.ResetColor();
            }
            else
            {
                foreach (Subject s in st.RegisteredSubjects)
                {
                    Console.WriteLine("  {0,-15} {1,-12} {2,-10}",
                        s.Code, s.Type, s.CreditHours);
                }
            }

            Console.WriteLine("  " + new string('─', 42));
            Console.WriteLine("  Registered Hrs : " + st.TotalRegisteredCredits + " / 9");
            Console.WriteLine("  Fee Per Credit : Rs. " + FeePerCreditHour);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  TOTAL FEE      : Rs. " + CalculateFee(st));
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.ResetColor();
        }
    }
}
