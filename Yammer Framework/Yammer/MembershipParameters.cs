using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yammer
{
    public class MembershipParameters
    {

        public MembershipParameters()
        {
        }

        [MembershipParameter("page")]
        public int PageId { get; set; }
        [MembershipParameter("sort_by")]
        public SortBy SortBy { get; set; }
        [MembershipParameter("letter")]
        public string Letter { get; set; }
        [MembershipParameter("reverse")]
        public bool Reverse { get; set; }
        
    }

    public class MembershipParameterAttribute : System.Attribute
    {
        public string Name { get; set; }

        public MembershipParameterAttribute(string name)
        {
            this.Name = name;
        }

    }
}
