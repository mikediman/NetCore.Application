using System.Data;

namespace NetCore.Application.Implementation
{
    public class DBProperties
    {
        public DBProperties(IDbConnection con, IDbTransaction tran)
        {
            this.con = con;
            this.tran = tran;
        }

        public IDbConnection con { get; set; }
        public IDbTransaction tran { get; set; }
    }
}
