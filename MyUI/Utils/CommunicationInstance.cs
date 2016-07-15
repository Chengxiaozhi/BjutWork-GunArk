using System;
using System.Collections.Generic;
using System.Text;

namespace MyUI.Utils
{
    class CommunicationInstance
    {
        private static CommunicationInstance instance;
        private Communication communication;

        private CommunicationInstance()
        {
            communication = new Communication();
        }

        public static CommunicationInstance getInstance()
        {
            if (instance == null)
            {
                instance = new CommunicationInstance();
                return instance;
            }

            return instance;
        }

        public Communication getCommunication()
        {
            return this.communication;
        }

    }
}
