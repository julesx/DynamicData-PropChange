using System;
using System.Collections.Generic;
using System.Windows;
using DynamicData;
using DynamicData.Binding;
using DynamicData.Controllers;

namespace WpfApplication5
{
    public partial class MainWindow
    {
        public IObservableCollection<IItemVm> AllItems { get; set; }
        public FilterController<IItemVm> AllFilterController;
        public SortController<IItemVm> SortController;
        public MySorter MySorter;
        public SourceCache<IItemVm, int> ItemCache { get; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            AllItems = new ObservableCollectionExtended<IItemVm>();

            ItemCache = new SourceCache<IItemVm, int>(itemVm => (int)itemVm.Id);

            for (var i = 0; i < 345; i++)
                ItemCache.AddOrUpdate(new ItemVm(i));

            MySorter = new MySorter();
            AllFilterController = new FilterController<IItemVm>(x => !string.IsNullOrEmpty(x.Header));
            SortController = new SortController<IItemVm>(MySorter);

            ItemCache
                .Connect()
                .Filter(AllFilterController)
                .Sort(SortController)
                .Top(2000)
                .Bind(AllItems)
                .Subscribe();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MySorter.SortOption = "Header";
            MySorter.SortDirection = MySorter.SortDirection == SortDirection.Ascending
                ? SortDirection.Descending
                : SortDirection.Ascending;
            SortController.Resort();
        }

        private void IdClicked(object sender, RoutedEventArgs e)
        {
            MySorter.SortOption = "Id";
            MySorter.SortDirection = MySorter.SortDirection == SortDirection.Ascending
                ? SortDirection.Descending
                : SortDirection.Ascending;
            SortController.Resort();
        }
    }

    public class MySorter : IComparer<IItemVm>
    {
        public string SortOption;
        public SortDirection SortDirection;

        public int Compare(IItemVm x, IItemVm y)
        {
            if (SortOption == "Id")
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    return x.Id.CompareTo(y.Id);
                }
                return x.Id.CompareTo(y.Id) * -1;
            }
            if (SortDirection == SortDirection.Ascending)
            {
                return string.Compare(x.Header, y.Header, StringComparison.Ordinal);
            }
            return string.Compare(x.Header, y.Header, StringComparison.Ordinal) * -1;
        }
    }
}
