namespace Vroumed.FSDumb.Utils
{
    public static class MathUtils
    {
        /// <summary>
        ///   Lerp is a function that interpolates between two values. It is used to calculate the value of an point at a specific
        ///   time.
        /// </summary>
        /// <param name="fro">Start value of the lerp</param>
        /// <param name="to">End value of the lerp</param>
        /// <param name="ratio">Float between 0 and 1 that represent the progression between Begin And End</param>
        /// <returns>the interpolated value </returns>
        public static float Lerp(float fro, float to, float ratio)
        {
            return fro + (to - fro) * ratio;
        }
    }
}
