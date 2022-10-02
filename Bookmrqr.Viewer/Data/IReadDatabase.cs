using System.Collections.Generic;

namespace Bookmrqr.Viewer.Data
{
    public interface IReadDatabase
    {
        Account GetAccount(string userName);

        void AddAccount(Account acount);

        bool DeleteAccount(string userName);

        Bookmark AddBookmark(string userName, Bookmark bookmark);

        bool DeleteBookmark(string userName, string bookmarkId);

        IList<Bookmark> GetBookmarks(string userName, int pageSize, int pageNumber);
    }
}
