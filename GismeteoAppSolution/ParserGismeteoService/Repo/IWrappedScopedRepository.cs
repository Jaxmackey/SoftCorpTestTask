using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParserGismeteoService.Repo
{
    public interface IWrappedScopedRepository: IRepositoryWrapper
    {
        void Save();
    }
}
