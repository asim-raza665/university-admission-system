using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University_Admission_System.Business_Layer;

namespace University_Admission_System.Data_Layer
{
    public class ProgramDL
    {
        private List<AcademicProgram> programs = new List<AcademicProgram>();

        public void AddProgram(AcademicProgram p)
        {
            programs.Add(p);
        }

        public List<AcademicProgram> GetAllPrograms()
        {
            return programs;
        }

        public AcademicProgram FindByTitle(string title)
        {
            foreach (AcademicProgram p in programs)
            {
                if (p.DegreeTitle == title)
                    return p;
            }
            return null;
        }
    }
}
