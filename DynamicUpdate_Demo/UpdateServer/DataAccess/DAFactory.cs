using UpdateServer.DataAccess.Interfaces;

namespace UpdateServer.DataAccess
{
    public class DAFactory
    {
        static DAFactory _Instance;
        public static DAFactory Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new DAFactory();
                return _Instance;
            }
        }

        public IClientsDA GetClientsDA()
        {
            return new MockDB.ClientsDA();
        }


    }
}
