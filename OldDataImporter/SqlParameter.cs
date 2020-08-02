namespace OldDataImporter
{
    internal class SqlParameter
    {
        private string v;
        private string tableName;

        public SqlParameter(string v, string tableName)
        {
            this.v = v;
            this.tableName = tableName;
        }
    }
}