using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitDbAccessEFTest.TestingDatabase
{
    public interface ITestingDbContext
    {
        void ClearDatabase();
    }
}
