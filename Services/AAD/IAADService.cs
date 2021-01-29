using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudDataManagement.Inteface
{
    public interface IAADService
    {
        Task Add();
        Task Delete();
        Task Update(); 
        Task Select();
    }
}
