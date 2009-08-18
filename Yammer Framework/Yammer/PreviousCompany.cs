using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yammer
{
    public struct PreviousCompany
    {
        public static PreviousCompany Create(string company, string position, string description, string startYear, string endYear)
        {
            PreviousCompany pc = new PreviousCompany();
            pc.Company = company;
            pc.Position = position;
            pc.Description = description;
            pc.StartYear = startYear;
            pc.EndYear = endYear;

            return pc;
        }

        public string Company { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string StartYear { get; set; }
        public string EndYear { get; set; }
    }
}
