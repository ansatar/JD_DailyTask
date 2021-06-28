using JD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.DailyTasks
{
    public class DoGameBySpeedUpDto : IDoSignBaseDto
    {
        public int code { get; set; }
        public string message { get; set; }
        public bool success { get; set; }
        public dataModel data { get; set; }
        public class dataModel
        {
            public int beans_num { get; set; }
            //public string destination { get; set; }
            //public int distance { get; set; }
            //public int done_distance { get; set; }
            //public DateTime end_time { get; set; }
            //public int first_task { get; set; }
            //public string icon { get; set; }
            //public int show_layer { get; set; }
            public int source_id { get; set; }
            //public DateTime start_time { get; set; }
            public int task_status { get; set; }
            //public string title { get; set; }
            //public string uuid { get; set; }
        }
        //public infoModel info { get; set; }

        public CusResponseDto GetCusResponse(string actionName = "")
        {
            JDDailyTaskEnum jdDailyTaskEnum = JDDailyTaskEnum.DoGameBySpeedUp;
            JDAwardTypeEnum jdAwardTypeEnum = JDAwardTypeEnum.京豆;
            actionName = string.IsNullOrEmpty(actionName) ? jdDailyTaskEnum.GetDesc() : actionName;

            try
            {
                //失败
                if (!success) return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, message);
                //成功
                if (data != null && data.task_status == 2)
                    return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, data.beans_num, jdAwardTypeEnum);
                else
                    return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, 0, jdAwardTypeEnum);

            }
            catch (Exception ex)
            {
                return CusResponseDto.IsFail(jdDailyTaskEnum, actionName, ex.Message);
            }

            //return CusResponseDto.IsSuccess(jdDailyTaskEnum, actionName, null, jdAwardTypeEnum);
        }
    }

    public class DoGameBySpeedUpSpaceEventDto
    {
        public int code { get; set; }
        public string message { get; set; }
        public bool success { get; set; }
        public List<dataModel> data { get; set; }
        public class dataModel
        {
            public int id { get; set; }
            public string intro { get; set; }
            public int status { get; set; }
            public List<optionsModel> options { get; set; }
            public class optionsModel
            {
                public string result { get; set; }
                public string title { get; set; }
                public int type { get; set; }
                public string value { get; set; }
                public int energy { get; set; }
            }
        }
    }

    public class DoGameBySpeedUpPropDto
    {
        public int code { get; set; }
        public string message { get; set; }
        public bool success { get; set; }
        public List<dataModel> data { get; set; }
        public class dataModel
        {
            public DateTime get_time { get; set; }
            public string id { get; set; }
            public int type { get; set; }
            public int value { get; set; }
        }
    }
}
