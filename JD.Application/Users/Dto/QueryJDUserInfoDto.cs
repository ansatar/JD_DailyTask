using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.Users
{
    public class QueryJDUserInfoDto
    {
        public baseModel Base { get; set; }
        public int definePin { get; set; }
        public int isHitArea { get; set; }
        public int isHomeWhite { get; set; }
        public int isLongPwdActive { get; set; }
        public bool isPlusVip { get; set; }
        public bool isRealNameAuth { get; set; }
        public int isShortPwdActive { get; set; }
        public string msg { get; set; }
        public int orderFlag { get; set; }
        public int retcode { get; set; }
        public int userFlag { get; set; }


        public class baseModel
        {
            public string TipUrl { get; set; }
            public int accountType { get; set; }
            public string curPin { get; set; }
            public string headImageUrl { get; set; }
            public int isJTH { get; set; }
            public int jdNum { get; set; }
            public int jvalue { get; set; }
            public string levelName { get; set; }
            public string mobile { get; set; }
            public string nickname { get; set; }
            public int userLevel { get; set; }
        }
    }
}
