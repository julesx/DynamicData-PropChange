using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DynamicData;
using DynamicData.Binding;
using DynamicData.Controllers;

namespace WpfApplication5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IObservableCollection<IItemVm> AllItems { get; set; }
        public IObservableCollection<IItemVm> FavoriteItems { get; set; }

        public FilterController<IItemVm> AllFilterController;
        public FilterController<IItemVm> FavoritesFilterController;

        public SourceCache<IItemVm, int> ItemCache { get; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            AllItems = new ObservableCollectionExtended<IItemVm>();
            FavoriteItems = new ObservableCollectionExtended<IItemVm>();

            ItemCache = new SourceCache<IItemVm, int>(itemVm => (int)itemVm.Id);
            ItemCache.AddOrUpdate(new ItemVm(1));
            ItemCache.AddOrUpdate(new ItemVm(2));
            ItemCache.AddOrUpdate(new ItemVm(3));
            ItemCache.AddOrUpdate(new ItemVm(4));
            ItemCache.AddOrUpdate(new ItemVm(5));

            FavoritesFilterController = new FilterController<IItemVm>(x => x.Favorite);
            AllFilterController = new FilterController<IItemVm>(x => !string.IsNullOrEmpty(x.Header));

            var allFilterCache = ItemCache.Connect()
                .Filter(AllFilterController)
                .AsObservableCache();

            allFilterCache
                        .Connect()
                        .Bind(AllItems)
                        .Subscribe();

            var favoritesFilterCache = ItemCache.Connect()
                .Filter(FavoritesFilterController)
                .AsObservableCache();

            favoritesFilterCache
                .Connect()
                .Bind(FavoriteItems)
                .Subscribe();
        }
    }
}
