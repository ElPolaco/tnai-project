namespace Service.SerachAndPage
{
    public class Pager
    {
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }

        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

        public string KeyWord { get; set; }
        public Pager()
        {

        }
        public Pager(int totalItems, int page, int pageSize=10)
        {
            TotalItems = totalItems;

            PageSize = pageSize;

            TotalPages = (int)Math.Ceiling((decimal)totalItems/(decimal)pageSize);
            CurrentPage = page;

            StartPage = page - 4;
            EndPage = page + 5;


            if(StartPage < 0)
            {
                EndPage = page - (StartPage - 1);
                StartPage = 1;
            }

            if(EndPage > TotalPages)
            {
                EndPage = TotalPages;
                if(TotalPages > 10)
                {
                    StartPage = TotalPages - 9;
                }
            }
        }

    }
}
