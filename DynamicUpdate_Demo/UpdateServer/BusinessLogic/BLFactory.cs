using UpdateServer.BusinessLogic.Implements;
using UpdateServer.DataAccess;

namespace UpdateServer.BusinessLogic
{
    internal class BLFactory
    {
        static BLFactory _Instance;

        public static BLFactory Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new BLFactory();
                return _Instance;
            }
        }
        public IClientsBL GetClientsBL()
        {
            return new ClientsBL();
        }
    }
}