namespace Krk.Doors
{
    public class DoorAutoController
    {
        readonly DoorController doorController;

        public DoorAutoController(DoorController doorController)
        {
            this.doorController = doorController;
        }

        public void OpenAndLock()
        {
            doorController.OpenStart();
            doorController.SetLocked(true);
        }

        public void UnlockAndClose()
        {
            doorController.SetLocked(false);
            doorController.CloseStart();
        }
    }
}