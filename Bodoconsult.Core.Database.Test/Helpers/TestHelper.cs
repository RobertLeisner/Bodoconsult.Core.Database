// Copyright (c) Bodoconsult EDV-Dienstleistungen GmbH. All rights reserved.


using System.IO;

namespace Bodoconsult.Core.Database.Test.Helpers
{
    /// <summary>
    /// Helper class for unit tests
    /// </summary>
    internal static class TestHelper
    {
        public static string TestFolder => @"C:\temp\EntityBackup";

        /// <summary>
        /// Default ctor
        /// </summary>
        static TestHelper()
        {
            if (!Directory.Exists(TestFolder))
            {
                Directory.CreateDirectory(TestFolder);
            }
        }

    }
}
