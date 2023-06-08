namespace Core
{
    public static class Cursor
    {
        public static void Lock() => UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.Locked;
        public static void LockInWindow() => UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.Locked;
        public static void UnLock() => UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.None;
        public static void Show() => UnityEngine.Cursor.visible = true;
        public static void Hide() => UnityEngine.Cursor.visible = false;
    }
}