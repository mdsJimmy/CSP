using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Bastogne.Objs
{
    public class ReturnBaseObj
    {
        public string status;
        public string code;
        public string msg;
        public string errtrace;

        public void setValues(string _status, string _code, string _msg)
        {
            status = _status;
            code = _code;
            msg = _msg;
        }
    }

    public class OptionItemObj
    {
        public OptionItemObj() { }
        public OptionItemObj(string _text, string _value)
        {
            value = _value;
            text = _text;
        }
        public string value = "";
        public string text = "";
    }
    public class ReturnOptionItemObj : ReturnBaseObj
    {
        public List<OptionItemObj> data = new List<OptionItemObj>();
    }
}