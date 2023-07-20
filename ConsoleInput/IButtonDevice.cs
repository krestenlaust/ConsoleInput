namespace ConsoleInput
{
    public interface IButtonDevice<in T>
    {
        /// <summary>
        /// Returns true while the button is being held down.
        /// </summary>
        /// <param name="button">The button identifier.</param>
        /// <returns>True if the button is currently pressed, otherwise false.</returns>
        bool IsButtonDown(T button);

        /// <summary>
        /// Returns true during the first cycle the button is held down.
        /// </summary>
        /// <param name="button">The button identifier.</param>
        /// <returns>True if the button has just been pressed, otherwise false.</returns>
        bool IsButtonPressed(T button);

        /// <summary>
        /// Returns true the cycle after the button has been released.
        /// </summary>
        /// <param name="button">The button identifier.</param>
        /// <returns>True if the button has just been released, otherwise false.</returns>
        bool IsButtonReleased(T button);
    }
}
