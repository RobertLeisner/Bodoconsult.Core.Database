﻿// Copyright (c) Bodoconsult EDV-Dienstleistungen GmbH. All rights reserved.

using System;
using Bodoconsult.Core.Database.EntityBackup;

namespace Bodoconsult.Core.Database.Interfaces
{
    public interface IEntityBackupService<T> where T : class
    {
        /// <summary>
        ///  Describes the manner how the data to backup are written in backup files. Daily means the data for one day is written in one file. Default: daily
        /// </summary>
        BackupTypeEnum BackupType { get; set; }


        /// <summary>
        /// Current page size for saving data
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// Target folder to store the backup files in
        /// </summary>
        string TargetFolder { get; }

        /// <summary>
        /// File name for the backup files. Start and end date will be added to the filename
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// The number of files written during the backup process
        /// </summary>
        int FilesWritten { get; }


        /// <summary>
        /// The current data service to provide the data for the backup
        /// </summary>
        IEntityBackupDataService<T> DataService { get; }

        /// <summary>
        /// Main method to start the backup process to CSV files
        /// </summary>
        /// <param name="from">Start date inclusive</param>
        /// <param name="to">End date exclusive</param>
        void BackupToCsv(DateTime from, DateTime to);

        /// <summary>
        /// Main method to start the backup process to CSV files
        /// </summary>
        /// <remarks>Method public for unit testing. Do not use directly</remarks>
        /// <param name="from">Start date inclusive</param>
        /// <param name="to">End date exclusive</param>
        /// <param name="pageIndex">Current page index</param>
        /// <param name="fileName">Filename the result should be appended to</param>
        /// <returns>Numbe rof rows affected</returns>
        int BackupToCsv(DateTime from, DateTime to, int pageIndex, string fileName);


        /// <summary>
        /// Get the corrected first start date for the next interval for the given <see cref="BackupType"/>
        /// </summary>
        /// <param name="lastStartDate">Last start date</param>
        /// <returns>The next valid start date or null</returns>
        DateTime? GetFirstStartDate(DateTime lastStartDate);


        /// <summary>
        /// Get the start date for the next interval for the given <see cref="BackupType"/>
        /// </summary>
        /// <param name="lastStartDate">Last start date</param>
        /// <param name="to">End date (start date has to be smaller then end date)</param>
        /// <returns>The next valid start date or null</returns>
        DateTime? GetNextStartDate(DateTime? lastStartDate, DateTime to);


    }
}