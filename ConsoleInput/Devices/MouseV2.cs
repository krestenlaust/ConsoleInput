using ConsoleInput.Logic;

namespace ConsoleInput.Devices
{
    public class MouseV2 : IButtonDevice<MouseButton>, ICursorDevice
    {
        /// <summary>
        /// The amount of buttons available.
        /// </summary>
        public const int MouseButtonCount = 5;

        public MouseV2()
        {
            DataFlipFlopArray dff = new DataFlipFlopArray(MouseButtonCount);
        }

        public short GetX()
        {
            throw new System.NotImplementedException();
        }

        public short GetY()
        {
            throw new System.NotImplementedException();
        }

        public bool IsButtonDown(MouseButton button)
        {
            throw new System.NotImplementedException();
        }

        public bool IsButtonPressed(MouseButton button)
        {
            throw new System.NotImplementedException();
        }

        public bool IsButtonReleased(MouseButton button)
        {
            throw new System.NotImplementedException();
        }
    }
}
