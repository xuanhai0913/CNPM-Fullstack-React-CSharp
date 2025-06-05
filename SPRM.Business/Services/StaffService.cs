using SPRM.Business.Interfaces;

namespace SPRM.Business.Services
{
    public class StaffService : IStaffService
    {
        public bool ApproveTransaction(object transactionDto)
        {
            // TODO: Duyệt giao dịch
            return true;
        }

        public bool ManageSystemAccount(object accountDto)
        {
            // TODO: Quản lý tài khoản hệ thống
            return true;
        }

        public bool CreateResearchTopic(object topicDto)
        {
            // TODO: Tạo đề tài nghiên cứu
            return true;
        }
    }
}