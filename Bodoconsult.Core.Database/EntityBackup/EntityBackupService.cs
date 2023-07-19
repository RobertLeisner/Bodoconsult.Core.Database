// Copyright (c) Bodoconsult EDV-Dienstleistungen GmbH. All rights reserved.


using System;
using System.IO;
using System.Text;
using Bodoconsult.Core.Database.Interfaces;

namespace Bodoconsult.Core.Database.EntityBackup;

public class EntityBackupService<T> : IEntityBackupService<T> where T : class
{

    public EntityBackupService(IEntityBackupDataService<T> dataService, string targetFolder, string fileName)
    {
        DataService = dataService;
        TargetFolder = targetFolder;
        FileName = fileName;
    }

    /// <summary>
    ///  Describes the manner how the data to backup are written in backup files. Daily means the data for one day is written in one file. Default: daily
    /// </summary>
    public BackupTypeEnum BackupType { get; set; } = BackupTypeEnum.Daily;

    /// <summary>
    /// Current page size for saving data. Default: 50 rows
    /// </summary>
    public int PageSize { get; set; } = 50;

    /// <summary>
    /// Target folder to store the backup files in
    /// </summary>
    public string TargetFolder { get; }

    /// <summary>
    /// File name for the backup files. Start and end date will be added to the filename
    /// </summary>
    public string FileName { get; }

    /// <summary>
    /// The number of files written during the backup process
    /// </summary>
    public int FilesWritten { get; private set; }

    /// <summary>
    /// The current data service to provide the data for the backup
    /// </summary>
    public IEntityBackupDataService<T> DataService { get; }

    /// <summary>
    /// Main method to start the backup process to CSV files
    /// </summary>
    /// <param name="from">Start date inclusive</param>
    /// <param name="to">End date exclusive</param>
    public void BackupToCsv(DateTime from, DateTime to)
    {
        var pageIndex = 1;

        var startDate = GetFirstStartDate(from);

        var endDate = GetNextStartDate(startDate, to);

        while (true)
        {

            if (!startDate.HasValue)
            {
                break;
            }

            if (!endDate.HasValue)
            {
                break;
            }

            var fileName = GetFileName(startDate.Value, endDate.Value);

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            while (true)
            {
                if (BackupToCsv(startDate.Value, endDate.Value, pageIndex, fileName) == 0)
                {
                    if (File.Exists(fileName))
                    {
                        FilesWritten++;
                    }
                    break;
                }

                pageIndex++;
            }

            startDate = endDate;
            endDate = GetNextStartDate(startDate, to);

        }
    }

    /// <summary>
    /// Main method to start the backup process to CSV files
    /// </summary>
    /// <remarks>Method public for unit testing. Do not use directly</remarks>
    /// <param name="from">Start date inclusive</param>
    /// <param name="to">End date exclusive</param>
    /// <param name="pageIndex">Current page index</param>
    /// <param name="fileName"></param>
    /// <returns>Number of rows affected</returns>
    public int BackupToCsv(DateTime from, DateTime to, int pageIndex, string fileName)
    {

        var data = DataService.GetData(from, to, PageSize, pageIndex);

        var sb = new StringBuilder();

        var count = data.Count;

        if (count == 0)
        {
            return 0;
        }

        foreach (var item in data)
        {
            DataService.FormatAsString(item, sb);
        }

        File.AppendAllText(fileName, sb.ToString(), Encoding.UTF8);

        DataService.RemoveData(data);



        return count;
    }

    private string GetFileName(DateTime from, DateTime to)
    {
        return Path.Combine(TargetFolder, BackupType == BackupTypeEnum.Daily ?
            $"{FileName}_{from:yyyyMMdd}.csv" :
            $"{FileName}_{from:yyyyMMdd}-{to:yyyyMMdd}.csv");
    }

    public DateTime? GetFirstStartDate(DateTime lastStartDate)
    {

        var nextStartDate = new DateTime(lastStartDate.Year, lastStartDate.Month, lastStartDate.Day);

        switch (BackupType)
        {
            case BackupTypeEnum.Daily:
                return nextStartDate;
            case BackupTypeEnum.Weekly:
                var day = nextStartDate.DayOfWeek;
                return nextStartDate.AddDays(-(int)day);
            case BackupTypeEnum.Monthly:
                return new DateTime(nextStartDate.Year, lastStartDate.Month, 1);
            case BackupTypeEnum.Yearly:
                return new DateTime(nextStartDate.Year, 1, 1);
            default:
                throw new ArgumentOutOfRangeException();
        }

    }

    /// <summary>
    /// Get the start date for the next interval for the given <see cref="BackupType"/>
    /// </summary>
    /// <param name="lastStartDate">Last start date</param>
    /// <param name="to">End date (start date has to be smaller then end date)</param>
    /// <returns>The next valid start date or null</returns>
    public DateTime? GetNextStartDate(DateTime? lastStartDate, DateTime to)
    {

        if (lastStartDate == null)
        {
            return null;
        }

        var nextStartDate = new DateTime(lastStartDate.Value.Year, lastStartDate.Value.Month, lastStartDate.Value.Day);

        switch (BackupType)
        {
            case BackupTypeEnum.Daily:
                nextStartDate = nextStartDate.AddDays(1);
                break;
            case BackupTypeEnum.Weekly:
                nextStartDate = nextStartDate.AddDays(7 - (int)nextStartDate.DayOfWeek);
                break;
            case BackupTypeEnum.Monthly:
                nextStartDate = new DateTime(nextStartDate.Year, nextStartDate.Month + 1, 1);
                break;
            case BackupTypeEnum.Yearly:
                nextStartDate = new DateTime(nextStartDate.Year + 1, 1, 1);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return nextStartDate > to ? null : nextStartDate;
    }
}