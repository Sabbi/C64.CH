using System;

namespace C64.Data.Archive
{
    public abstract class BaseArchiveService
    {
        /// <summary>
        /// The standard fileIdDiz to be packed with the file
        /// </summary>
        protected string fileIdDiz
        {
            get
            {
                return "This C64-Masterpiece was downloaded from:" + Environment.NewLine
                        + "__________________________________________________" + Environment.NewLine
                        + "       ____   __     __  __      ____   __  __" + Environment.NewLine
                        + "      / __/\\ / /__  / /_/ /\\    / __/\\ / /_/ /\\" + Environment.NewLine
                        + "     / /_ \\ / __ /\\/__   / /_  / /_ \\// __  / /" + Environment.NewLine
                        + "    / ____/\\/____/ /\\_/__/ /_/\\/____/\\/_/\\/_/ /" + Environment.NewLine
                        + "   \\____\\/\\____\\/   \\__\\/\\_\\/\\____\\/\\_\\/\\_\\/" + Environment.NewLine
                        + "" + Environment.NewLine
                        + "" + Environment.NewLine
                        + "       C64.CH - Your #1 Source for C64-Demos" + Environment.NewLine
                        + "__________________________________________________" + Environment.NewLine;
            }
        }
    }
}