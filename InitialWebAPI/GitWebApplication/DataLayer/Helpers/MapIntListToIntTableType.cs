using System.Collections.Generic;
using System.Data;


namespace DataLayer.Helpers
{
    public class MapIntListToIntTableType
    {
        public static DataTable Map(IEnumerable<int> items)
        {
            DataTable returnValue = new DataTable();

            try
            {
                if (items != null)
                {
                    returnValue.Columns.Add("Item", typeof(int));
                    foreach (int item in items)
                    {
                        returnValue.Rows.Add(item);
                    }
                }

                return returnValue;
            }
            catch
            {
                if (returnValue != null)
                {
                    returnValue.Dispose();
                }
                throw;
            }
        }
    }

}