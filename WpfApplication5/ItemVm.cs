using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData.Binding;

namespace WpfApplication5
{
    public class ItemVm : AbstractNotifyPropertyChanged, IItemVm
    {
        public string Header { get; set; }

        private bool _favorite;

        public bool Favorite
        {
            get
            {
                return _favorite;
            }
            set
            {
                _favorite = value;
                OnPropertyChanged("Favorite");
            }
            
        }
        public long Id { get; set; }

        public ItemVm(long id)
        {
            Id = id;
            Header = "Option " + Id;
        }
    }
}
