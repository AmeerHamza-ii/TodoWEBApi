namespace ToDoApplication.DAL.DTO
{
    public class UserListRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
