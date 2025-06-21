using LeaveManagementSystem.Service.Leave;

namespace LeaveManagementSystem.Service
{
    public static class ServiceRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var serviceDictonary = new Dictionary<Type, Type>
            {
               {typeof(ILeaveService),typeof(LeaveService) }
            };
            return serviceDictonary;
        }
    }
}