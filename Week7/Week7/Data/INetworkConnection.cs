using System;
using System.Collections.Generic;
using System.Text;

namespace Week7.Data
{
    public interface INetworkConnection
    {
        bool isConnected { get; }
        void checkNetworkConnection();
    }
}
