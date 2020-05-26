using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnotheekWeb.Interfaces
{
    public interface IDatabaseHandler
    {
        void CUD(string query);
        bool TestConnection();
        void OpenConnectionToDB();
        void CloseConnectionToDB();
        SqlConnection GetCon();
        void ClearTable();
    }
}
