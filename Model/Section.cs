using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Section
    {
        SectionTypes SectionType { get; set; }
        public Section(SectionTypes sectionType)
        {
            this.SectionType = SectionType;
        }
    }
}
