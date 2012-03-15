// -----------------------------------------------------------------------
// <copyright file="Class1.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace RentItServiceLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using RentItDatabase;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TestClass
    {

        public static void Main()
        {
            DatabaseDataContext db = new DatabaseDataContext();

            var acs = from ac in db.Accounts select ac;

            foreach (RentItDatabase.Account ac in acs)
            {
                Console.WriteLine(ac.full_name);
            }

        }

    }
}
