namespace ConsoleInput.Devices
{
    // TODO: Make copy of enum to fit name.
    public class KeyboardV2 : IButtonDevice<Keyboard.VirtualKey>
    {
        public bool IsButtonDown(Keyboard.VirtualKey button)
        {
            throw new System.NotImplementedException();
        }

        public bool IsButtonPressed(Keyboard.VirtualKey button)
        {
            throw new System.NotImplementedException();
        }

        public bool IsButtonReleased(Keyboard.VirtualKey button)
        {
            throw new System.NotImplementedException();
        }
    }
}
