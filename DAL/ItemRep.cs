using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using CommonFiles;
using Windows.Storage;

namespace DAL
{
    public class ItemRep
    {
        string dir;
        string stockItemFilePath;
        public List<AbstractItem> ItemList;

        public ItemRep()
        {
            ItemList = new List<AbstractItem>();
            dir = ApplicationData.Current.LocalFolder.Path;
            stockItemFilePath = Path.Combine(dir, "InStockData.bin");
            Load();
        }
        public void AddItem(AbstractItem item)
        {
            ItemList.Add(item);
            Save();
        } //add an item to our timly list and then save the change
        public void RemoveItem(AbstractItem RemoveItem)
        {
            ItemList.Remove(RemoveItem);
            Save();
        }//Removes an item to our timly list and then save the change
        public void Save()
        {
            try
            {
                using (Stream stream = File.Open(stockItemFilePath, FileMode.OpenOrCreate))
                {
                    var binFormatter = new BinaryFormatter();
                    binFormatter.Serialize(stream, ItemList);
                }
            }
            catch (Exception)
            {
                throw new CorruptFileException($"You have a currpot file, please go to this address, {stockItemFilePath} \n and delete the file named: InStockData.bin ");
            }
        } // Save our timly list into our bin file via seralization
        public void Load()
        {
            try
            {
                using (Stream stream = File.Open(stockItemFilePath, FileMode.OpenOrCreate))
                {
                    var binFormatter = new BinaryFormatter();
                    if (stream.Length > 0)
                        ItemList = (List<AbstractItem>)binFormatter.Deserialize(stream);
                }
            }
            catch (Exception)
            {
                throw new CorruptFileException($"You have a currpot file, please go to this address, {stockItemFilePath} \n and delete the file named: InStockData.bin ");
            }

        }// Load our timly list from our bin file via deseralization
    }
}
