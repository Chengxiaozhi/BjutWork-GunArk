using System;
using System.Collections.Generic;
using System.Text;

namespace MyUI.Utils
{
    public class SearchDoorStatus
    {
        public static bool searchGunStatus()
        {
            Communication comm = CommunicationInstance.getInstance().getCommunication();
            char[] cc = null;
            char[] data1 = { '\x02', '0', 'A', '0', '0', '2', '0', 'D', 'A', 'T', 'A', '6', '3', '\x03', '\x0D' };
            comm.send_message(Port.Get_ComGun(), data1);
            cc = comm.getAlarmCC();
            //System.Windows.Forms.MessageBox.Show(cc[7].ToString());
            if (cc[7] == '0')
                return false;
            else
                return true;
            //return true;
        }
        public static bool searchBulletStatus()
        {
            Communication comm = CommunicationInstance.getInstance().getCommunication();
            char[] cc = null;
            char[] data1 = { '\x02', '0', 'A', '0', '0', '2', '0', 'D', 'A', 'T', 'A', '6', '3', '\x03', '\x0D' };
            comm.send_message(Port.Get_ComBullet(), data1);
            cc = comm.getAlarmCC();
            //System.Windows.Forms.MessageBox.Show(cc[7].ToString());
            if (cc[7] == '0')
                return false;
            else
                return true;
            //return true;
        }
    }
}
