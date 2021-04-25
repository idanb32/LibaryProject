
using System;
using System.Collections.Generic;
using BLL;
using CommonFiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        Logic testLogic = new Logic();
        [TestMethod]
        public void CheckSortByType()
        {
            List<AbstractItem> letsCheck = new List<AbstractItem>();
            Book book1 = new Book("a", DateTime.MinValue, 1, "a", Genre.Action, "a", 0, 0, 0);
            Journal journal2 = new Journal("b", DateTime.MaxValue, 2, JournalGenre.Fashion, "b", "b", 1, 1, 1);
            letsCheck.Add(journal2);
            letsCheck.Add(book1);
            testLogic.SortBy(WhatToSortBy.BookOrJournal, letsCheck);
            Assert.IsTrue(letsCheck[0] == book1);
        }
        [TestMethod]
        public void CheckSortByCountOrWriter()
        {
            List<AbstractItem> letsCheck = new List<AbstractItem>();
            Book book1 = new Book("a", DateTime.MinValue, 1, "a", Genre.Action, "a", 0, 0, 0);
            Journal journal2 = new Journal("b", DateTime.MaxValue, 2, JournalGenre.Fashion, "b", "b", 1, 1, 1);
            letsCheck.Add(journal2);
            letsCheck.Add(book1);
            testLogic.SortBy(WhatToSortBy.CountOrWriter, letsCheck);
            Assert.IsTrue(letsCheck[0] == book1);
        }

        [TestMethod]
        public void CheckSortByDate()
        {
            List<AbstractItem> letsCheck = new List<AbstractItem>();
            Book book1 = new Book("a", DateTime.MinValue, 1, "a", Genre.Action, "a", 0, 0, 0);
            Journal journal2 = new Journal("b", DateTime.MaxValue, 2, JournalGenre.Fashion, "b", "b", 1, 1, 1);
            letsCheck.Add(journal2);
            letsCheck.Add(book1);
            testLogic.SortBy(WhatToSortBy.Date, letsCheck);
            Assert.IsTrue(letsCheck[0] == book1);
        }
        [TestMethod]
        public void CheckSortByDescription()
        {
            List<AbstractItem> letsCheck = new List<AbstractItem>();
            Book book1 = new Book("a", DateTime.MinValue, 1, "a", Genre.Action, "a", 0, 0, 0);
            Journal journal2 = new Journal("b", DateTime.MaxValue, 2, JournalGenre.Fashion, "b", "b", 1, 1, 1);
            letsCheck.Add(journal2);
            letsCheck.Add(book1);
            testLogic.SortBy(WhatToSortBy.Description, letsCheck);
            Assert.IsTrue(letsCheck[0] == book1);
        }
        [TestMethod]
        public void CheckSortByDiscount()
        {
            List<AbstractItem> letsCheck = new List<AbstractItem>();
            Book book1 = new Book("a", DateTime.MinValue, 1, "a", Genre.Action, "a", 0, 0, 0);
            Journal journal2 = new Journal("b", DateTime.MaxValue, 2, JournalGenre.Fashion, "b", "b", 1, 1, 1);
            letsCheck.Add(journal2);
            letsCheck.Add(book1);
            testLogic.SortBy(WhatToSortBy.Discount, letsCheck);
            Assert.IsTrue(letsCheck[0] == book1);
        }
        [TestMethod]
        public void CheckSortByEdition()
        {
            List<AbstractItem> letsCheck = new List<AbstractItem>();
            Book book1 = new Book("a", DateTime.MinValue, 1, "a", Genre.Action, "a", 0, 0, 0);
            Journal journal2 = new Journal("b", DateTime.MaxValue, 2, JournalGenre.Fashion, "b", "b", 1, 1, 1);
            letsCheck.Add(journal2);
            letsCheck.Add(book1);
            testLogic.SortBy(WhatToSortBy.Edition, letsCheck);
            Assert.IsTrue(letsCheck[0] == book1);
        }
        [TestMethod]
        public void CheckSortByName()
        {
            List<AbstractItem> letsCheck = new List<AbstractItem>();
            Book book1 = new Book("a", DateTime.MinValue, 1, "a", Genre.Action, "a", 0, 0, 0);
            Journal journal2 = new Journal("b", DateTime.MaxValue, 2, JournalGenre.Fashion, "b", "b", 1, 1, 1);
            letsCheck.Add(journal2);
            letsCheck.Add(book1);
            testLogic.SortBy(WhatToSortBy.Name, letsCheck);
            Assert.IsTrue(letsCheck[0] == book1);
        }
        [TestMethod]
        public void CheckSearchGenere()
        {
            testLogic.AddABook("a", DateTime.MinValue, 1, "a", Genre.Action, "a", 0, 0, 0);
            List<AbstractItem> letsCheck = testLogic.Search("Action", "", "");
            foreach (var item in letsCheck)
            {
                if (item is Book)
                {
                    Book item1 = (Book)item;
                    if (item1.NameOfProduct == "a" && item1.PrintDate == DateTime.MinValue
                        && item1.Price == 0 && item1.Quantity == 0 && item1.Discount == 0 && item1.Description == "a" &&
                        item1.Edition == 1 && item1.WriterName == "a" && item1.BookGenere == Genre.Action)
                    {
                        Assert.IsTrue(true);
                        return;
                    }
                }
            }
            Assert.IsTrue(false);
        }
        [TestMethod]
        public void CheckSearchBiggerNum()
        {
            testLogic.AddABook("a", DateTime.MinValue, 1, "a", Genre.Action, "a", 0, 0, 0);
            List<AbstractItem> letsCheck = testLogic.Search(">", "-1", "Edition");
            foreach (var item in letsCheck)
            {
                Book item1;
                if (item is Book)
                {
                    item1 = (Book)item;
                    if (item1.NameOfProduct == "a" && item1.PrintDate == DateTime.MinValue
                        && item1.Price == 0 && item1.Quantity == 0 && item1.Discount == 0 && item1.Description == "a" &&
                        item1.Edition == 1 && item1.WriterName == "a" && item1.BookGenere == Genre.Action)
                    {
                        Assert.IsTrue(true);
                        return;
                    }
                }
            }
            Assert.IsTrue(false);
        }
        [TestMethod]
        public void CheckSearchSmallerNum()
        {
            testLogic.AddABook("a", DateTime.MinValue, 1, "a", Genre.Action, "a", 0, 0, 0);
            List<AbstractItem> letsCheck = testLogic.Search("<", "10", "Edition");
            foreach (var item in letsCheck)
            {
                Book item1;
                if (item is Book)
                {
                    item1 = (Book)item;
                    if (item1.NameOfProduct == "a" && item1.PrintDate == DateTime.MinValue
                        && item1.Price == 0 && item1.Quantity == 0 && item1.Discount == 0 && item1.Description == "a" &&
                        item1.Edition == 1 && item1.WriterName == "a" && item1.BookGenere == Genre.Action)
                    {
                        Assert.IsTrue(true);
                        return;
                    }
                }
            }
            Assert.IsTrue(false);
        }
        [TestMethod]
        public void CheckSearchHave() 
        {
            testLogic.AddABook("a", DateTime.MinValue, 1, "a", Genre.Action, "a", 0, 0, 0);
            List<AbstractItem> letsCheck = testLogic.Search("Have", "a", "Name");
            foreach (var item in letsCheck)
            {
                Book item1;
                if (item is Book)
                {
                    item1 = (Book)item;
                    if (item1.NameOfProduct == "a" && item1.PrintDate == DateTime.MinValue
                        && item1.Price == 0 && item1.Quantity == 0 && item1.Discount == 0 && item1.Description == "a" &&
                        item1.Edition == 1 && item1.WriterName == "a" && item1.BookGenere == Genre.Action)
                    {
                        Assert.IsTrue(true);
                        return;
                    }
                }
            }
            Assert.IsTrue(false);
        }
        [TestMethod]
        public void CheckSearchStartWith() 
        {
            testLogic.AddABook("a", DateTime.MinValue, 1, "a", Genre.Action, "a", 0, 0, 0);
            List<AbstractItem> letsCheck = testLogic.Search("Start with", "a", "Name");
            foreach (var item in letsCheck)
            {
                Book item1;
                if (item is Book)
                {
                    item1 = (Book)item;
                    if (item1.NameOfProduct == "a" && item1.PrintDate == DateTime.MinValue
                        && item1.Price == 0 && item1.Quantity == 0 && item1.Discount == 0 && item1.Description == "a" &&
                        item1.Edition == 1 && item1.WriterName == "a" && item1.BookGenere == Genre.Action)
                    {
                        Assert.IsTrue(true);
                        return;
                    }
                }
            }
            Assert.IsTrue(false);
        }
        [TestMethod]
        public void CheckSearchEndsWith() 
        {
            testLogic.AddABook("a", DateTime.MinValue, 1, "a", Genre.Action, "a", 0, 0, 0);
            List<AbstractItem> letsCheck = testLogic.Search("Ends with", "a", "Name");
            foreach (var item in letsCheck)
            {
                Book item1;
                if (item is Book)
                {
                    item1 = (Book)item;
                    if (item1.NameOfProduct == "a" && item1.PrintDate == DateTime.MinValue
                        && item1.Price == 0 && item1.Quantity == 0 && item1.Discount == 0 && item1.Description == "a" &&
                        item1.Edition == 1 && item1.WriterName == "a" && item1.BookGenere == Genre.Action)
                    {
                        Assert.IsTrue(true);
                        return;
                    }
                }
            }
            Assert.IsTrue(false);
        }
        [TestMethod]
        public void CheckSearchEqualNum()
        {
            testLogic.AddABook("a", DateTime.MinValue, 1, "a", Genre.Action, "a", 0, 0, 0);
            List<AbstractItem> letsCheck = testLogic.Search("=", "1", "Edition");
            foreach (var item in letsCheck)
            {
                Book item1;
                if (item is Book)
                {
                    item1 = (Book)item;
                    if (item1.NameOfProduct == "a" && item1.PrintDate == DateTime.MinValue
                        && item1.Price == 0 && item1.Quantity == 0 && item1.Discount == 0 && item1.Description == "a" &&
                        item1.Edition == 1 && item1.WriterName == "a" && item1.BookGenere == Genre.Action)
                    {
                        Assert.IsTrue(true);
                        return;
                    }
                }
            }
            Assert.IsTrue(false);
        }
        [TestMethod]
        public void CheckSortByPrice()
        {
            List<AbstractItem> letsCheck = new List<AbstractItem>();
            Book book1 = new Book("a", DateTime.MinValue, 1, "a", Genre.Action, "a", 0, 0, 0);
            Journal journal2 = new Journal("b", DateTime.MaxValue, 2, JournalGenre.Fashion, "b", "b", 1, 1, 1);
            letsCheck.Add(journal2);
            letsCheck.Add(book1);
            testLogic.SortBy(WhatToSortBy.Price, letsCheck);
            Assert.IsTrue(letsCheck[0] == book1);
        }
        [TestMethod]
        public void CheckSortByQuantity()
        {
            List<AbstractItem> letsCheck = new List<AbstractItem>();
            Book book1 = new Book("a", DateTime.MinValue, 1, "a", Genre.Action, "a", 0, 0, 0);
            Journal journal2 = new Journal("b", DateTime.MaxValue, 2, JournalGenre.Fashion, "b", "b", 1, 1, 1);
            letsCheck.Add(journal2);
            letsCheck.Add(book1);
            testLogic.SortBy(WhatToSortBy.Quantity, letsCheck);
            Assert.IsTrue(letsCheck[0] == book1);
        }
        [TestMethod]
        public void CheckSortByTypeOfGenre()
        {
            List<AbstractItem> letsCheck = new List<AbstractItem>();
            Book book1 = new Book("a", DateTime.MinValue, 1, "a", Genre.Action, "a", 0, 0, 0);
            Journal journal2 = new Journal("b", DateTime.MaxValue, 2, JournalGenre.Fashion, "b", "b", 1, 1, 1);
            letsCheck.Add(journal2);
            letsCheck.Add(book1);
            testLogic.SortBy(WhatToSortBy.TypeOfGenre, letsCheck);
            Assert.IsTrue(letsCheck[0] == book1);
        }
        [TestMethod]
        public void CheckSortByTypeOfISBN()
        {
            List<AbstractItem> letsCheck = new List<AbstractItem>();
            Book book1 = new Book("a", DateTime.MinValue, 1, "a", Genre.Action, "a", 0, 0, 0);
            book1.ISBN = Guid.Empty;
            Journal journal2 = new Journal("b", DateTime.MaxValue, 2, JournalGenre.Fashion, "b", "b", 1, 1, 1);
            letsCheck.Add(journal2);
            letsCheck.Add(book1);
            testLogic.SortBy(WhatToSortBy.ISBN, letsCheck);
            Assert.IsTrue(letsCheck[0] == book1);
        }
        [TestMethod]
        public void TestGetAllBook()
        {
            bool flag = true;
            List<AbstractItem> book;
            book = testLogic.Search("Book", "", "");
            foreach (var item in book)
            {
                if (!(item is Book) && item != null)
                    flag = false;
            }
            Assert.IsTrue(flag);
        }
        [TestMethod]
        public void TestGetAllJournal()
        {
            bool flag = true;
            List<AbstractItem> book;
            book = testLogic.Search("Journal", "", "");
            foreach (var item in book)
            {
                if (!(item is Journal) && item != null)
                    flag = false;
            }
            Assert.IsTrue(flag);
        }
        [TestMethod]
        public void TestAddABook()
        {
            int NumOfItems = testLogic.GetAllItems().Count;
            testLogic.AddABook("Harry Poter", DateTime.Now, 1.1, "Nice Book", Genre.Fantasy, "good writer", 80, 0, 50);
            Assert.IsTrue(NumOfItems + 1 == testLogic.GetAllItems().Count);
        }
        [TestMethod]
        public void TestAddAJournal()
        {
            int NumOfItems = testLogic.GetAllItems().Count;
            testLogic.AddAJournal("Happy thoughts", DateTime.Now, 1.1, JournalGenre.Health, "Happy thoughts = happy life", "Spain", 20, 5, 50);
            Assert.IsTrue(NumOfItems + 1 == testLogic.GetAllItems().Count);
        }
        [TestMethod]
        public void RemoveAnItem()
        {
            testLogic.AddABook("", DateTime.Now, 1.1, "", Genre.Action, "", 80, 0, 50);
            int NumOfItems = testLogic.GetAllItems().Count;
            testLogic.RemoveAnItem(testLogic.GetAllItems()[testLogic.GetAllItems().Count - 1]);
            Assert.IsTrue(NumOfItems - 1 == testLogic.GetAllItems().Count);
        }
    }
}
