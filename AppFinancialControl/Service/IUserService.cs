using AppFinancialControl.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppFinancialControl.Service
{
    public interface IUserService
    {
        public bool Login(User user);
        public void Insert(User user);
        public void Delete(User user);
        public void Update(User user);
    }
}
