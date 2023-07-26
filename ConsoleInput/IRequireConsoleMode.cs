namespace ConsoleInput
{
    /// <summary>
    /// Requests that the Console Mode be changed to include a specific set of permissions.
    /// Only valid for objects also implementing <see cref="IDevice"/>.
    /// </summary>
    public interface IRequireConsoleMode : IDevice
    {
        /// <summary>
        /// Gets the console mode to include.
        /// </summary>
        /// <returns>The console mode to include.</returns>
        uint GetConsoleMode();
    }
}
