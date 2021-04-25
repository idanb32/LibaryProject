using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFiles
{
    public enum JournalGenre
    {
        Sport,
        Fashion,
        Fitness,
        Health,
        People
    }
    [Serializable]
    public class Journal : AbstractItem
    {
        public JournalGenre Genre { get; set; }
        public string Country { get; set; }
        public Journal(string name, DateTime printdate, double edition, JournalGenre genre, string desc, string country, double price, double discount = 0, int quanty = 0) : base(name, printdate, edition, desc, price, discount, quanty)
        {
            Country = country;
            Genre = genre;
        }
        public override string ToString()
        {
            return $"Name: {NameOfProduct} \n Print date:{PrintDate} \n Edition: {Edition} \n desc: {Description} \n Journal Genre: {Genre} \n " +
                $"Country: {Country} \n Quanity: {Quantity} \n Price: {Price} \n Discount: {Discount} \n ISBN: {ISBN}";
        }
    }
}
