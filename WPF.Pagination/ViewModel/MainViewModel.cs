using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF.Pagination.Model;

namespace WPF.Pagination.ViewModel
{
    internal class MainViewModel:KNotificationObject
    {

        public List<string> ModelDtos { get; init; }
        private ObservableCollection<string> _Models;

        public ObservableCollection<string> Models
        {
            get { return _Models; }
            set { _Models = value; RaisePropertyChanged(); }
        }

        private DataTable _modelTable;

        public DataTable ModelTable
        {
            get { return _modelTable; }
            set { _modelTable = value; RaisePropertyChanged(); }
        }


        private int _curPage;

        public int CurPage
        {
            get { return _curPage; }
            set { _curPage = value; RaisePropertyChanged(); }
        }

        private int _totalPage;

        public int TotalPage
        {
            get { return _totalPage; }
            set { _totalPage = value; RaisePropertyChanged();  }
        }

        private int _pageCount;

        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; RaisePropertyChanged();  }
        }

        public KCommand SearchCommand { get; set; }


        public MainViewModel()
        {
            ModelDtos = new List<string>();
            Models = new ObservableCollection<string>();
            for (int i = 0; i < 1000; i++)
            {
                ModelDtos.Add(i.ToString().PadLeft(3, '0'));
            }
            PageCount = 50;
            TotalPage = 20;
            CurPage = 1;
            GetData();
            SearchCommand = new KCommand();
            SearchCommand.ExecuteAction = new Action<object>(Search);
            ModelTable = RecordModel.GetTable();
        }

        private void Search(object obj)
        {
            GetData();
        }

        private void GetData()
        {
            Models.Clear();
            var infos = ModelDtos.Skip((CurPage-1)*PageCount).Take(PageCount).ToList();
            if(infos!=null)
            {
                foreach (var item in infos)
                {
                    Models.Add(item);
                }
            }
        }
    }
}
