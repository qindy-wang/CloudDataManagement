using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudDataManagement
{
    public interface IIntuneService
    {
        Task Delete();
        Task Update();
        Task Add();
        Task Select();
    }
}
