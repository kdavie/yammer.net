using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yammer
{
    public struct UserEducation
    {
        

        public static UserEducation Create(string school, string degree, string description, string startYear, string endYear)
        {
            UserEducation ue = new UserEducation();
            ue.School = school;
            ue.Degree = degree;
            ue.Description = description;
            ue.StartYear = startYear;
            ue.EndYear = endYear;

            return ue;
        }

        public string School { get; set; }
        public string Degree { get; set; }
        public string Description { get; set; }
        public string StartYear { get; set; }
        public string EndYear { get; set; }
    }
}
