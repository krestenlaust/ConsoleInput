namespace ConsoleInput
{
    /// <summary>
    /// Represents a device which outputs console cursor positions.
    /// </summary>
    public interface ICursorDevice : IDevice
    {
        /// <summary>
        /// Gets the column position of the cursor (based on console window).
        /// </summary>
        short X { get; }

        /// <summary>
        /// Gets the row position of mouse (based on console window).
        /// </summary>
        short Y { get; }
    }
}
