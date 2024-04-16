
namespace FileProcessing
{
    /// <summary>
    /// Class for choosing method.
    /// </summary>
    public class Choosing
    {
        /// <summary>
        /// Choose data by field from List.
        /// </summary>
        /// <param name="recreators"></param>
        /// <param name="filterBy"></param>
        /// <param name="field"></param>
        /// <returns>new List with resul of choosing.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static List<Recreator> ChoosingByField(List<Recreator> recreators, string filterBy, string field)
        {
            List<Recreator> filteredList = new List<Recreator>();
            switch (field)
                {
                case "mainobject":
                    filteredList = (from r in recreators
                                    where r.MainObjects.Contains(filterBy, StringComparison.OrdinalIgnoreCase)
                                    select r).ToList();
                    break;
                    
                case "workplace":
                    filteredList = (from r in recreators
                                    where r.Workplace.Contains(filterBy, StringComparison.OrdinalIgnoreCase)
                                    select r).ToList();
                    break;
                case "year":
                    filteredList = (from r in recreators
                                    where r.RankYear.ToString().Contains(filterBy, StringComparison.OrdinalIgnoreCase)
                                    select r).ToList();
                    break;
                default:
                    throw new ArgumentException("Invalid choosing field. Available options: 'mainobject', 'workplase or 'year'.");
            }
            return filteredList;
        }
    }
}