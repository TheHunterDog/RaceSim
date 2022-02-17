using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Track { 
         public String Name { get; set; }
        private LinkedList<Section> Sections { get; set; }

        public Track(String name, SectionTypes[] sections )
        {
            this.Name = name;
           // this.Sections = sections;
        }

        private LinkedList<Section> ConvertToSection(SectionTypes[] sectionTypes)
        {
            LinkedList<Section> result = new LinkedList<Section>();

            foreach(SectionTypes sectionType in sectionTypes)
            {
                Sections.AddLast(new Section(sectionType));
            }

            return result;
        }
    }
}
