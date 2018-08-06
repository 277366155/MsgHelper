using System;
using System.Collections.Generic;
using System.Text;

namespace MH.Models
{
    public class ResultBase
    {
        public bool Success { get; set; }

        protected string _msg;
        public string Msg
        {
            get
            {
                return _msg;
            }
            set
            {
                _msg = value;
            }
        }
    }

    public class Result : ResultBase
    {
        public Result() { }
        public Result(string msg)
        {
            _msg = msg;
        }
        public Object Data { get; set; }

    }
    public class Result<T> : ResultBase
    {
        public Result() { }
        public Result(string msg)
        {
            _msg = msg;
        }
        public T Data { get; set; }
    }

    public class Error : Result
    {
        public Error():base() { }
        public Error(string msg):base(msg)
        {
        }
        public new bool Success = false;
    }

    public class Ok : Result
    {
        public Ok() { }
        public Ok(string msg) : base(msg)
        {
        }
        public new bool Success = true;
    }

    public class Error<T> : Result<T>
    {
        public Error():base() { }
        public Error(string msg) : base(msg)
        {
        }
        public new bool Success = false;
    }

    public class Ok<T> : Result<T>
    {
        public Ok():base() { }
        public Ok(string msg) : base(msg)
        {
        }
        public new bool Success = true;
    }
}
