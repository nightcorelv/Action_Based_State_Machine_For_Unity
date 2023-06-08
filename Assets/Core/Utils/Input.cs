using UnityEngine;

namespace Core.Utils
{
    public static class Input
    {
        #region MouseLook

        public static Transform MouseLook
            (
            Transform trans,
            float sensitivityX = 15f,
            float sensitivityY = 15f,
            Enums.Axis2Combine axis = Enums.Axis2Combine.XY,
            float minimumY = -60,
            float maximumY = 60,
            float minimumX = -360,
            float maximumX = 360,
            bool limitX = false,
            bool limitY = true
            )
        {

            if (trans == null)
                return null;

            if (axis == Enums.Axis2Combine.XY)
                trans.localEulerAngles = new Vector3(GetYRotation(), GetXRotation(), 0);
            else if (axis == Enums.Axis2Combine.X)
                trans.localEulerAngles = new Vector3(trans.localEulerAngles.x, GetXRotation(), 0);
            else if (axis == Enums.Axis2Combine.Y)
                trans.localEulerAngles = new Vector3(GetYRotation(-1), trans.localEulerAngles.y, 0);

            return trans;

            float GetXRotation()
            {
                float rotationX = trans.localRotation.eulerAngles.y;
                rotationX += UnityEngine.Input.GetAxis("Mouse X") * sensitivityX;

                if (limitX)
                    rotationX = ClampAngle(rotationX, minimumX, maximumX) % 360;

                return rotationX;
            }
            float GetYRotation(float invert = 1)
            {
                float rotationY = trans.localRotation.eulerAngles.x;
                rotationY += UnityEngine.Input.GetAxis("Mouse Y") * sensitivityY * invert;

                if (limitY)
                    rotationY = ClampAngle(rotationY, minimumY, maximumY) % 360;

                return rotationY;
            }
            // Clamp function that respects IsNone and 360 wrapping
            static float ClampAngle(float angle, float min, float max)
            {
                if (angle < 0f)
                    angle = 360 + angle;

                if (angle > 180f)
                    return Mathf.Max(angle, 360 + min);

                return Mathf.Min(angle, max);
            }

        }

        #endregion

        #region MousePick

        public static RaycastHit MousePick()
        {
            Physics.Raycast(
                Components.CameraBehaviour.instance.transform.position, 
                Components.CameraBehaviour.instance.transform.forward, 
                out RaycastHit hitInfo);
            return hitInfo;
        }

        public static RaycastHit MousePick(float distance)
        {
            Physics.Raycast(
                Components.CameraBehaviour.instance.transform.position, 
                Components.CameraBehaviour.instance.transform.forward, 
                out RaycastHit hitInfo,
                distance);
            return hitInfo;
        }

        public static RaycastHit MousePick(float distance, int layerMask)
        {
            Physics.Raycast(
                Components.CameraBehaviour.instance.transform.position, 
                Components.CameraBehaviour.instance.transform.forward, 
                out RaycastHit hitInfo, 
                distance, 
                layerMask);
            return hitInfo;
        }

        public static RaycastHit MousePick(float distance, LayerMask layerMask)
        {
            Physics.Raycast(
                Components.CameraBehaviour.instance.transform.position, 
                Components.CameraBehaviour.instance.transform.forward, 
                out RaycastHit hitInfo, 
                distance, 
                layerMask);
            return hitInfo;
        }

        #endregion

        #region ScreentPick

        public static RaycastHit ScreenPick(int x, int y)
        {
            Physics.Raycast(
                Components.CameraBehaviour.mainCamera.ScreenPointToRay(new Vector3(x, y, 0)), 
                out RaycastHit hitInfo);

            return hitInfo;
        }

        public static RaycastHit ScreenPick(int x, int y, float distance)
        {
            Physics.Raycast(
                Components.CameraBehaviour.mainCamera.ScreenPointToRay(new Vector3(x, y, 0)), 
                out RaycastHit hitInfo, distance);

            return hitInfo;
        }

        public static RaycastHit ScreenPick(int x, int y, float distance, int layerMask)
        {
            Physics.Raycast(
                Components.CameraBehaviour.mainCamera.ScreenPointToRay(new Vector3(x, y, 0)), 
                out RaycastHit hitInfo, distance, layerMask);

            return hitInfo;
        }

        public static RaycastHit ScreenPick(int x, int y, float distance, LayerMask layerMask)
        {
            Physics.Raycast(
                Components.CameraBehaviour.mainCamera.ScreenPointToRay(new Vector3(x, y, 0)), 
                out RaycastHit hitInfo, distance, layerMask);

            return hitInfo;
        }

        #endregion

    }
}