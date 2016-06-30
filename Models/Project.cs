using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace PromatoWebApp.Models
{
    public class Project
    {
        //[XmlArrayAttribute("Mini Project(en)", IsNullable = false)]
        //[XmlElementAttribute(IsNullable=false), XmlArrayAttribute("test")]
        //[XmlElement (ElementName = "Project", IsNullable=false, Order=5)]
        [XmlArrayAttribute("Subprojecten", IsNullable = false, Order = 5)]
        public List<Project> subProjecten = new List<Project>();

        [XmlAttribute]
        public int ProjectID
        {
            get;
            set;
        }
        //[XmlAttribute]
        [XmlElement(Order = 1)]
        public String ProjectNaam
        {
            get;
            set;
        }
        [XmlElement(Order = 2)]
        public Double StartBudget
        {
            get;
            set;
        }
        [XmlElement(Order = 3)]
        public int StartDatum
        {
            get;
            set;
        }
        [XmlElement(Order = 4)]
        public int EindDatum
        {
            get;
            set;
        }

        public bool HeeftSubProjecten(Project sub)
        {
            bool b = false;
            if (subProjecten.Contains(sub))
            {
                b = true;
            }
            return b;
        }

        public bool VoegSubProjectToe(Project sub)
        {
            bool b = false;
            if (!HeeftSubProjecten(sub))
            {
                subProjecten.Add(sub);
                b = true;
            }
            return b;
        }

        public Project ZoekProjectViaId(int id)
        {
            foreach (Project p in subProjecten)
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
            if (subProjecten.Count() > 0)
            {
                return true;
            }
            return b;
        }
    }
}