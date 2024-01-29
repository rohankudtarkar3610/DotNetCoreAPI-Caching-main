
namespace Demo7.DataAccess.Contracts
{
    public interface IObjectState
    {
        long Id { get; set; }
        DALEnums.ObjectState EntityState { get; set; }
    }
}
