namespace SPRM.Business.Interfaces
{
    public interface IStaffService
    {
        bool ApproveTransaction(object transactionDto);
        bool ManageSystemAccount(object accountDto);
        bool CreateResearchTopic(object topicDto);
    }
}
