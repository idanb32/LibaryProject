using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFiles;
using VisualRep.MyControls;

namespace VisualRep
{
    class DataShowcase : IEnumerable // its a class i made that implament ienumerable so i can use it as source for my list view
    {
        public ViewAbstractItem[] Data;  //list of my spacific user control so i will have an orginized list view 
        public DataShowcase(List<AbstractItem> data)
        {
            UpdateData(data);
        }
        public void UpdateData(List<AbstractItem> data)
        {
            Data = new ViewAbstractItem[data.Count];
            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = new ViewAbstractItem(data[i]);
            }
        } // a func to update what i show beacuse i change it every time we change our items, sort them, or search for them.
        public IEnumerator GetEnumerator()
        {
            return Data.GetEnumerator();
        }
    }
}
