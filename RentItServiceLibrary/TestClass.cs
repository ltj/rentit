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
        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public Account ValidateCredentials(AccountCredentials credentials)
        {
            DatabaseDataContext db = new DatabaseDataContext();

            IQueryable<RentItDatabase.Account> result = from ac in db.Accounts
                                                        where ac.user_name.Equals(credentials.UserName)
                                                        select ac;

            // If the result contains no accounts, there do not exist an account in the database with the user name
            // that is provided in the credentials...
            if (result.Count() == 0)
            {
                // ... throw an exception to inform the caller.
                throw new InvalidCredentialsException("Submitted user name does not exist.");
            }

            RentItDatabase.Account account = result.First();

            // If the submitted hashed password does not match the one stored in the database...
            if (!account.password.Equals(credentials.HashedPassword))
            {
                // ... throw an exception to inform the caller.
                throw new InvalidCredentialsException("Submitted password is incorrect.");
            }

            // The credentials has successfully been evaluated, return account details to caller.
            return RentItServiceLibrary.Account.ValueOf(account);
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newAccountData"></param>
        /// <returns></returns>
        public static bool CreateNewUser(RentItServiceLibrary.Account newAccountData)
        {
            DatabaseDataContext db = new DatabaseDataContext();

            // If there exist an account with the submitted user name...
            if (db.Accounts.Exists(ac => ac.user_name.Equals(newAccountData.UserName)))
            {
                // ...the request is told so.
                throw new UserCreationException("The specified user name is already in use");
            }

            // The user name is free, create a new instance with the data provided.
            RentItDatabase.Account baseAccount = new RentItDatabase.Account()
                {
                    user_name = newAccountData.UserName,
                    full_name = newAccountData.FullName,
                    email = newAccountData.Email,
                    password = newAccountData.HashedPassword
                };
            User_account newAccount = new User_account()
                {
                    credit = 0,
                    Account = baseAccount
                };

            db.User_accounts.InsertOnSubmit(newAccount);
            db.SubmitChanges();
            return true;
        }

        /// <author>Kenneth Søhrmann</author>
        /// <summary>
        /// Used for testing purposes only.
        /// </summary>
        public static void Main()
        {
            try
            {
                CreateNewUser(new RentItServiceLibrary.Account("hulemand", "Hulebo 23", "hule@hjem.dk", "hulemis"));
            }
            catch (UserCreationException e)
            {
                Console.WriteLine(e.Message);
            }

            DatabaseDataContext db = new DatabaseDataContext();

            var acs = from ac in db.Accounts select ac;

            foreach (RentItDatabase.Account ac in acs)
            {
                Console.Write(ac.user_name + "\t");
                Console.WriteLine(ac.full_name);
            }
        }

    }
}
