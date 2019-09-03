
using Conference.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Conference.EntityFrameworkCore
{
    public class DbContextFactory : IDesignTimeDbContextFactory<ConferenceContext>
    {
        public ConferenceContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ConferenceContext>();
            //读取Appsettings.json的数据库连接字符串
            var connection = JsonConfigurationHelper.GetAppSettings<ConnectionService>("appsettings.json", "ConnectionService");

            builder.UseSqlServer(connection.ConnectionSqlService);

            return new ConferenceContext(builder.Options);
        }       
    }
}
