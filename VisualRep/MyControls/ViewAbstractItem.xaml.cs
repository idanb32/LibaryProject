using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using CommonFiles;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace VisualRep.MyControls
{
    public sealed partial class ViewAbstractItem : UserControl
    {
        public AbstractItem Data;
        public ViewAbstractItem()
        {
            this.InitializeComponent();
        }
        public ViewAbstractItem(AbstractItem data) : this()
        {
            if (data is Book)
            {
                Book item = (Book)data;
                type.Text = "Book";
                printDate.Text = item.PrintDate.ToString();
                name.Text = item.NameOfProduct;
                countryWritter.Text = item.WriterName;
                Description.Text = item.Description;
                Genre.Text = item.BookGenere.ToString();
                ISBN.Text = item.ISBN.ToString();
                Edition.Text = item.Edition.ToString();
                Quanity.Text = item.Quantity.ToString();
                Price.Text = $" {item.Price.ToString()}$";
                Discount.Text = $"{item.Discount.ToString()}%";
                Data = (Book)data;
            }
            else if (data is Journal)
            {
                Journal item = (Journal)data;
                type.Text = "Journal";
                printDate.Text = item.PrintDate.ToString();
                name.Text = item.NameOfProduct;
                countryWritter.Text = item.Country;
                Description.Text = item.Description;
                Genre.Text = item.Genre.ToString();
                ISBN.Text = item.ISBN.ToString();
                Edition.Text = item.Edition.ToString();
                Quanity.Text = item.Quantity.ToString();
                Price.Text = $" {item.Price.ToString()}$";
                Discount.Text = $"{item.Discount.ToString()}%";
                Data = (Journal)data;
            }
        }

    }
}
