using System;
using System.Collections;
using System.Collections.Generic;

namespace GraphSystem
{
    public class GraphConnections
    {
        public class ResponseConnection { }

        [Serializable]
        public class ResponseConnectionLink : ResponseConnection
        {

        }
        [Serializable]
        public class ResponseConnectionData : ResponseConnection
        {
            public string response;
        }
    }
}