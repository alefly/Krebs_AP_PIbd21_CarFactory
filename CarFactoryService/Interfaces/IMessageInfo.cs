using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactoryService.Interfaces
{
    public interface IMessageInfo
    {
        List<MessageInfoView> GetList();

        MessageInfoView GetElement(int id);

        void AddElement(BindingMessageInfo model);
    }
}