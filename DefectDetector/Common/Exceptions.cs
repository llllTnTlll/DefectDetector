using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectDetector.Common
{
    public class SocketConnectException: Exception
    {
        public SocketConnectException(string message): base(message) { }
    }
}
