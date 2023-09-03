using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Pagination.Model
{
    internal class RecordModel
    {
        public string ItemNumber { get; set; } = "";
        public string ItemDesc { get; set; } = "";
        public DateTime Date { get; set; }
        public int Value { get; set; }

        public static List<RecordModel> GetDefaultModels()
        {
            var date = DateTime.Now.Date;
            var days = 30;
            var count = 100;
            var result = new List<RecordModel>();
            for (var i = 0;i<count;i++)
            {
                var itemNum = $"itemNumb{(i+1).ToString().PadLeft(4, '0')}";
                var itemDesc = $"itemDesc{(i+1).ToString().PadLeft(4, '0')}";
                for (var j = 0;j<days;j++)
                {
                    var d = date.AddDays(j);
                    var value = j + 1;
                    result.Add(new RecordModel
                    {
                        ItemNumber = itemNum,
                        ItemDesc = itemDesc,
                        Date = d,
                        Value = value
                    });
                }
            }
            return result;
        }

        public static DataTable GetTable()
        {
            var records = GetDefaultModels();
            var dt = new DataTable();
            var gpDate = records.GroupBy(c => c.ItemNumber).Select(c => new
            {
                ItemNum = c.Key,
                ItemDesc = c.First().ItemDesc,
                Dates = c.Select(v=>v.Date).OrderBy(v=>v).ToList(),
                Infos = c.OrderBy(c=>c.Date).ToList()
            }).ToList();
            dt.Columns.Add("料号",typeof(string));
            dt.Columns.Add("描述",typeof(string));
            var dates = gpDate[0].Dates;
            for (var i = 0; i < dates.Count; i++)
            {
                dt.Columns.Add(dates[i].ToString("yyyy-MM-dd"), typeof(string));
            }
            for (int i = 0; i < gpDate.Count; i++)
            {
                var row = dt.NewRow();
                row[0] = gpDate[i].ItemNum;
                row[1] = gpDate[i].ItemDesc;
                for (int j = 0; j < gpDate[i].Infos.Count; j++)
                {
                    row[2 + j] =  gpDate[i].Infos[j].Value;
                }

                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
