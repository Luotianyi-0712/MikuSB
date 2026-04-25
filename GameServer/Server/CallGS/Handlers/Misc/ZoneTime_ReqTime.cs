using MikuSB.Util.Extensions;

namespace MikuSB.GameServer.Server.CallGS.Handlers.Misc;

// Client requests server time to calculate timezone offset.
// nTime1/nTime2 are DST transition reference timestamps; returning the same value means no offset.
[CallGSApi("ZoneTime_ReqTime")]
public class ZoneTime_ReqTime : ICallGSHandler
{
    public async Task Handle(Connection connection, string param, ushort seqNo)
    {
        var now = Extensions.GetUnixSec();
        var arg = $"{{\"nTime1\":{now},\"nTime2\":{now}}}";
        await CallGSRouter.SendScript(connection, "ZoneTime_ChangeTime", arg);
    }
}
