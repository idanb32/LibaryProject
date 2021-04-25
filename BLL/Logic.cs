using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFiles;
using DAL;

namespace BLL
{
    public enum WhatToSortBy
    {
        Name, Date, Quantity, ISBN, Edition, Description, Price, Discount, TypeOfGenre, CountOrWriter, BookOrJournal
    }
    public class Logic
    {
        ItemRep myItems;
        public Logic()
        {
            myItems = new ItemRep();
        }
        public List<AbstractItem> GetAllItems()
        {
            return myItems.ItemList;
        }
        public void SortBy(WhatToSortBy whatWeSort, List<AbstractItem> ListWeSort)
        {
            switch (whatWeSort)
            {
                case WhatToSortBy.Name:
                    ListWeSort.Sort((X, Y) => X.NameOfProduct.CompareTo(Y.NameOfProduct));
                    break;
                case WhatToSortBy.Date:
                    ListWeSort.Sort((X, Y) => X.PrintDate.CompareTo(Y.PrintDate));
                    break;
                case WhatToSortBy.Quantity:
                    ListWeSort.Sort((X, Y) => X.Quantity.CompareTo(Y.Quantity));
                    break;
                case WhatToSortBy.ISBN:
                    ListWeSort.Sort((X, Y) => X.ISBN.CompareTo(Y.ISBN));
                    break;
                case WhatToSortBy.Edition:
                    ListWeSort.Sort((X, Y) => X.Edition.CompareTo(Y.Edition));
                    break;
                case WhatToSortBy.Description:
                    ListWeSort.Sort((X, Y) => X.Description.CompareTo(Y.Description));
                    break;
                case WhatToSortBy.Price:
                    ListWeSort.Sort((X, Y) => X.Price.CompareTo(Y.Price));
                    break;
                case WhatToSortBy.Discount:
                    ListWeSort.Sort((X, Y) => X.Discount.CompareTo(Y.Discount));
                    break;
                case WhatToSortBy.BookOrJournal:
                    ListWeSort.Sort(SortByType);
                    break;
                case WhatToSortBy.CountOrWriter:
                    ListWeSort.Sort(SortByCountryOrWriter);
                    break;
                case WhatToSortBy.TypeOfGenre:
                    ListWeSort.Sort(SortByTypeOfGenre);
                    break;
                default:
                    break;
            }
        }// get a list and sort by a spacific prop .
        private int SortByType(AbstractItem a, AbstractItem b)
        {
            if (a is Book)
            {
                Book firstItem = (Book)a;
                if (b is Book)
                {
                    Book secondItem = (Book)b;
                    return 0;
                }
                else
                {
                    return -13;
                }
            }
            if (a is Journal)
            {
                Journal firstItem = (Journal)a;
                if (b is Book)
                {
                    Book secondItem = (Book)b;
                    return 10;
                }
                else
                {
                    return 0;
                }
            }
            throw new NullReferenceException();
        }
        private int SortByCountryOrWriter(AbstractItem a, AbstractItem b)
        {
            if (a is Book)
            {
                Book firstItem = (Book)a;
                if (b is Book)
                {
                    Book secondItem = (Book)b;
                    return firstItem.WriterName.CompareTo(secondItem.WriterName);
                }
                else if (b is Journal)
                {
                    Journal secondItem = (Journal)b;
                    return firstItem.WriterName.CompareTo(secondItem.Country);
                }
            }
            else if (a is Journal)
            {
                Journal firstItem = (Journal)a;
                if (b is Book)
                {
                    Book secondItem = (Book)b;
                    return firstItem.Country.CompareTo(secondItem.WriterName);
                }
                else if (b is Journal)
                {
                    Journal secondItem = (Journal)b;
                    return firstItem.Country.CompareTo(secondItem.Country);
                }
            }
            throw new NullReferenceException();
        }
        private int SortByTypeOfGenre(AbstractItem a, AbstractItem b)
        {
            if (a is Book)
            {
                Book firstItem = (Book)a;
                if (b is Book)
                {
                    Book secondItem = (Book)b;
                    return firstItem.BookGenere.ToString().CompareTo(secondItem.BookGenere.ToString());
                }
                else if (b is Journal)
                {
                    Journal secondItem = (Journal)b;
                    return firstItem.BookGenere.ToString().CompareTo(secondItem.Genre.ToString());
                }
            }
            else if (a is Journal)
            {
                Journal firstItem = (Journal)a;
                if (b is Book)
                {
                    Book secondItem = (Book)b;
                    return firstItem.Genre.ToString().CompareTo(secondItem.BookGenere.ToString());
                }
                else if (b is Journal)
                {
                    Journal secondItem = (Journal)b;
                    return firstItem.Genre.ToString().CompareTo(secondItem.Genre.ToString());
                }
            }
            throw new NullReferenceException();
        }
        public void SortBy(WhatToSortBy whatWeSort)
        {
            SortBy(whatWeSort, myItems.ItemList);
            myItems.Save();
        }//if we dont get a list we sort by our rep list.
        public List<AbstractItem> Search(string howToSearch, string userSearch, string propToSearchBy)
        {
            if (howToSearch == "Journal")
                return GetAllJournal();
            else if (howToSearch == "Book")
                return GetAllBooks();
            else if (howToSearch == ">" || howToSearch == "<" || howToSearch == "=")
            {
                double userNumber = double.Parse(userSearch);
                return SearchByNumberProp(howToSearch, propToSearchBy, userNumber);
            }
            else if (howToSearch == "Start with" || howToSearch == "Have" || howToSearch == "Ends with")
            {
                return SearchByString(howToSearch, userSearch, propToSearchBy);
            }
            else
            {
                return SearchByGenre(howToSearch);
            }
        } // Search by user input, spacific prop, and how to sort that is changing acording to the prop we are trying to search by.
        private List<AbstractItem> SearchByGenre(string propToSearchBy)
        {
            List<AbstractItem> whatToReturn = new List<AbstractItem>();
            foreach (var item in myItems.ItemList)
            {
                if (item is Book)
                {
                    Book item1 = (Book)item;
                    if (item1.BookGenere.ToString() == propToSearchBy)
                        whatToReturn.Add(item);
                }
                else if (item is Journal)
                {
                    Journal item1 = (Journal)item;
                    if (item1.Genre.ToString() == propToSearchBy)
                        whatToReturn.Add(item);
                }
            }
            return whatToReturn;
        }
        private List<AbstractItem> SearchByString(string howToSearch, string userSearch, string propToSearchBy)
        {
            List<AbstractItem> whatToReturn = new List<AbstractItem>();
            foreach (var item in myItems.ItemList)
            {
                if (propToSearchBy == "Name")
                {
                    if (howToSearch == "Start with")
                    {
                        if (item.NameOfProduct.StartsWith(userSearch))
                            whatToReturn.Add(item);
                    }
                    else if (howToSearch == "Ends with")
                    {
                        if (item.NameOfProduct.EndsWith(userSearch))
                            whatToReturn.Add(item);
                    }
                    else
                        if (item.NameOfProduct.Contains(userSearch))
                        whatToReturn.Add(item);
                }

                else if (propToSearchBy == "Description")
                {
                    if (howToSearch == "Start with")
                    {
                        if (item.Description.StartsWith(userSearch))
                            whatToReturn.Add(item);
                    }
                    else if (howToSearch == "Ends with")
                    {
                        if (item.Description.EndsWith(userSearch))
                            whatToReturn.Add(item);
                    }
                    else
                        if (item.Description.Contains(userSearch))
                        whatToReturn.Add(item);
                }

                else if (propToSearchBy == "ISBN")
                {
                    if (howToSearch == "Start with")
                    {
                        if (item.ISBN.ToString().StartsWith(userSearch))
                            whatToReturn.Add(item);
                    }
                    else if (howToSearch == "Ends with")
                    {
                        if (item.ISBN.ToString().EndsWith(userSearch))
                            whatToReturn.Add(item);
                    }
                    else
                        if (item.ISBN.ToString().Contains(userSearch))
                        whatToReturn.Add(item);
                }

                else if (propToSearchBy == "Country / writter")
                {
                    if (item is Book)
                    {
                        Book item1 = (Book)item;
                        if (howToSearch == "Start with")
                        {
                            if (item1.WriterName.StartsWith(userSearch))
                                whatToReturn.Add(item);
                        }
                        else if (howToSearch == "Ends with")
                        {
                            if (item1.WriterName.EndsWith(userSearch))
                                whatToReturn.Add(item);
                        }
                        else
                            if (item1.WriterName.Contains(userSearch))
                            whatToReturn.Add(item);
                    }

                    else if (item is Journal)
                    {
                        Journal item1 = (Journal)item;
                        if (howToSearch == "Start with")
                        {
                            if (item1.Country.StartsWith(userSearch))
                                whatToReturn.Add(item);
                        }
                        else if (howToSearch == "Ends with")
                        {
                            if (item1.Country.EndsWith(userSearch))
                                whatToReturn.Add(item);
                        }
                        else
                            if (item1.Country.Contains(userSearch))
                            whatToReturn.Add(item);
                    }
                }
            }
            return whatToReturn;

        }
        private List<AbstractItem> SearchByNumberProp(string howToSearch, string prop, double number)
        {
            List<AbstractItem> whatToReturn = new List<AbstractItem>();
            foreach (var item in myItems.ItemList)
            {
                if (howToSearch == "=")
                {
                    if (prop == "Edition")
                    {
                        if (item.Edition == number)
                            whatToReturn.Add(item);
                    }
                    else if (prop == "Quanity")
                    {
                        if (item.Quantity == number)
                            whatToReturn.Add(item);
                    }
                    else if (prop == "Price")
                    {
                        if (item.Price == number)
                            whatToReturn.Add(item);
                    }
                    else
                    {
                        if (item.Discount == number)
                            whatToReturn.Add(item);
                    }
                }
                else if (howToSearch == ">")
                {
                    if (prop == "Edition")
                    {
                        if (item.Edition > number)
                            whatToReturn.Add(item);
                    }
                    else if (prop == "Quanity")
                    {
                        if (item.Quantity > number)
                            whatToReturn.Add(item);
                    }
                    else if (prop == "Price")
                    {
                        if (item.Price > number)
                            whatToReturn.Add(item);
                    }
                    else
                    {
                        if (item.Discount > number)
                            whatToReturn.Add(item);
                    }
                }
                else
                {
                    if (prop == "Edition")
                    {
                        if (item.Edition < number)
                            whatToReturn.Add(item);
                    }
                    else if (prop == "Quanity")
                    {
                        if (item.Quantity < number)
                            whatToReturn.Add(item);
                    }
                    else if (prop == "Price")
                    {
                        if (item.Price < number)
                            whatToReturn.Add(item);
                    }
                    else
                    {
                        if (item.Discount < number)
                            whatToReturn.Add(item);
                    }
                }
            }
            return whatToReturn;
        }
        private List<AbstractItem> GetAllBooks()
        {
            List<AbstractItem> AllMyBooks = new List<AbstractItem>();
            foreach (var item in myItems.ItemList)
            {
                if (item is Book)
                {
                    AllMyBooks.Add((Book)item);
                }
            }
            return AllMyBooks;
        }
        private List<AbstractItem> GetAllJournal()
        {
            List<AbstractItem> AllMyJournal = new List<AbstractItem>();
            foreach (var item in myItems.ItemList)
            {
                if (item is Journal)
                {
                    AllMyJournal.Add((Journal)item);
                }
            }
            return AllMyJournal;
        }
        public void RemoveAnItem(AbstractItem removeMe)
        {
            myItems.RemoveItem(removeMe);
        } // remove an item from our rep
        public void AddABook(string name, DateTime printdate, double edition, string desc, Genre genere, string writer, double price, double discount = 0, int quanty = 0)
        {
            Book ExtraBook = new Book(name, printdate, edition, desc, genere, writer, price, discount, quanty);
            myItems.AddItem(ExtraBook);
        }
        public void AddABook(string name, DateTime printdate, double edition, string desc, Genre genere, string writer, double price, Guid isbn, double discount = 0, int quanty = 0)
        {
            Book ExtraBook = new Book(name, printdate, edition, desc, genere, writer, price, discount, quanty);
            ExtraBook.ISBN = isbn;
            myItems.AddItem(ExtraBook);
        } // add a spacific book with an isbn that is known to us, its called only when we try to change a spacific item.
        public void AddAJournal(string name, DateTime printdate, double edition, JournalGenre genre, string desc, string country, double price, double discount = 0, int quanty = 0)
        {
            Journal ExtraJournal = new Journal(name, printdate, edition, genre, desc, country, price, discount, quanty);
            myItems.AddItem(ExtraJournal);
        }
        public void AddAJournal(string name, DateTime printdate, double edition, JournalGenre genre, string desc, string country, double price, Guid isbn, double discount = 0, int quanty = 0)
        {
            Journal ExtraJournal = new Journal(name, printdate, edition, genre, desc, country, price, discount, quanty);
            ExtraJournal.ISBN = isbn;
            myItems.AddItem(ExtraJournal);
        } // add a spacific Journal with an isbn that is known to us, its called only when we try to change a spacific item.
    }
}
