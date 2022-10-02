using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Bookmrqr.EventProcessor.Data.Databases
{
    public class InMemoryReadDatabase : IReadDatabase
    {
        private ConcurrentDictionary<string, AccountBookmarks> _accounts;

        public InMemoryReadDatabase()
        {
            _accounts = new ConcurrentDictionary<string, AccountBookmarks>();
        }

        public Account GetAccount(string userName)
        {
            AccountBookmarks accountBookmarks = GetByUserName(userName);
            if (accountBookmarks == null)
                return null;

            return accountBookmarks.Account;
        }

        public void AddAccount(Account acount)
        {
            if (_accounts.ContainsKey(acount.Id))
            {
                return;
            }

            _accounts.TryAdd(acount.Id, new AccountBookmarks() { Account = acount });
        }

        public bool DeleteAccount(string userName)
        {
            AccountBookmarks accountBookmarks;
            _accounts.TryRemove(userName, out accountBookmarks);
            return (accountBookmarks != null);
        }

        public Bookmark AddBookmark(string userName, Bookmark bookmark)
        {
            AccountBookmarks accountBookmarks = GetByUserName(userName);
            if (accountBookmarks == null)
            {
                AddAccount(new Account { Id = userName, DisplayName = userName, Email = string.Empty });
                accountBookmarks = GetByUserName(userName);
            }

            accountBookmarks.Bookmarks.Add(bookmark.Id, bookmark);

            return bookmark;
        }

        public bool DeleteBookmark(string userName, string bookmarkId)
        {
            AccountBookmarks accountBookmarks = GetByUserName(userName);
            if (accountBookmarks == null)
                return false;

            Bookmark bookmark = null;
            if (!accountBookmarks.Bookmarks.TryGetValue(bookmarkId, out bookmark))
                return false;

            accountBookmarks.Bookmarks.Remove(bookmarkId);

            return true;
        }

        public IList<Bookmark> GetBookmarks(string userName, int pageSize, int pageNumber)
        {
            List<Bookmark> bookmarks = new List<Bookmark>();

            AccountBookmarks accountBookmarks = GetByUserName(userName);
            if (accountBookmarks == null)
                return bookmarks;

            return accountBookmarks.Bookmarks.Values
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        private AccountBookmarks GetByUserName(string userName)
        {
            AccountBookmarks accountBookmarks = null;
            if (!_accounts.TryGetValue(userName, out accountBookmarks))
            {
                Trace.TraceInformation(DateTime.Now.ToString("s") + " - Account not found" + userName);
            }
            return accountBookmarks;
        }

        private class AccountBookmarks
        {
            public AccountBookmarks()
            {
                Bookmarks = new Dictionary<string, Bookmark>();
            }
            public Account Account { get; set; }
            public Dictionary<string, Bookmark> Bookmarks { get; private set; }
        }
    }
}
