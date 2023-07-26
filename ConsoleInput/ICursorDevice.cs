namespace ConsoleInput
{
    /// <summary>
    /// Represents a device which outputs console cursor positions.
    /// </summary>
    {
        /// <summary>
        /// Gets the column position of the cursor (based on console window).
        /// </summary>
        /// <returns></returns>
        short X { get; }

        /// <summary>
        /// Gets the row position of mouse (based on console window).
        /// </summary>
        short Y { get; }
    }
}
