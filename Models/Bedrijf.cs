using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace PromatoWebApp.Models
{
    public class Bedrijf
    {
        public Bedrijf()
        {
        }

        public Bedrijf(String naam)
        {
            this.BedrijfsNaam = naam;
        }

        //[XmlAttribute]
        //Deleted because use of XMLArrayAttribute
        [XmlElement(Order = 1)]
        public String BedrijfsNaam
        {
            get;
            set;
        }
        [XmlArrayAttribute("Afdelingen", Order = 2, IsNullable = false)]
        public List<Afdeling> afdelingen = new List<Afdeling>();

        public Boolean HeeftAfdeling(Afdeling a)
        {
            Boolean b = false;
            if (afdelingen.Contains(a))
            {
                b = true;
            }
            return b;
        }

        public Boolean VoegAfdelingToe(Afdeling a)
        {
            Boolean b = false;
            if (!HeeftAfdeling(a))
            {
                afdelingen.Add(a);
                b = true;
            }

            return b;
        }
        public Afdeling ZoekAfdelingViaId(int id)
        {
            foreach (Afdeling a in afdelingen)
            {
                if (a.AfdelingsID == id)
                {
                    return a;
                }
            }

            return null;
        }

        public bool HasChildren()
        {
            bool b = false;
            if (afdelingen.Count() > 0)
            {
                return true;
            }
            return b;
        }

        public List<Afdeling> getAfdelingLijst()
        {
            return afdelingen;
        }
    }
}