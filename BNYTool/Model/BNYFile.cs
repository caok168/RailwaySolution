using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNYTool
{
    public class BNYFile
    {
        public static int oneByteSize = 4;

        public static int channelNum = 18;


        public static int GetChannelSize()
        {
            return oneByteSize * channelNum;
        }

    }
}
