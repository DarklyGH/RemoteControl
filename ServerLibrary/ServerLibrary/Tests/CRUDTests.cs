using NUnit.Framework;
using ServerLibrary.Database;
using ServerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Tests
{
    class CRUDTests
    {
        List<User> allUsers = new List<User>();
     
        //User CRUD
        [Test]
        public void TestWriteNewUser()
        {
            User user = new User
            {
                Username = "Jeremy",
                Password = "Thisisatest",
                IsAdmin = false
            };

            if (DBConnect.TryCreateUser(user, out User _newUser))
            {
                // The test was successful!!
                Assert.AreEqual(_newUser.Username, user.Username, "The user has been created");
            }
            else
            {
                // The test failed!
                Assert.IsTrue(false, "The test did not write successfully");
            }
        }
        /* DBConnect.SelectAllUsers(); -
         * Assistant call to find the number of rows returned by the reader on SELECT COUNT(*) From Users table
         * default return type is a long for some retarded reason.
         */
        [Test]
        public void TryReadAll() {            
            allUsers = DBConnect.TryReadAllUsers();
            int rowsAffected = (int)DBConnect.CountAllUsers();
            Assert.AreEqual(allUsers.Count, rowsAffected);
        }                  
        [Test]
        public void TryReadSingleUserById()
        {
            User _StaticUser = new User()
            {
                Id = 34,
                Username = "Daryl",
                Password = "ThisIsDaryl",
                IsAdmin = true
            };
            User UserFromDB = DBConnect.GetSingleUser(34);
            Assert.AreEqual(_StaticUser.Id, UserFromDB.Id);
            Assert.AreEqual(_StaticUser.Username, UserFromDB.Username);
            Assert.AreEqual(_StaticUser.Password, UserFromDB.Password);
        }                
        [Test]
        public void UpdateUserPassword()
        {
            User StaticUser = DBConnect.GetSingleUser(43);          
            
            if(DBConnect.UpdateUserPass(StaticUser, "ThisIsUser16", out User _updatedUser))
            {
                Assert.AreNotEqual(StaticUser.Password, _updatedUser.Password, "The Password was updated successfully.");
            } else
            {
                Assert.IsTrue(false, "The passwords were either the same, or the operation failed.");
            } 
        }
        [Test]
        public void DeleteUser()
        {
            if (DBConnect.DeleteUser(40))
            {
                Assert.IsTrue(true, "The User has been deleted successfully!");
            } else
            {
                Assert.IsFalse(true, "There was an error, the user has not been deleted.");
            }
        }

        [Test]
        public void CreateNewCommand()
        {
            //Read user detail from the database - for testing use the last created user
            DataHandler dataHandler = new DataHandler();
            int row = dataHandler.GetLastInsertedId("Users");
            User CommandTestUser = DBConnect.GetSingleUser(row);
            //dataHandler.Dispose(); //Datahandler instance no longer required, so dispose.

            if (DBConnect.TryCreateCommand(CommandTestUser.CreateCommand("TestCommand"), out Command _NewCommand)) {
                Assert.AreEqual(CommandTestUser.UserCommand.CommandName, _NewCommand.CommandName);
            } else
            {
                Assert.IsTrue(false, "The test did not create the command successfully.");
            }
            
            

        }


    }
}

