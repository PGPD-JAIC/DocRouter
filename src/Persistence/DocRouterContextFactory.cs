using Microsoft.EntityFrameworkCore;

namespace DocRouter.Persistence
{
    /// <summary>
    /// Implemention of <see cref="CIU_CarjackingsContextFactoryBase{TContext}"></see>
    /// </summary>
    public class DocRouterContextFactory : DocRouterContextFactoryBase<DocRouterContext>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="DocRouterContext"/>
        /// </summary>
        /// <param name="options">A <see cref="DbContextOptions"/> object.</param>
        /// <returns>A <see cref="DocRouterContext"/></returns>
        protected override DocRouterContext CreateNewInstance(DbContextOptions<DocRouterContext> options)
        {
            return new DocRouterContext(options);
        }
    }
}
