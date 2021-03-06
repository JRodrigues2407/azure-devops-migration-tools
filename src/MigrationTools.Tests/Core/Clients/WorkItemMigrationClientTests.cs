﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MigrationTools.DataContracts;
using MigrationTools.Clients;
using System;
using System.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace MigrationTools.Clients.Tests
{
    [TestClass()]
    public class WorkItemMigrationClientTests
    {
        [TestMethod()]
        public void TestGetWorkItems()
        {
            IWorkItemMigrationClient sink = new WorkItemMigrationClientMock();
            var list = sink.GetWorkItems();
            //
            Assert.IsTrue(list.Count() == 5);
        }

        [TestMethod()]
        public void TestPersistNewWorkItem()
        {
            IWorkItemMigrationClient sink = new WorkItemMigrationClientMock();
            sink.PersistWorkItem(new WorkItemData { Title = "Item 6" });
            //
            var list = sink.GetWorkItems();
            Assert.IsTrue(list.Count() == 6);
        }

        [TestMethod()]
        public void TestpersistExistingItem()
        {
            IWorkItemMigrationClient sink = new WorkItemMigrationClientMock();
            var workItem = sink.GetWorkItems().First();
            workItem.Title = "New Title";
            sink.PersistWorkItem(workItem);
            //
            var list = sink.GetWorkItems();
            Assert.IsTrue(list.Count() == 5);
            var updatedworkItem = sink.GetWorkItems().First();
            Assert.IsTrue(updatedworkItem.Title == workItem.Title);
        }

    }
}
