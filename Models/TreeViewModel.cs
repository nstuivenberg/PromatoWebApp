using System.Collections.Generic;

namespace PromatoWebApp.Models
{
    public class TreeViewModel
    {
        public TreeViewModel()
        {
            this.List = new List<TreeViewModel>();
        }

        //type + _ + id
        //bij bedrijf alleen Bedrijf
        //Let op hoofdletter
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<TreeViewModel> List { get; private set; }
        public bool IsChild
        {
            get
            {
                return this.List.Count == 0;
            }
        }
    }
}