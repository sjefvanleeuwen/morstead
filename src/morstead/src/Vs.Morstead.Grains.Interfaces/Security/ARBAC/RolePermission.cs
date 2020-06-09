namespace Vs.Morstead.Grains.Interfaces.Security.ARBAC
{
    public class RolePermission
    {
        public string RoleName { get; set; }
        public string Resource { get; set; }
        public string Operation { get; set; }
        public bool Allow { get; set; }
    }
}
