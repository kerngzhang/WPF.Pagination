using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WPF.Pagination.UserControls
{
    /// <summary>
    /// Pagination.xaml 的交互逻辑
    /// </summary>
    public partial class Pagination : UserControl
    {
        public Pagination()
        {
            InitializeComponent();
            DataContext = this;
            LoadEvent();
        }

        private void LoadEvent()
        {
            btnFirstPage.Click += (s, e) =>
            {
                if (CurrentPage != 1)
                {
                    CurrentPage = 1;
                    Go();
                }
            };
            btnPreviousPage.Click += (s, e) =>
            {
                if (CurrentPage > 1)
                {
                    CurrentPage--;
                    Go();
                }
            };
            btnNextPage.Click += (s, e) =>
            {
                if (CurrentPage < TotalPage)
                {
                    CurrentPage++;
                    Go();
                }
            };
            btnLastPage.Click += (s, e) =>
            {
                if (CurrentPage != TotalPage)
                {
                    CurrentPage = TotalPage;
                    Go();
                }

            };
            btnGo.Click += (s, e) =>
            {
                Go();
            };
        }

        private void Go()
        {
            Command?.Execute(null);
            SetEnabled();
        }

        #region 依賴屬性
        public ObservableCollection<int> PageCounts => new ObservableCollection<int> { 5, 10, 20, 30, 50, 100, 200, 500, 1000 };



        public bool FistPageEnabled
        {
            get { return (bool)GetValue(FistPageEnabledProperty); }
            set { SetValue(FistPageEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FistPageEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FistPageEnabledProperty =
            DependencyProperty.Register("FistPageEnabled", typeof(bool), typeof(Pagination), new PropertyMetadata(true));



        public bool LastPageEnabled
        {
            get { return (bool)GetValue(LastPageEnabledProperty); }
            set { SetValue(LastPageEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LastPageEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LastPageEnabledProperty =
            DependencyProperty.Register("LastPageEnabled", typeof(bool), typeof(Pagination), new PropertyMetadata(true));



        public bool PreviousPageEnabled
        {
            get { return (bool)GetValue(PreviousPageEnabledProperty); }
            set { SetValue(PreviousPageEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PreviousPageEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviousPageEnabledProperty =
            DependencyProperty.Register("PreviousPageEnabled", typeof(bool), typeof(Pagination), new PropertyMetadata(true));



        public bool NextPageEnabled
        {
            get { return (bool)GetValue(NextPageEnabledProperty); }
            set { SetValue(NextPageEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NextPageEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NextPageEnabledProperty =
            DependencyProperty.Register("NextPageEnabled", typeof(bool), typeof(Pagination), new PropertyMetadata(true));



        public int CurPageCount
        {
            get { return (int)GetValue(CurPageCountProperty); }
            set { SetValue(CurPageCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurPageCountInex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurPageCountProperty =
            DependencyProperty.Register("CurPageCount", typeof(int), typeof(Pagination), new PropertyMetadata(4));



        public int CurrentPage
        {
            get { return (int)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentPage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register("CurrentPage", typeof(int), typeof(Pagination), new PropertyMetadata(1));



        public ObservableCollection<int> Pages
        {
            get { return (ObservableCollection<int>)GetValue(PagesProperty); }
            set { SetValue(PagesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Pages.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PagesProperty =
            DependencyProperty.Register("Pages", typeof(ObservableCollection<int>), typeof(Pagination), new PropertyMetadata(new ObservableCollection<int>()));



        public int TotalPage
        {
            get { return (int)GetValue(TotalPageProperty); }
            set { SetValue(TotalPageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TotalPage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalPageProperty =
            DependencyProperty.Register("TotalPage", typeof(int), typeof(Pagination), new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(TotalPageCallback)));

        private static void TotalPageCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;
            var pagination = (Pagination)d;
            var res = int.TryParse(e.NewValue.ToString(), out int value);
            if (res && value > 0)
            {
                pagination.TotalPage = value;
                pagination.Pages.Clear();
                for (int i = 1; i <= value; i++)
                {
                    pagination.Pages.Add(i);
                }
            }
            else
            {
                pagination.TotalPage = 1;
                pagination.Pages.Clear();
                pagination.Pages.Add(1);
                pagination.CurrentPage = 1;
            }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(Pagination), new PropertyMetadata(null));


        #endregion

        private void SetEnabled()
        {
            if (TotalPage < 2)
            {
                FistPageEnabled = false;
                LastPageEnabled = false;
                NextPageEnabled = false;
                PreviousPageEnabled = false;
            }
            else
            {
                if (CurrentPage == 1)
                {
                    FistPageEnabled = false;
                    PreviousPageEnabled = false;
                    NextPageEnabled = true;
                    LastPageEnabled = true;
                }
                else if (CurrentPage == TotalPage)
                {
                    FistPageEnabled = true;
                    PreviousPageEnabled = true;
                    NextPageEnabled = false;
                    LastPageEnabled = false;
                }
                else
                {
                    FistPageEnabled = true;
                    PreviousPageEnabled = true;
                    NextPageEnabled = true;
                    LastPageEnabled = true;
                }
            }
        }

    }
}
