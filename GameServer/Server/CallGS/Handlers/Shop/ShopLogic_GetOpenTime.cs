namespace MikuSB.GameServer.Server.CallGS.Handlers.Shop;

// Returns the open/close timestamps for each shop tab.
// Response: {shopId: {nBegin, nEnd}}
[CallGSApi("ShopLogic_GetOpenTime")]
public class ShopLogic_GetOpenTime : ICallGSHandler
{
    public async Task Handle(Connection connection, string param, ushort seqNo)
    {
        // TODO: return actual shop open times from config
        await CallGSRouter.SendScript(connection, "ShopLogic_GetOpenTime", "{}", seqNo);
    }
}
