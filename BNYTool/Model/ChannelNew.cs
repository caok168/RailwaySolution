using CitFileProcess;

namespace BNYTool.Model
{
    public class ChannelNew
    {
        public DataChannelInfo dataChannelInfo { get; set; }

        public int BNYChannelId { get; set; }

        public string BNYChannelName { get; set; }

        public ChannelNew()
        {
            BNYChannelId = -1;
        }
    }
}
