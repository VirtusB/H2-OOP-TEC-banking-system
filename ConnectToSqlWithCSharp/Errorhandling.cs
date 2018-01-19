using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectToSqlWithCSharp
{
    public class TestFejl : Exception
    {
        public TestFejl()
        {
        }

        public TestFejl(string message)
            : base(message)
        {
        }

        public TestFejl(string message, Exception inner)
            : base(message, inner)
        {
        }
    }



}
