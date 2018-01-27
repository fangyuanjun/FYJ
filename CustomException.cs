using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FYJ
{
    public class CustomException : Exception, ISerializable
    {
        private int _code = -9999;
        private string _message;
        public CustomException()
            : base()
        {

        }

        public CustomException(string message)
            : base(message)
        {
            _message = message;
        }

        public CustomException(int code, string message)
            : base(message)
        {
            _code = code;
            _message = message;
        }

        public CustomException(SerializationInfo info, StreamingContext context)
        {
            _code = info.GetInt32("code");
            _message = info.GetString("message");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("code", Code);
            info.AddValue("message", Message);
        }

        public int Code
        {
            get { return _code; }
        }

        public override string Message
        {
            get { return _message; }
        }

        public override string ToString()
        {
            string s = (Message == null ? null : Message.Replace("\"", "\\\""));
            return "{\"code\":\"" + Code + "\",\"message\":\"" + Message + "\"}";
        }
    }
}
