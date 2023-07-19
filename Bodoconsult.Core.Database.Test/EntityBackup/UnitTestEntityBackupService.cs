// Copyright (c) Bodoconsult EDV-Dienstleistungen GmbH. All rights reserved.

using System;
using System.Collections.Generic;
using Bodoconsult.Core.Database.EntityBackup;
using Bodoconsult.Core.Database.Test.Helpers;
using NUnit.Framework;

namespace Bodoconsult.Core.Database.Test.EntityBackup
{
    [TestFixture]
    internal class UnitTestEntityBackupService
    {

        
        [Test]
        public void TestCtor()
        {
            // Arrange 
            var dataService = new DemoEntityEntityBackupDataService();

            // Act  
            var backupService = new EntityBackupService<DemoEntity>(dataService, TestHelper.TestFolder, "TestBackup");

            // Assert
            Assert.IsNotNull(backupService);
            Assert.That(backupService.PageSize, Is.Not.EqualTo(0));
            Assert.That(backupService.BackupType, Is.EqualTo(BackupTypeEnum.Daily));
        }

        [Test]public void TestGetFirstStartDateDailyWednesday()
        {
            // Arrange 
            var dataService = new DemoEntityEntityBackupDataService();

            var backupService = new EntityBackupService<DemoEntity>(dataService, TestHelper.TestFolder, "TestBackup")
                {
                    BackupType = BackupTypeEnum.Daily
                };

            var startDate = new DateTime(2023, 7, 19, 19, 0, 0); // Wednesday
 
            var expectedResult = new DateTime(2023, 7, 19);

            // Act  
            var result = backupService.GetFirstStartDate(startDate);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestGetFirstStartDateYearlyWednesday()
        {
            // Arrange 
            var dataService = new DemoEntityEntityBackupDataService();

            var backupService = new EntityBackupService<DemoEntity>(dataService, TestHelper.TestFolder, "TestBackup")
            {
                BackupType = BackupTypeEnum.Yearly
            };

            var startDate = new DateTime(2023, 7, 19, 19, 0, 0); // Wednesday
            
            var expectedResult = new DateTime(2023, 1, 1);

            // Act  
            var result = backupService.GetFirstStartDate(startDate);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestGetFirstStartDateMonthlyWednesday()
        {
            // Arrange 
            var dataService = new DemoEntityEntityBackupDataService();

            var backupService = new EntityBackupService<DemoEntity>(dataService, TestHelper.TestFolder, "TestBackup")
            {
                BackupType = BackupTypeEnum.Monthly
            };

            var startDate = new DateTime(2023, 7, 19, 19, 0, 0); // Wednesday
            
            var expectedResult = new DateTime(2023, 7, 1);

            // Act  
            var result = backupService.GetFirstStartDate(startDate);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestGetFirstStartDateWeeklyWednesday()
        {
            // Arrange 
            var dataService = new DemoEntityEntityBackupDataService();

            var backupService = new EntityBackupService<DemoEntity>(dataService, TestHelper.TestFolder, "TestBackup")
            {
                BackupType = BackupTypeEnum.Weekly
            };

            var startDate = new DateTime(2023, 7, 19, 19, 0, 0); // Wednesday
            var expectedResult = new DateTime(2023, 7, 16);

            // Act  
            var result = backupService.GetFirstStartDate(startDate);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestGetNextStartDateDailyWednesday()
        {
            // Arrange 
            var dataService = new DemoEntityEntityBackupDataService();

            var backupService = new EntityBackupService<DemoEntity>(dataService, TestHelper.TestFolder, "TestBackup")
            {
                BackupType = BackupTypeEnum.Daily
            };

            var startDate = new DateTime(2023, 7, 19, 19, 0, 0); // Wednesday
            var to = DateTime.Now.AddDays(100);
            var expectedResult = new DateTime(2023, 7, 20);

            // Act  
            var result = backupService.GetNextStartDate(startDate, to);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestGetNextStartDateYearlyWednesday()
        {
            // Arrange 
            var dataService = new DemoEntityEntityBackupDataService();

            var backupService = new EntityBackupService<DemoEntity>(dataService, TestHelper.TestFolder, "TestBackup")
            {
                BackupType = BackupTypeEnum.Yearly
            };

            var startDate = new DateTime(2023, 7, 19, 19, 0, 0); // Wednesday
            var to = DateTime.Now.AddDays(500);
            var expectedResult = new DateTime(2024, 1, 1);

            // Act  
            var result = backupService.GetNextStartDate(startDate, to);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestGetNextStartDateMonthlyWednesday()
        {
            // Arrange 
            var dataService = new DemoEntityEntityBackupDataService();

            var backupService = new EntityBackupService<DemoEntity>(dataService, TestHelper.TestFolder, "TestBackup")
            {
                BackupType = BackupTypeEnum.Monthly
            };

            var startDate = new DateTime(2023, 7, 19, 19, 0, 0); // Wednesday
            var to = DateTime.Now.AddDays(100);
            var expectedResult = new DateTime(2023, 8, 1);

            // Act  
            var result = backupService.GetNextStartDate(startDate, to);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }



        [Test]
        public void TestGetNextStartDateWeeklyWednesday()
        {
            // Arrange 
            var dataService = new DemoEntityEntityBackupDataService();

            var backupService = new EntityBackupService<DemoEntity>(dataService, TestHelper.TestFolder, "TestBackup")
            {
                BackupType = BackupTypeEnum.Weekly
            };

            var startDate = new DateTime(2023, 7, 19, 19, 0, 0); // Wednesday
            var to = DateTime.Now.AddDays(100);
            var expectedResult = new DateTime(2023, 7, 23);

            // Act  
            var result = backupService.GetNextStartDate(startDate, to);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestBackupToCsv()
        {
            // Arrange 
            var from = new DateTime(2023, 7, 19);
            var to = new DateTime(2023, 7, 22);


            var dataService = new DemoEntityEntityBackupDataService();
            GetDataForService(dataService.DemoEntities, from, to);

            var backupService = new EntityBackupService<DemoEntity>(dataService, TestHelper.TestFolder, "TestBackup");

            // Act  
            backupService.BackupToCsv(from, to);

            // Assert
            Assert.That(backupService.FilesWritten, Is.EqualTo(2));
        }

        private static void GetDataForService(ICollection<DemoEntity> result, DateTime from, DateTime to)
        {
            var entity = new DemoEntity
            {
                Id = 1,
                Name = "Test1",
                Date = from
            };

            result.Add(entity);

            entity = new DemoEntity
            {
                Id = 2,
                Name = "Test2",
                Date = from.AddDays(1)
            };

            result.Add(entity);

            entity = new DemoEntity
            {
                Id = 3,
                Name = "Test3",
                Date = to.AddDays(1)
            };

            result.Add(entity);

            entity = new DemoEntity
            {
                Id = 4,
                Name = "Test4",
                Date = from
            };

            result.Add(entity);
        }
    }
}
