namespace FileProcessing
{
    /// <summary>
    /// Class for sorting method.
    /// </summary>
    public class Sorting
    {
        /// <summary>
        /// Sort List by field.
        /// </summary>
        /// <param name="recreators"></param>
        /// <param name="sortBy"></param>
        /// <returns>Sorted List of Recreators.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static List<Recreator> Sort(List<Recreator> recreators, string sortBy)
        {
            List<Recreator> sortedList;

            if (sortBy.ToLower() == "name")
            {
                sortedList = (from r in recreators orderby r.Name select r).ToList();
            }
            else if (sortBy.ToLower() == "year")
            {
                sortedList = (from r in recreators orderby r.RankYear descending select r).ToList();
            }
            else
            {
                throw new ArgumentException("Invalid sorting field. Available options: 'name' or 'year'.");
            }

            return sortedList;
        }
    }
}
