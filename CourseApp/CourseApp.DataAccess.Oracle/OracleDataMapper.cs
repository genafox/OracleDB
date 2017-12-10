using CourseApp.DataAccess.Models;
using System.Data.Common;

namespace CourseApp.DataAccess.Oracle
{
    public static class OracleDataMapper
    {
        public static Course MapCourse(DbDataReader dataReader)
        {
            double coursePrice = dataReader.IsDBNull(2)
                    ? default(double)
                    : dataReader.GetDouble(2);

            return new Course(
                dataReader.GetInt32(0),
                dataReader.GetString(1),
                coursePrice);
        }

        public static Course MapCourseWithRating(DbDataReader dataReader)
        {
            double coursePrice = dataReader.IsDBNull(3)
                ? default(double)
                : dataReader.GetDouble(3);

            double? courseRating = dataReader.IsDBNull(2)
                ? new double?()
                : dataReader.GetDouble(2);

            return new Course(
                dataReader.GetInt32(0),
                dataReader.GetString(1),
                coursePrice,
                courseRating);
        }
    }
}
