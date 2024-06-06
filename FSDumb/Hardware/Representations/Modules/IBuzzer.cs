namespace Vroumed.FSDumb.Hardware.Representations.Modules
{
    public interface IBuzzer
    {
        /// <summary>
        /// Start the buzzer at the specified <see cref="Frequency"/>
        /// </summary>
        public void Start();

        /// <summary>
        /// Stop the buzzer
        /// </summary>
        public void Stop();

        /// <summary>
        /// Current frequency of the buzzer
        /// </summary>
        public int Frequency { get; set; }

        /// <summary>
        /// Call a distinctive ping notification
        /// </summary>
        public void Ping();
    }
}
