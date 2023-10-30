using System.Collections.Generic;
using System.Text;

namespace Assets.Codebase.Utils.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Returns string in format "3 2 2"
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static string ToPushupsListString(this List<int> results)
        {
            StringBuilder sb = new StringBuilder();

            int resultIndex = 0;

            // results
            while (resultIndex < results.Count)
            {
                sb.Append(results[resultIndex]).Append(" ");
                resultIndex++;
            }

            return sb.ToString();
        }
    }
}
