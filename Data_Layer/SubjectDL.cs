using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University_Admission_System.Business_Layer;

namespace University_Admission_System.Data_Layer
{
    public class SubjectDL
    {
        private List<Subject> subjects = new List<Subject>();

        public void AddSubject(Subject s)
        {
            subjects.Add(s);
        }

        public List<Subject> GetAllSubjects()
        {
            return subjects;
        }

        public Subject FindByCode(string code)
        {
            foreach (Subject s in subjects)
            {
                if (s.Code == code)
                    return s;
            }
            return null;
        }
    }
}
