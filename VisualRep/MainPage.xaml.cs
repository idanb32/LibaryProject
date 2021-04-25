using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using BLL;
using CommonFiles;
using DAL;
using VisualRep.MyControls;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace VisualRep
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region
        Logic dataBase;
        DataShowcase myData;
        List<AbstractItem> searchedList;
        AbstractItem changeMe;
        bool haveSpacificSearch = false;
        #endregion
        public MainPage()
        {
            this.InitializeComponent();
            try
            {
                dataBase = new Logic();
                myData = new DataShowcase(dataBase.GetAllItems());
                SetItems();
            }
            catch (CorruptFileException ex)
            {
                DisplayMassage.Text = ex.Message;
            }
        }
        private void SetItems()
        {
            var itemList = dataBase.GetAllItems();
            BookLst.ItemsSource = null;
            BookLst.ItemsSource = itemList;
            UpdateVisual();
        }
        private void UpdateVisual()
        {
            if (haveSpacificSearch)
            {
                myData.UpdateData(searchedList);
            }
            else
            {
                myData.UpdateData(dataBase.GetAllItems());
            }
            foreach (var item in myData.Data)
            {
                item.RightTapped += ItemIsRightClicked;
            }
            SearchedTable.ItemsSource = null;
            SearchedTable.ItemsSource = myData;
        } 
        private void ItemIsRightClicked(object sender, RightTappedRoutedEventArgs e)
        {
            if (sender is ViewAbstractItem)
            {
                ViewAbstractItem toMakeWhatImChanging = (ViewAbstractItem)sender;
                changeMe = toMakeWhatImChanging.Data;
            }

            MenuFlyout rightClicked = new MenuFlyout();
            MenuFlyoutItem Delete = new MenuFlyoutItem();
            MenuFlyoutItem Update = new MenuFlyoutItem();
            Delete.Text = "Delete";
            Update.Text = "Update";
            Delete.Click += Delete_Click;
            Update.Click += Update_Click;
            rightClicked.Items.Add(Delete);
            rightClicked.Items.Add(Update);
            rightClicked.ShowAt(sender as UIElement, e.GetPosition(sender as UIElement));
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            DisplayMassage.Text = $"You are now changing an item, please notice that in your left you have all of the data of the spacific item. \n  Until you change it, you canot add another item.";
            add.Click -= Add_Click;
            add.Click += ChangeClick;
            if (changeMe is Book)
            {
                Book item = (Book)changeMe;
                BookOrJornaul.SelectedItem = "Book";
                Quanty.Text = item.Quantity.ToString();
                countryOrWritter.Text = item.WriterName;
                Editions.Text = item.Edition.ToString();
                Genere.SelectedItem = item.BookGenere;
                NameOfItem.Text = item.NameOfProduct;
                Prices.Text = item.Price.ToString();
                Discounts.Text = item.Discount.ToString();
                Desc.Text = item.Description;
                PrintDate.Date = item.PrintDate;
                BookOrJornaul.SelectedIndex = 0;
            }
            else if (changeMe is Journal)
            {
                Journal item = (Journal)changeMe;
                Quanty.Text = item.Quantity.ToString();
                countryOrWritter.Text = item.Country;
                Editions.Text = item.Edition.ToString();
                Genere.SelectedItem = item.Genre;
                NameOfItem.Text = item.NameOfProduct;
                Prices.Text = item.Price.ToString();
                Discounts.Text = item.Discount.ToString();
                Desc.Text = item.Description;
                PrintDate.Date = item.PrintDate;
                BookOrJornaul.SelectedIndex = 1;
            }
            AddingProduct.Visibility = Visibility.Visible;
            VisualGrid.Visibility = Visibility.Collapsed;
            add.Content = "Change";
        }
        private void SetAllToEmpty()
        {
            Quanty.Text = "";
            countryOrWritter.Text = "";
            Editions.Text = "";
            Genere.SelectedItem = default;
            NameOfItem.Text = "";
            Prices.Text = "";
            Discounts.Text = "";
            Desc.Text = "";
            PrintDate.Date = default;
            BookOrJornaul.SelectedIndex = default;
        }
        private void ChangeClick(object sender, RoutedEventArgs e)
        {
            if (changeMe is Book)
            {
                if (AllIsWell())
                {
                    try
                    {
                        dataBase.RemoveAnItem(changeMe);
                        dataBase.AddABook(NameOfItem.Text, PrintDate.Date.DateTime, double.Parse(Editions.Text), Desc.Text,
                           (Genre)Genere.SelectedItem, countryOrWritter.Text, double.Parse(Prices.Text), changeMe.ISBN, double.Parse(Discounts.Text), int.Parse(Quanty.Text));
                    }
                    catch (CorruptFileException ex)
                    {
                        DisplayMassage.Text = ex.Message;
                    }
                    SetItems();
                    SetAllToEmpty();
                    add.Click += Add_Click;
                    add.Click -= ChangeClick;
                    add.Content = "Add a book";
                }
            }
            else if (changeMe is Journal)
            {
                if (AllIsWell())
                {
                    try
                    {
                        dataBase.RemoveAnItem(changeMe);
                        dataBase.AddAJournal(NameOfItem.Text, PrintDate.Date.DateTime, double.Parse(Editions.Text), (JournalGenre)Genere.SelectedItem,
                           Desc.Text, countryOrWritter.Text, double.Parse(Prices.Text), changeMe.ISBN, double.Parse(Discounts.Text), int.Parse(Quanty.Text));
                    }
                    catch (CorruptFileException ex)
                    {
                        DisplayMassage.Text = ex.Message;
                    }
                    SetItems();
                    SetAllToEmpty();
                    add.Click += Add_Click;
                    add.Click -= ChangeClick;
                    add.Content = "Add a book";
                }
            }

        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dataBase.RemoveAnItem(changeMe);
            }
            catch (CorruptFileException ex)
            {
                DisplayMassage.Text = ex.Message;
            }
            myData.UpdateData(dataBase.GetAllItems());
            SearchedTable.ItemsSource = null;
            SearchedTable.ItemsSource = myData;
        }
        private void BookOrJornaul_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BookOrJornaul.SelectedIndex == 0)
            {
                var enumItemSource = Enum.GetValues(typeof(Genre)).Cast<Genre>();
                Genere.ItemsSource = enumItemSource.ToList();
                Genere.Header = "Book Genre";
                countryOrWritter.Header = "Writter name";
                add.Content = "Add a book";
            }
            else if (BookOrJornaul.SelectedIndex == 1)
            {
                var enumItemSource = Enum.GetValues(typeof(JournalGenre)).Cast<JournalGenre>();
                Genere.ItemsSource = enumItemSource.ToList();
                Genere.Header = "Journal Genre";
                countryOrWritter.Header = "Country of the Journal";
                add.Content = "Add a journal";
            }
        }
        private void NumBoxBeforeChange(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => (!char.IsDigit(c)));
        }
        private bool AllIsWell()
        {
            StringBuilder ErrorList = new StringBuilder();
            if (BookOrJornaul.SelectedItem != null)
            {
                if (PrintDate.Date != null)
                {
                    if (Quanty.Text != "" && Quanty.Text != null)
                    {
                        if (countryOrWritter.Text != "" && countryOrWritter.Text != null)
                        {
                            if (Editions.Text != "" && Editions.Text != null)
                            {
                                if (double.TryParse(Editions.Text, out double worked))
                                {
                                    if (Genere.SelectedItem != null)
                                    {
                                        if (NameOfItem.Text != null && NameOfItem.Text != "")
                                        {
                                            if (Prices.Text != null && Prices.Text != "")
                                            {
                                                if (double.Parse(Prices.Text) >= 0)
                                                {
                                                    if (Discounts.Text != null && Discounts.Text != "")
                                                    {
                                                        if (double.Parse(Discounts.Text) >= 0 && double.Parse(Discounts.Text) < 100)
                                                        {
                                                            return true;
                                                        }
                                                        else
                                                        {
                                                            ErrorList.Append($"Please make sure the discount is between 0-99. \n");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ErrorList.Append($"Please insert the discount of the item(between 0-100). \n");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                ErrorList.Append($"Please insert the price of the item, that is above or equal to 0. \n");
                                            }
                                        }
                                        else
                                        {
                                            ErrorList.Append($"Please insert a name of the item. \n");
                                        }
                                    }
                                    else
                                    {
                                        ErrorList.Append($"Please select a {Genere.Header}. \n");
                                    }
                                }
                                else
                                {
                                    ErrorList.Append($"Please Only insert number in the Edition box. \n");
                                }
                            }
                            else
                            {
                                ErrorList.Append($"Please insert an Edition for the item  \n");
                            }
                        }
                        else
                        {
                            ErrorList.Append($"Please insert the {countryOrWritter.Header} for the item  \n");
                        }
                    }
                    else
                    {
                        ErrorList.Append($"Please insert the quanty of the item  \n");
                    }
                }
                else
                {
                    ErrorList.Append($"Please insert the print date  \n");
                }
            }
            else
            {
                ErrorList.Append($"Please choose type of item  \n");
            }
            DisplayMassage.Text = ErrorList.ToString();
            return false;

        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (AllIsWell())
                if (BookOrJornaul.SelectedIndex == 0)
                {
                    try
                    {
                        dataBase.AddABook(NameOfItem.Text, PrintDate.Date.DateTime, double.Parse(Editions.Text), Desc.Text,
                            (Genre)Genere.SelectedItem, countryOrWritter.Text, double.Parse(Prices.Text), double.Parse(Discounts.Text), int.Parse(Quanty.Text));
                    }
                    catch (CorruptFileException ex)
                    {
                        DisplayMassage.Text = ex.Message;
                    }
                    SetItems();
                    SetAllToEmpty();
                }
                else
                {
                    try
                    {
                        dataBase.AddAJournal(NameOfItem.Text, PrintDate.Date.DateTime, double.Parse(Editions.Text), (JournalGenre)Genere.SelectedItem,
                           Desc.Text, countryOrWritter.Text, double.Parse(Prices.Text), double.Parse(Discounts.Text), int.Parse(Quanty.Text));
                    }
                    catch (CorruptFileException ex)
                    {
                        DisplayMassage.Text = ex.Message;
                    }
                    SetItems();
                    SetAllToEmpty();
                }
        }
        private void GoToVisualGrid_Click(object sender, RoutedEventArgs e)
        {
            ExplainProject.Visibility = Visibility.Collapsed;
            AddingProduct.Visibility = Visibility.Collapsed;
            VisualGrid.Visibility = Visibility.Visible;
            haveSpacificSearch = false;
            myData.UpdateData(dataBase.GetAllItems());
            UpdateVisual();
        }
        private void SortByThis(object sender, PointerRoutedEventArgs e)
        {
            var PropToSortBy = sender as TextBlock;
            if (PropToSortBy == null)
                return;
            string whatToSortBy = PropToSortBy.Name;
            WhatToSortBy EnumRep = (WhatToSortBy)Enum.Parse(typeof(WhatToSortBy), whatToSortBy);
            if (haveSpacificSearch)
            {
                try
                {
                    dataBase.SortBy(EnumRep, searchedList);
                }
                catch (NullReferenceException)
                {
                    SearchBar.Text = "Not enough items to sort.";
                }
            }
            else
                try
                {
                    dataBase.SortBy(EnumRep);
                }
                catch (NullReferenceException)
                {
                    SearchBar.Text = "Not enough items to sort.";
                }
            SetItems();
        }
        private void ReturnToAdd_Click(object sender, RoutedEventArgs e)
        {
            ExplainProject.Visibility = Visibility.Collapsed;
            AddingProduct.Visibility = Visibility.Visible;
            VisualGrid.Visibility = Visibility.Collapsed;
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (HowToSearch.SelectedItem != null)
            {
                if (HowToSearch.SelectedItem == "Book" || HowToSearch.SelectedItem == "Journal")
                {
                    haveSpacificSearch = true;
                    searchedList = dataBase.Search(HowToSearch.SelectedItem.ToString(), "", "");
                    myData.UpdateData(searchedList);
                }
                else if (HowToSearch.SelectedItem == ">" || HowToSearch.SelectedItem == "<" || HowToSearch.SelectedItem == "=")
                {
                    if (double.TryParse(SearchBar.Text, out double MakeSureItsFine))
                    {
                        haveSpacificSearch = true;
                        searchedList = dataBase.Search(HowToSearch.SelectedItem.ToString(), SearchBar.Text, PropToSearch.SelectedItem.ToString());
                        myData.UpdateData(searchedList);
                    }
                    else
                        SearchBar.Text = "Only number is acceptable in this type of search";
                }
                else if (HowToSearch.SelectedItem == "Start with" || HowToSearch.SelectedItem == "Have" || HowToSearch.SelectedItem == "Ends with")
                {
                    haveSpacificSearch = true;
                    searchedList = dataBase.Search(HowToSearch.SelectedItem.ToString(), SearchBar.Text, PropToSearch.SelectedItem.ToString());
                    myData.UpdateData(searchedList);
                }
                else
                {
                    haveSpacificSearch = true;
                    searchedList = dataBase.Search(HowToSearch.SelectedItem.ToString(), "", "");
                    myData.UpdateData(searchedList);
                }
                UpdateVisual();
            }
        }
        private void PropToSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PropToSearch.SelectedIndex == 0)
            {
                HowToSearch.ItemsSource = new string[2] { "Book", "Journal" };
            }
            else if (PropToSearch.SelectedIndex > 6)
            {
                HowToSearch.ItemsSource = new string[3] { ">", "=", "<" };
            }
            else if (PropToSearch.SelectedIndex == 5)
            {
                var enumItemSource = Enum.GetNames(typeof(JournalGenre)).ToList();
                enumItemSource.AddRange(Enum.GetNames(typeof(Genre)).ToList());
                HowToSearch.ItemsSource = enumItemSource;
            }
            else
            {
                HowToSearch.ItemsSource = new string[3] { "Start with", "Have", "Ends with" };
            }
        }
    }
}
