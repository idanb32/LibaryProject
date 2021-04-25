using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFiles
{
    [Serializable]

    public abstract class AbstractItem
    {
        #region
        public string NameOfProduct { get; set; }
        public DateTime PrintDate { get; set; }
        public double Edition { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public Guid ISBN { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }
        #endregion
        public AbstractItem(string name, DateTime printdate, double edition, string desc, double price, double discount = 0, int quanty = 0)
        {
            NameOfProduct = name;
            PrintDate = printdate;
            Edition = edition;
            Quantity = quanty;
            Description = desc;
            ISBN = Guid.NewGuid();
            this.Price = price;
            this.Discount = discount;
        }

    }
}
