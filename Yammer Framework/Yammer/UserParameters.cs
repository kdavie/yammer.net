using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yammer
{
    public class UserParameters
    {
        
 
    //* education[] (school,degree,description,start_year,end_year) - accepts multiple attributes
    //* i.e. education[]=UCLA,BS,Economics,1998,2002&education[]=USC,MBA,Finance,2002,2004
    //* previous_companies[] (company,position,description,start_year,end_year) - accepts multiple attributes
    //* i.e. previous_companies[]=Geni.com,Engineer,,2005,2008

        [User("email")]
        public string Email { get; set; }
        [User("full_name")]
        public string FullName { get; set; }
        [User("job_title")]
        public string JobTitle { get; set; }
        [User("location")]
        public string Location { get; set; }
        [User("im_provider")]
        public string ImProvider { get; set; }
        [User("im_username")]
        public string ImUserName { get; set; }
        [User("work_telephone")]
        public string WorkPhone { get; set; }
        [User("work_extension")]
        public string WorkExtension { get; set; }
        [User("mobile_telephone")]
        public string MobilePhone { get; set; }
        [User("external_profiles")]
        public string ExternalProfiles { get; set; }
        [User("significant_other")]
        public string SignificantOther { get; set; }
        [User("kids_names")]
        public string KidsNames { get; set; }
        [User("interests")]
        public string Interests { get; set; }
        [User("summary")]
        public string Summary { get; set; }
        [User("expertise")]
        public string Expertise { get; set; }
        [User("education[]")]
        public List<UserEducation> Education { get; set; }
        [User("previous_companies[]")]
        public List<PreviousCompany> PreviousCompanies { get; set; }

    }
    public class UserAttribute : System.Attribute
    {
        public string Name { get; set; }

        public UserAttribute(string name)
        {
            this.Name = name;
        }

    }
}
