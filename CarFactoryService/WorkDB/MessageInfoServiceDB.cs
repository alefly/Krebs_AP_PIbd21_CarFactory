using CarFactory;
using CarFactoryService.BindingModels;
using CarFactoryService.Interfaces;
using CarFactoryService.ViewModels;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarFactoryService.WorkDB
{
    public class MessageInfoServiceDB : IMessageInfo
    {
        private CarFactoryDbContext context;

        public MessageInfoServiceDB(CarFactoryDbContext context)
        {
            this.context = context;
        }

        public List<MessageInfoView> GetList()
        {
            List<MessageInfoView> result = context.MessageInfos
                .Where(rec => !rec.ConsumerId.HasValue)
                .Select(rec => new MessageInfoView
                {
                    MessageId = rec.MessageId,
                    ConsumerName = rec.FromMailAddress,
                    DateDelivery = rec.DateDelivery,
                    Subject = rec.Subject,
                    Body = rec.Body
                })
                .ToList();
            return result;
        }

        public MessageInfoView GetElement(int id)
        {
            MessageInfo element = context.MessageInfos.Include(rec => rec.Consumer)
                .FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new MessageInfoView
                {
                    MessageId = element.MessageId,
                    ConsumerName = element.Consumer.ConsumerName,
                    DateDelivery = element.DateDelivery,
                    Subject = element.Subject,
                    Body = element.Body
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(BindingMessageInfo model)
        {
            MessageInfo element = context.MessageInfos.FirstOrDefault(rec => rec.MessageId == model.MessageId);
            if (element != null)
            {
                return;
            }
            var message = new MessageInfo
            {
                MessageId = model.MessageId,
                FromMailAddress = model.FromMailAddress,
                DateDelivery = model.DateDelivery,
                Subject = model.Subject,
                Body = model.Body
            };

            var mailAddress = Regex.Match(model.FromMailAddress, @"(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))");
            if (mailAddress.Success)
            {
                var client = context.Consumers.FirstOrDefault(rec => rec.Mail == mailAddress.Value);
                if (client != null)
                {
                    message.ConsumerId = client.Id;
                }
            }

            context.MessageInfos.Add(message);
            context.SaveChanges();
        }
    }
}