using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonFiles
{
    public enum Genre
    {
        Action, Drama,
        Guide,
        Fairytale,
        Health_fitness,
        Fantasy,
        History,
        Graphic_novel,
        Home_and_garden,
        Fiction,
        Humor,
        Horror,
    }
    [Serializable]
    public class Book : AbstractItem
    {
        public Genre BookGenere { get; set; }
        public string WriterName { get; set; }
        public Book(string name, DateTime printdate, double edition, string desc, Genre genere, string writer, double price, double discount = 0, int quanty = 0) : base(name, printdate, edition, desc, price, discount, quanty)
        {
            BookGenere = genere;
            WriterName = writer;
        }
        public override string ToString()
        {
            return $"Name: {NameOfProduct} \n Print date:{PrintDate} \n Edition: {Edition} \n desc: {Description} \n Genre: {BookGenere} \n " +
                $"Writer: {WriterName} \n Quanity: {Quantity} \n Price: {Price} \n Discount: {Discount} \n ISBN: {ISBN}";
        }
    }
}
