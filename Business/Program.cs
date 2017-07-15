using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DTO;
using Business.DataAccess;

namespace Business
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i <= 105; i++)
            {
                ItemDTO item = Items.Read(i);
                Console.WriteLine("Item id " + i.ToString() + " item name " + item.Name);

                string pic = null;
                pic = Console.ReadLine();
                item.IconLink = "~/Content/icons/png/" + pic;

                Items.Update(item);
            }
        }
    }
}
