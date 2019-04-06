using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesMediaCenter
{
    /// <summary>
    /// Interface permettant de créer un proxy permettant de requeter la base de données IMDB
    /// </summary>
    public interface IIDMBRequest
    {
        Task<string> GetInformation(string title);
    }
}
