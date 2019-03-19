using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;

namespace TestingStuff.EFCore
{
    public static class DatabaseRoot
    {
        private static InMemoryDatabaseRoot _root;

        public static InMemoryDatabaseRoot Root => _root ?? (_root = new InMemoryDatabaseRoot());
    }
}
