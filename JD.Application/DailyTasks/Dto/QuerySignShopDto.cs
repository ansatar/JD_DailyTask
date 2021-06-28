using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public class QuerySignShopDto
    {
        public int code { get; set; }
        public string msg { get; set; }

        public List<floatLayerListModel> floatLayerList { get; set; }

        public class floatLayerListModel
        {
            public string Params { get; set; }
        }

        public List<floorListModel> floorList { get; set; }
        public class floorListModel
        {
            public signInfosModel signInfos { get; set; }
            public class signInfosModel
            {
                public string Params { get; set; }
                /// <summary>
                /// 1:重复签到
                /// </summary>
                public int signStat { get; set; }
            }

            public boardParamsModel boardParams { get; set; }
            public class boardParamsModel
            {
                public string turnTableId { get; set; }
            }
        }

        public string paginationFlrs { get; set; }
    }
}