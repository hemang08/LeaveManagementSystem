using LeaveManagementSystem.Data.DBRepository.Leave;

namespace LeaveManagementSystem.Data
{
    public static class DataRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dataDictionary = new Dictionary<Type, Type>
            {
                {typeof(ILeaveRepository),typeof(LeaveRepository) },
            };
            return dataDictionary;
        }
    }
}