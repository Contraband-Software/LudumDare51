using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
            [TextArea]
            public string response;

            public bool AutoChoose;
        }
    }
}