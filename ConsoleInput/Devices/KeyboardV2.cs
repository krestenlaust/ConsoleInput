namespace ConsoleInput.Devices
{
    public class KeyboardV2 : IDevice, IButtonDevice<KeyboardButton>
    {
        public bool IsButtonDown(KeyboardButton button)
        {
            throw new System.NotImplementedException();
        }

        public bool IsButtonPressed(KeyboardButton button)
        {
            throw new System.NotImplementedException();
        }

        public bool IsButtonReleased(KeyboardButton button)
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}
