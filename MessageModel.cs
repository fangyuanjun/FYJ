using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FYJ
{
    /// <summary>
    /// 消息实体
    /// </summary>
    public class MessageModel : ISerializable
    {
        private int _code;
        private string _message;
        public MessageModel(int code, string message)
        {
            this._code = code;
            this._message = message;
        }

        public MessageModel(SerializationInfo info, StreamingContext context)
        {
            _code = info.GetInt32("code");
            _message = info.GetString("message");
        }

        public int code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string message
        {
            get { return _message; }
            set { _message = value; }
        }

        public override string ToString()
        {
            return "{\"code\":\"" + code + "\",\"message\":\"" + message + "\"}";
        }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("code", code);
            info.AddValue("message", message);
        }
    }
}
