using CourseApp.DataAccess.Models;
using System.Data.Common;

namespace CourseApp.DataAccess.Oracle
{
    public static class OracleDataMapper
    {
        public static Course FromReader(DbDataReader dataReader)
        {
            double coursePrice = dataReader.IsDBNull(2)
                    ? default(double)
                    : dataReader.GetDouble(2);

            return new Course(
                dataReader.GetInt32(0),
                dataReader.GetString(1),
                coursePrice);
        }
    }
}
