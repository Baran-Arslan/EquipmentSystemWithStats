namespace _Common.iCare.Extensions {
    public static class NumberExtensions {
        
        /// <summary>
        /// Get percent of a given number.
        /// </summary>
        /// <param name="number">Number that you want to get percent of</param>
        /// <param name="percent">Percent amount</param>
        /// <returns></returns>
        public static float GetPercent(this float number, float percent) => number * percent / 100;
    }
}