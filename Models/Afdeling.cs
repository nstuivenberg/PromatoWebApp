using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace PromatoWebApp.Models
{
    public class Afdeling
    {

        //[XmlAttribute]
        [XmlElement(Order = 1)]
        public String AfdelingsNaam
        {
            get;
            set;
        }

        [XmlAttribute]
        public int AfdelingsID
        {
            get;
            set;
        }


        //[XmlArrayAttribute("Afdelingsprojecten")]
        //[XmlElementAttribute(IsNullable = false)]
        [XmlArrayAttribute("Afdelingsprojecten", IsNullable = false, Order = 2)]
        public List<Project> afdelingsProjecten = new List<Project>();

        public bool HeeftAfdelingsProject(Project afdelingsproject)
        {
            bool b = false;

            if (afdelingsProjecten.Contains(afdelingsproject))
            {
                b = true;
            }
            return b;
        }

        public bool VoegAfdelingsProjectToe(Project afdelingsproject)
        {
            bool b = false;
            if (!HeeftAfdelingsProject(afdelingsproject))
            {
                b = true;
                afdelingsProjecten.Add(afdelingsproject);
            }
            return b;
        }

        public Project ZoekAfdelingsProjectViaId(int id)
        {
            foreach (Project p in afdelingsProjecten)
            {
                if (p.ProjectID == id)
                {
                    return p;
                }
            }
            return null;
        }

        public bool HasChildren()
        {
            bool b = false;
            if (afdelingsProjecten.Count() > 0)
            {
                return true;
            }
            return b;
        }

        public List<Project> GetProjectLijst()
        {
            return afdelingsProjecten;
        }
    }
}