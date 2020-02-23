using Hangfire.PostgreSql;
using Hangfire.Tags.Storage;

namespace Hangfire.Tags.PostgreSql
{
    /// <summary>
    /// Provides extension methods to setup Hangfire.Tags
    /// </summary>
    public static class GlobalConfigurationExtensions
    {
        /// <summary>
        /// Configures Hangfire to use Tags.
        /// </summary>
        /// <param name="configuration">Global configuration</param>
        /// <param name="options">Options for tags</param>
        /// <param name="sqlOptions">Options for postgre storage</param>
        /// <returns></returns>
        public static IGlobalConfiguration UseTagsWithPostgreSql(this IGlobalConfiguration configuration, TagsOptions options = null, PostgreSqlStorageOptions postgreSqlOptions = null)
        {
            options = options ?? new TagsOptions();
            postgreSqlOptions = postgreSqlOptions ?? new PostgreSqlStorageOptions();

            options.Storage = new PostgreSqlTagsServiceStorage(postgreSqlOptions);

            TagsServiceStorage.Current = options.Storage;

            var config = configuration.UseTags(options);
            return config;
        }
    }
}
