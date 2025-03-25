using UnityEngine;
using UnityEngine.VFX;

namespace NewPortals
{
    public class PortalComponents : MonoBehaviour
    {
        public PortalBehaviour Portal;
        public PortalBaseEffect BasePortal;
        public TeleportLeaveEffect LeaveEffect;
        public TeleportArriveEffect ArriveEffect;
    }
}
