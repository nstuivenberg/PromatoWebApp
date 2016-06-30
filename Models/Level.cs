using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PromatoWebApp.Models
{
    public class Level
    {
        public string name { get; set; }
        public string elementName { get; set; }
        public int elementID { get; set; }
        public int startDatum { get; set; }
        public int eindDatum { get; set; }
        public List<Level> subLevels { get; set; }

        public Level()
        {
        }
    }
}