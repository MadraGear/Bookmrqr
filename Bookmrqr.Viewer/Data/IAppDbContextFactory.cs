namespace Bookmrqr.Viewer.Data
{
    public interface IAppDbContextFactory
    {
        AppDbContext Create();
    }
}