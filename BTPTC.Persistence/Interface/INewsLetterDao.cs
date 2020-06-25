using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTPTC.Domain;


namespace BTPTC.Persistence.Interface
{
    public interface INewsLetterDao
    {
        int CheckNewsTitleName(string NewsTitle, Guid GUID);
        Int64 SaveNewsLetter(NewsLetter NL);
     
        List<NewsLetter> Get(String Year = null, int PageIndex = 1, int PageSize = 10);

        NewsLetter GetEditNewsLetterGUID(Guid guid);

        Int64 DeleteNewsLetter(Guid GUID, Guid UserId, string SystemIp);

        List<NewsLetter> ViewNewsLetter();

        List<NewsLetter> GetYearViewNewsLetter(int year);

    }
}
