namespace ConsoleInput
{
    // TODO: Consider renaming this.
    public interface ICursorDevice
    {
        /// <summary>
        /// Gets the column position of the cursor (based on console window).
        /// </summary>
        /// <returns></returns>
        short GetX();

        /// <summary>
        /// Gets the row position of mouse (based on console window).
        /// </summary>
        short GetY();
    }
}
