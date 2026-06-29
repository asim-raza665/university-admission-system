using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University_Admission_System.Business_Layer;

namespace University_Admission_System.Data_Layer
{
    public class StudentDL
    {
        private List<Student> students = new List<Student>();

        public void AddStudent(Student s)
        {
            students.Add(s);
        }

        public List<Student> GetAllStudents()
        {
            return students;
        }

        public Student FindByName(string name)
        {
            foreach (Student s in students)
            {
                if (s.Name == name)
                    return s;
            }
            return null;
        }
    }
}
